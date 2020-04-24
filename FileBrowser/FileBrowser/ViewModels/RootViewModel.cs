using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FileBrowser.Models;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using FileBrowser.Views;
using System.Text.RegularExpressions;

namespace FileBrowser.ViewModels
{
    public class RootViewModel : Screen
    {
        private string _searchValue, _name;
        private ObservableCollection<DirectoryItemViewModel> _drives;
        private InspectViewModel _viewport;
        private Stack<InspectViewModel> _backstack;
        private Stack<InspectViewModel> _forwardstack;
        private SettingsView _settingview;
        private SettingsViewModel _settingviewmodel;


        #region public varables
        public ObservableCollection<InspectViewModel> ViewPortChildren
        {
            get
            {
                ObservableCollection<InspectViewModel> temp = ViewPort.Children;
                if (FileBrowserSetting.FilterNotSearch && searchValue != null)
                {
                    temp = new ObservableCollection<InspectViewModel>(temp.Where(x => x.FullPath.Contains(searchValue)));
                }
                if (FileBrowserSetting.OnlyShowFilesVisted && searchValue != null)
                {
                    temp = new ObservableCollection<InspectViewModel>(temp.Where(x => x.Visted == true));
                }
                if (FileBrowserSetting.OnlyShowFilesHaventVisted && searchValue != null)
                {
                    temp = new ObservableCollection<InspectViewModel>(temp.Where(x => x.Visted == false));
                }
                return temp;
            }
        }
        public InspectViewModel ViewPort
        {
            get { return this._viewport; }
            set
            {
                this._viewport = value;
                Name = ViewPort.Name;
                NotifyOfPropertyChange(() => ViewPort);
            }
        }

        public string searchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                NotifyOfPropertyChange(() => searchValue);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }


        public ObservableCollection<DirectoryItemViewModel> Drives
        {
            get { return _drives; }
            set
            {
                _drives = value;
                NotifyOfPropertyChange(() => Drives);
            }
        }

        #endregion

        public RootViewModel()
        {
            this._settingview = new SettingsView();
            this._settingviewmodel = new SettingsViewModel(this._settingview, this);
            this._settingview.DataContext = this._settingviewmodel;
            this._backstack = new Stack<InspectViewModel>();
            this._forwardstack = new Stack<InspectViewModel>();
            ViewPort = new InspectViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.MyDocuments, $"{Environment.SpecialFolder.MyDocuments}", false);
            RefreshViewPort();
            NotifyOfPropertyChange(() => ViewPortChildren);
            this.Drives = new ObservableCollection<DirectoryItemViewModel>(DirectoryStructure.GetLogicalDrives().Select(e => new DirectoryItemViewModel(e.FullPath, e.Type, e.Name, e.Hidden)));
            DefultDirectoies();
        }

        private void DefultDirectoies()
        {
            string DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), DirectoryType.MyMusic, $"{Environment.SpecialFolder.MyMusic}", false));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), DirectoryType.MyVideos, $"{Environment.SpecialFolder.MyVideos}", false));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), DirectoryType.MyPhotos, $"{Environment.SpecialFolder.MyPictures}", false));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.MyDocuments, $"{Environment.SpecialFolder.MyDocuments}", false));
            this.Drives.Insert(0, new DirectoryItemViewModel(DownloadPath, DirectoryType.MyDownloads, "Downloads", false));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DirectoryType.Desktop, $"{Environment.SpecialFolder.Desktop}", false));
        }


        public void Exit(Window MainWindow)
        {
            this._settingview.Close();
            MainWindow.Close();
        }

        public void Minimise(Window MainWindow)
        {
            MainWindow.WindowState = WindowState.Minimized;
        }

        public void Maximise(Window MainWindow)
        {
            if(MainWindow.WindowState != WindowState.Maximized)
            {
                MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                MainWindow.WindowState = WindowState.Normal;
            }
        }

        public void OpenChild(InspectViewModel child, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2) 
            {
                if (child.Type != DirectoryType.File)
                {
                    this._backstack.Push(this.ViewPort);
                    if(this._forwardstack.Count > 0)
                    {
                        this._forwardstack.Clear();
                    }
                    for(int i = 0; i < ViewPort.Children.Count; i++)
                    {
                        if(ViewPort.Children[i].FullPath == child.FullPath)
                        {
                            ViewPort.Children[i].Visted = true;
                        }
                    }
                    this.ViewPort = child;
                    ViewPort.Visted = true;
                    RefreshViewPort();

                }
                else
                {
                    DirectoryStructure.OpenFileInProgramme(child.FullPath);
                }
            }
        }

        public void OpenFromSideBar(DirectoryItemViewModel child)
        {
            this._backstack.Push(this.ViewPort);
            this.ViewPort = child;
            RefreshViewPort();
        }

        public void BackButton()
        {
            if(this._backstack.Count > 0)
            {
                this._forwardstack.Push(this._viewport);
                this.ViewPort = this._backstack.Pop();
                this.ViewPort = this.ViewPort;
                NotifyOfPropertyChange(() => ViewPortChildren);
            }
        }

        public void ForwardButton()
        {
            if(this._forwardstack.Count > 0)
            {
                this._backstack.Push(this.ViewPort);
                this.ViewPort = this._forwardstack.Pop();
                NotifyOfPropertyChange(() => ViewPortChildren);
            }
        }

        public void Settings()
        {
            this._settingview.Show();
        }

        public void RefreshViewPort()
        {
            this.ViewPort.GetChildren();
            this.ViewPort.Visted = true;
            NotifyOfPropertyChange(() => ViewPortChildren);
        }

        public void NotifyChlidren() => NotifyOfPropertyChange(() => ViewPortChildren);


        public async void Search(KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!FileBrowserSetting.FilterNotSearch)
                {
                    await TreeSearch();
                }
                else
                {
                    NotifyChlidren();
                }
            }
        }

        private async Task TreeSearch()
        {
            Regex regex = new Regex("\\.[a-z | A-Z]+");
            Tree search = new Tree(this._viewport);
            List<TreeNode> results = new List<TreeNode>();
            if (regex.IsMatch(searchValue))
            {
                results = await search.DepthSearch(search.root, results, searchValue);
            }
            else
            {
                results = await Task.Run(() => search.BredthSearch(searchValue));
            }
            if (results.Count > 0)
            {
                this.ViewPort = new InspectViewModel($"Search: {searchValue}, {results.Count} Items", new ObservableCollection<InspectViewModel>(results.Select(x => new InspectViewModel(x.FullPath, x.Type, x.Name, x.Hidden))));
                RefreshViewPort();
            }
            else
            {
                Name = "No results found";
            }
        }

    }
}
