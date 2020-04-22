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
        private string _searchValue;
        private ObservableCollection<DirectoryItemViewModel> _drives;

        #region public varables

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
            this.Drives = new ObservableCollection<DirectoryItemViewModel>(DirectoryStructure.GetLogicalDrives().Select(e => new DirectoryItemViewModel(e.FullPath, e.Type, e.Name, e.Hidden)));
            DefultDirectoies();
        }

        private void DefultDirectoies()
        {
            string DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), DirectoryType.MyMusic, $"{Environment.SpecialFolder.MyMusic}", Visibility.Visible));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), DirectoryType.MyVideos, $"{Environment.SpecialFolder.MyVideos}", Visibility.Visible));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), DirectoryType.MyPhotos, $"{Environment.SpecialFolder.MyPictures}", Visibility.Visible));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.MyDocuments, $"{Environment.SpecialFolder.MyDocuments}", Visibility.Visible));
            this.Drives.Insert(0, new DirectoryItemViewModel(DownloadPath, DirectoryType.MyDownloads, "Downloads", Visibility.Visible));
            this.Drives.Insert(0, new DirectoryItemViewModel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DirectoryType.Desktop, $"{Environment.SpecialFolder.Desktop}", Visibility.Visible));
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
    }
}
