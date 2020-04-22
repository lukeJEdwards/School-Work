using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FileBrowser.Models;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;

namespace FileBrowser.ViewModels
{
    public class DirectoryItemViewModel : Screen
    {
        private ObservableCollection<DirectoryItemViewModel> _children;
        private string _fullpath;
        private DirectoryType _type;
        private string _name;
        private BitmapImage _image;
        private Visibility _hidden;

        #region Public Variables
        public ObservableCollection<DirectoryItemViewModel> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                NotifyOfPropertyChange(() => Children);
            }
        }

        public string FullPath
        {
            get { return _fullpath; }
            set
            {
                this._fullpath = value;
                NotifyOfPropertyChange(() => FullPath);
            }
        }

        public DirectoryType Type
        {
            get { return this._type; }
        }

        public BitmapImage Image
        {
            get { return this._image; }
            set
            {
                this._image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public string Name
        {
            get { return this._name; }
            set
            {
                this._name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public bool CanExpand { get { return this.Type != DirectoryType.File; } }

        public bool IsExpanded
        {
            get { return this.Children?.Count(f => f != null) > 0; }
            set
            {
                if(value == true)
                {
                    Expand();
                }
                else
                {
                    this.ClearChildren();
                }
            }
        }
        public Visibility Hidden
        {
            get { return this._hidden; }
            set
            {
                this._hidden = value;
                NotifyOfPropertyChange(() => Hidden);
            }
        }
        #endregion

        public ICommand ExpandCommand { get; set; } 


        public DirectoryItemViewModel(string fullpath, DirectoryType type, string Name, Visibility hidden)
        {
            this.ExpandCommand = new RelayCommand(Expand);
            this.Hidden = hidden;
            this.FullPath = fullpath;
            this._type = type;
            this.Name = Name;
            this.ClearChildren();
            this.Image = new BitmapImage(new Uri(DirectoryStructure.GetPicture(this.Type)));

        }

        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();
            if(this.Type != DirectoryType.File)
            {
                this.Children.Add(null);
            }
        }

        private void Expand()
        {
            if(this.Type == DirectoryType.File)
            {
                return;
            }
            List<DirectoryItem> Children = DirectoryStructure.GetDirectoryFolders(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(Children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type, content.Name, content.Hidden)));
        }
    }
}
