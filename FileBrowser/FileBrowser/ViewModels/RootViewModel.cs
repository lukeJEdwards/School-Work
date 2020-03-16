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

namespace FileBrowser.ViewModels
{
    public class RootViewModel : Screen
    {
        private FileItemViewModel _currentPath;
        private Stack<FileItemViewModel> _BackButtonStack;
        private Stack<FileItemViewModel> _ForwardButtonStack;
        private string _searchValue;
        private Tree _searchTree;
        private ObservableCollection<DirectoryItemViewModel> _drives;

        #region public varables

        public FileItemViewModel CurrentPath
        {
            get { return _currentPath; }
            set
            {
                _currentPath = value;
                NotifyOfPropertyChange(() => CurrentPath);
            }
        }

        public ObservableCollection<FileItemViewModel> CurrentPathChildren
        {
            get { return _currentPath.Children; }
            set
            {
                _currentPath.Children = value;
                NotifyOfPropertyChange(() => CurrentPathChildren);
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
            this._BackButtonStack = new Stack<FileItemViewModel>();
            this._ForwardButtonStack = new Stack<FileItemViewModel>();
            this.CurrentPath = new FileItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.SpecialFolder, $"{Environment.SpecialFolder.MyDocuments}");
            this.CurrentPath.GetChildren();
            Sidebar();
        }

        private void Sidebar()
        {
            List<Environment.SpecialFolder> folders = Enum.GetValues(typeof(Environment.SpecialFolder)).Cast<Environment.SpecialFolder>().Where(x => x.ToString().ToLower().Contains("my")).ToList();
            folders.Remove(Environment.SpecialFolder.MyDocuments);
            folders.Remove(Environment.SpecialFolder.MyComputer);
            List<DirectoryItem> drives = DirectoryStructure.GetLogicalDrives();
            this.Drives = new ObservableCollection<DirectoryItemViewModel>(folders.Select(e => new DirectoryItemViewModel(Environment.GetFolderPath(e), DirectoryType.SpecialFolder, e.ToString())).Concat(drives.Select(e => new DirectoryItemViewModel(e.FullPath, e.Type, e.Name))));
        }


        public void Exit(Window MainWindow)
        {
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

        public void BackButton()
        {
            if(this._BackButtonStack.Count > 0)
            {
                this._ForwardButtonStack.Push(CurrentPath);
                this.CurrentPath = this._BackButtonStack.Pop();
                NotifyOfPropertyChange(() => CurrentPathChildren);
            }
        }

        public void ForwardButton()
        {
            if(this._ForwardButtonStack.Count > 0)
            {
                this._BackButtonStack.Push(CurrentPath);
                this.CurrentPath = this._ForwardButtonStack.Pop();
                NotifyOfPropertyChange(() => CurrentPathChildren);
            }
        }

        public void Clicked(FileItemViewModel child)
        {
            if(child.Type == DirectoryType.File)
            {
                DirectoryStructure.OpenFileInProgramme(child.FullPath);
            }
            else
            {
                this._BackButtonStack.Push(CurrentPath);
                child.GetChildren();
                this.CurrentPath = child;
                NotifyOfPropertyChange(() => CurrentPathChildren);
            }
        }

        public void OpenFromSidebar(DirectoryItemViewModel path)
        {
            Clicked(new FileItemViewModel(path.FullPath, path.Type, path.Name));
        }

        public async void SearchEnter()
        {
            if (Keyboard.IsKeyDown(Key.Return))
            {
                _searchTree = new Tree(CurrentPath.FullPath, CurrentPath.Type);
                List<DirectoryItem> results;
                if (!_searchValue.Contains('.'))
                {
                    results = await App.Current.Dispatcher.InvokeAsync(() => _searchTree.BredthSearch(searchValue));
                }
                else
                {
                    results = await App.Current.Dispatcher.InvokeAsync(() => _searchTree.DepthSearch(searchValue));
                }
                if(results.Count > 0)
                {
                    this.CurrentPathChildren = App.Current.Dispatcher.Invoke(() => SearchResultSort(results, 0, results.Count - 1));
                }
            }
        }

        private ObservableCollection<FileItemViewModel> SearchResultSort(List<DirectoryItem> results, int high, int low)
        {
            if (low < high)
            {
                int p = partition(results, high, low);
                SearchResultSort(results, low, p - 1);
                SearchResultSort(results, p + 1, high);
            }
            return new ObservableCollection<FileItemViewModel>(results.Select(x => new FileItemViewModel(x, true)));
        }

        private int partition(List<DirectoryItem> arr, int high, int low)
        {
            string piviot = arr[high].FullPath;
            int i = low - 1;
            for(int j = 0; j<high; j++)
            {
                if(string.Compare(arr[j].FullPath, piviot) < 0)
                {
                    i++;
                    DirectoryItem temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            DirectoryItem temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            return i + 1;
        }
    }
}
