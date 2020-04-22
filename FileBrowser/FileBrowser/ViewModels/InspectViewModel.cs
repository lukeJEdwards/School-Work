using Caliburn.Micro;
using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileBrowser.ViewModels
{
    public class InspectViewModel : Screen
    {
        private ObservableCollection<InspectViewModel> _files, _folders;
        private string _fullpath, _name, _filterkey;
        private Visibility _hidden;
        private DirectoryType _type;

        #region Public Variables
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
            get { return new ObservableCollection<InspectViewModel>(this._folders.Concat(this._files)); }
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
        public string FilterKey
        {
            get { return this._filterkey; }
            set
            {
                this._filterkey = value;
                NotifyOfPropertyChange(() => FilterKey);
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

        public InspectViewModel(string fullpath, DirectoryType type, string name, Visibility hidden)
        {
            FullPath = fullpath;
            Name = name;
            Type = type;
            Hidden = hidden;
        }

        private void GetChildren()
        {
            List<DirectoryItem> folders = DirectoryStructure.GetDirectoryFolders(FullPath);
            List<DirectoryItem> files = DirectoryStructure.GetDirectoryFiles(FullPath);
            _folders = new ObservableCollection<InspectViewModel>(folders.Select(x => new InspectViewModel(x.FullPath, x.Type, x.Name, x.Hidden)));
            _files = new ObservableCollection<InspectViewModel>(files.Select(x => new InspectViewModel(x.FullPath, x.Type, x.Name, x.Hidden)));
        }
    }
}
