using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public static class FileBrowserSetting  
    {
        public static bool ShowHiddenFiles { get; set; } = false;
        public static bool OnlyShowFilesVisted { get; set; } = false;
        public static bool FilterNotSearch { get; set; } = false;
        public static bool OnlyShowFilesHaventVisted { get; set; } = false;
    }
}
