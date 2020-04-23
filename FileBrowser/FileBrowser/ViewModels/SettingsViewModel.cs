using Caliburn.Micro;
using FileBrowser.Models;
using FileBrowser.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileBrowser.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private SettingsView _settingsview;
        private RootViewModel _rootview;

        public bool ShowHiddenItems
        {
            get { return FileBrowserSetting.ShowHiddenFiles; }
            set
            {
                FileBrowserSetting.ShowHiddenFiles = value;
                this._rootview.RefreshViewPort();
                NotifyOfPropertyChange(() => ShowHiddenItems);
            }
        }

        public bool OnlyShowFilesVisted
        {
            get { return FileBrowserSetting.OnlyShowFilesVisted; }
            set
            {
                FileBrowserSetting.OnlyShowFilesVisted = value;
                this._rootview.NotifyChlidren();
                NotifyOfPropertyChange(() => OnlyShowFilesVisted);
            }
        }

        public bool FilterNotSearch
        {
            get { return FileBrowserSetting.FilterNotSearch; }
            set
            {
                FileBrowserSetting.FilterNotSearch = value;
                this._rootview.NotifyChlidren();
                NotifyOfPropertyChange(() => FilterNotSearch);
            }
        }

        public bool OnlyShowFileNotVisted
        {
            get { return FileBrowserSetting.OnlyShowFilesHaventVisted; }
            set
            {
                FileBrowserSetting.OnlyShowFilesHaventVisted = value;
                this._rootview.NotifyChlidren();
                NotifyOfPropertyChange(() => OnlyShowFileNotVisted);
            }
        }

        public SettingsViewModel(SettingsView settingsview, RootViewModel rootview)
        {
            this._settingsview = settingsview;
            this._rootview = rootview;
        }
        public void Exit()
        {
            this._settingsview.Hide();
        }
    }
}
