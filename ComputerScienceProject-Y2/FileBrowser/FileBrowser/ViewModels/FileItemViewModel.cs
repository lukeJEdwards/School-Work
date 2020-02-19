using Caliburn.Micro;
using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FileBrowser.ViewModels
{
    public class FileItemViewModel : Screen
    {
        private string _fullpath;
        private ObservableCollection<FileItemViewModel> _children;
        private string _name;
        private DirectoryType _type;

        public string FullPath
        {
            get { return _fullpath; }
            set
            {
                _fullpath = value;
                NotifyOfPropertyChange(() => FullPath);
            }
        }

        public ObservableCollection<FileItemViewModel> Children
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
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public BitmapImage Image { set; get; }

        public FileItemViewModel(string FullPath, DirectoryType Type, string Name)
        {
            this.FullPath = FullPath;
            this.Name = Name;
            this._type = Type;
            if (this._type == DirectoryType.SpecialFolder)
            {
                this.Image = new BitmapImage(new Uri(GetSpecialFolderSource()));
            }
            else
            {
                this.Image = new BitmapImage(new Uri(GetImageSource()));
            }
        }

        public void GetChildren()
        {
            List<DirectoryItem> children = DirectoryStructure.GetDirectoryContent(this.FullPath);
            this.Children = new ObservableCollection<FileItemViewModel>(children.Select(content => new FileItemViewModel(content.FullPath, content.Type, content.Name)));
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
                case "MyPhoto":
                    return "pack://application:,,,/Icons/MyPhoto.png";
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
