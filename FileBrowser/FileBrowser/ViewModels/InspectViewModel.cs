using Caliburn.Micro;
using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FileBrowser.ViewModels
{
    public class InspectViewModel : Screen
    {
        private ObservableCollection<InspectViewModel> _children;
        private string _fullpath, _name;
        private bool _hidden;
        private BitmapImage _image;
        private DirectoryType _type;
        private bool _visted;

        #region Public Variables
        public BitmapImage Image
        {
            get { return this._image; }
            set
            {
                this._image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }
        public string FullPath
        {
            get { return this._fullpath; }
            set
            {
                this._fullpath = value;
                NotifyOfPropertyChange(() => FullPath);
            }
        }

        public DirectoryType Type
        {
            get { return this._type; }
            set
            {
                this._type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }

        public ObservableCollection<InspectViewModel> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                NotifyOfPropertyChange(() => Children);
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

        public bool Hidden
        {
            get { return this._hidden; }
            set
            {
                this._hidden = value;
                NotifyOfPropertyChange(() => Hidden);
            }
        }

        public bool Visted
        {
            get { return this._visted; }
            set
            {
                this._visted = value;
                NotifyOfPropertyChange(() => Visted);
            }
        }
        #endregion

        public InspectViewModel(string fullpath, DirectoryType type, string name, bool hidden)
        {
            FullPath = fullpath;
            Name = name;
            Type = type;
            Image = new BitmapImage(new Uri(DirectoryStructure.GetPicture(type)));
            if (FileBrowserSetting.ShowHiddenFiles)
            {
                Hidden = false;
            }
            else
            {
                Hidden = hidden;
            }
        }

        public InspectViewModel(string Name, ObservableCollection<InspectViewModel> children)
        {
            Children = children;
            this.Name = Name;
        }

        public void GetChildren()
        {
            if (FullPath != null && Type != DirectoryType.File)
            {
                List<DirectoryItem> item = DirectoryStructure.GetDirectoryItems(this.FullPath);
                Children = new ObservableCollection<InspectViewModel>(item.Select(x => new InspectViewModel(x.FullPath, x.Type, x.Name, x.Hidden)));
            }
        }

        public static implicit operator InspectViewModel(DirectoryItemViewModel e) => new InspectViewModel(e.FullPath, e.Type, e.Name, e.Hidden);
    }
}
