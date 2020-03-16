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
        private bool _GoingBack;
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
            this._GoingBack = false;
            this.Drives = new ObservableCollection<DirectoryItemViewModel>(DirectoryStructure.GetLogicalDrives().Select(e => new DirectoryItemViewModel(e.FullPath, e.Type, e.Name)));
            DefultDirectoies();
            this.CurrentPath = new FileItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.SpecialFolder, $"{Environment.SpecialFolder.MyDocuments}");
            this.CurrentPath.GetChildren();
        }

        private void DefultDirectoies()
        {
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.SpecialFolder, $"{Environment.SpecialFolder.MyDocuments}"));
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
            if (this._BackButtonStack.Count > 0)
            {
                this._ForwardButtonStack.Push(this.CurrentPath);
                this.CurrentPath = this._BackButtonStack.Pop();
                NotifyOfPropertyChange(() => this.CurrentPathChildren);
                this._GoingBack = true;
            }
        }

        public void ForwardButton()
        {
            if (this._ForwardButtonStack.Count > 0)
            {
                this._BackButtonStack.Push(this.CurrentPath);
                this.CurrentPath = this._ForwardButtonStack.Pop();
                NotifyOfPropertyChange(() => this.CurrentPathChildren);
            }
        }

        public void OpenChild(FileItemViewModel child)
        {
            this._BackButtonStack.Push(this.CurrentPath);
            this.CurrentPath = child;
            this.CurrentPath.GetChildren();
            NotifyOfPropertyChange(() => CurrentPathChildren);
            if (this._GoingBack)
            {
                this._ForwardButtonStack.Clear();
            }
        }

        public void OpenFromSidebar(DirectoryItemViewModel path)
        {
            OpenChild(new FileItemViewModel(path.FullPath, path.Type, path.Name));
        }

        public void SearchEnter()
        {
            if (Keyboard.IsKeyDown(Key.Return))
            {
                _searchTree = new Tree(CurrentPath.FullPath, CurrentPath.Type);
                List<DirectoryItem> results;
                if (!_searchValue.Contains('.'))
                {
                    results = _searchTree.BredthSearch(searchValue);
                }
                else
                {
                    results = _searchTree.DepthSearch(searchValue);
                }
                this.CurrentPathChildren = new ObservableCollection<FileItemViewModel>(results.Select(x => new FileItemViewModel(x, true)));
            }
        }
    }
}
