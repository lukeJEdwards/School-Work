using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public struct DirectoryItem
    {
        public DirectoryType Type { set; get; }
        public string FullPath { set; get; }

        public string Name { get { return this.Type == DirectoryType.Drive ? this.FullPath : DirectoryStructure.GetFileOrFolderName(this.FullPath); } }
    }

    public enum DirectoryType
    {
        File,
        Folder,
        Drive,
        NUll
    }
}
