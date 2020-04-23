using FileBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileBrowser.Models
{
    public struct DirectoryItem
    {
        public DirectoryType Type { set; get; }
        public string FullPath { set; get; }
        public bool Hidden { set; get; }
        public string Name { get { return this.Type == DirectoryType.Drive ? $"(Local Disk): {this.FullPath}" : DirectoryStructure.GetFileOrFolderName(this.FullPath); } }

        public static implicit operator DirectoryItem(InspectViewModel e) => new DirectoryItem() {FullPath = e.FullPath, Type = e.Type, Hidden = e.Hidden };
        public static implicit operator DirectoryItem(DirectoryItemViewModel e) => new DirectoryItem() {FullPath = e.FullPath, Type = e.Type, Hidden = e.Hidden };
    }

    public enum DirectoryType
    {
        File,
        Folder,
        Drive,
        MyDocuments,
        MyDownloads,
        MyPhotos,
        MyVideos,
        MyMusic,
        Desktop,
        NUll
    }
}
