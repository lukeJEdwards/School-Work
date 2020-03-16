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

namespace FileBrowser.ViewModels
{

    public class DirectoryItemBaseViewModel : Screen
    {

    }
    public class DirectoryItemViewModel : Screen
    {
        private ObservableCollection<DirectoryItemViewModel> _children;
        private string _fullpath;
        private DirectoryType _type;
        private string _name;

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

        public BitmapImage Image { set; get; }

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

        public ICommand ExpandCommand { get; set; } 


        public DirectoryItemViewModel(string fullpath, DirectoryType type, string Name)
        {
            this.ExpandCommand = new RelayCommand(Expand);

            this.FullPath = fullpath;
            this._type = type;
            this.Name = Name;
            this.ClearChildren();

            if(type == DirectoryType.SpecialFolder)
            {
                this.Image = new BitmapImage(new Uri(GetSpecialFolderSource()));
            }
            else
            {
                this.Image = new BitmapImage(new Uri(GetImageSource()));
            }

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

            List<DirectoryItem> Children = DirectoryStructure.GetDirectoryContent(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(Children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type, content.Name)));
        }

        private string GetImageSource()
        {
            switch (this._type)
            {
                case DirectoryType.Drive:
                    return "pack://application:,,,/Icons/Drive.png";
                case DirectoryType.File:
                    return "pack://application:,,,/Icons/File.png";
                case DirectoryType.Folder:
                    return "pack://application:,,,/Icons/Folder.png";
                default:
                    return "pack://application:,,,/Icons/Error.png";
            }
        }

        private string GetSpecialFolderSource()
        {
            switch (this._name)
            {
                case "MyDocuments":
                    return "pack://application:,,,/Icons/MyDocuments.png";
                case "MyVideos":
                    return "pack://application:,,,/Icons/MyVideos.png";
                case "MyPictures":
                    return "pack://application:,,,/Icons/MyPhotos.png";
                case "MyDownloads":
                    return "pack://application:,,,/Icons/MyDownloads.png";
                case "MyMusic":
                    return "pack://application:,,,/Icons/MyMusic.png";
                default:
                    return "pack://application:,,,/Icons/Error.png";
            }
        }



    }
}
