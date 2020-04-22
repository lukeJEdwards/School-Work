using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileBrowser.Models
{
    public static class DirectoryStructure
    {
        /// <summary>
        /// Trys to delete directory
        /// </summary>
        /// <param name="fullPath"></param>
        public static void DeleteDir(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                throw new DirectoryNotFoundException();
            }
            else
            {
                try
                {
                    Directory.Delete(fullPath, true);
                }
                catch (Exception)
                {
                    throw new DirectoryActionFailedException();
                }
            }
        }

        /// <summary>
        /// Trys to create a new directory
        /// </summary>
        /// <param name="fullPath"></param>
        public static void CreateDir(string fullPath)
        {
            if (Directory.Exists(fullPath))
            {
                throw new DirectoryFoundException();
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(fullPath);
                }
                catch (Exception)
                {
                    throw new DirectoryActionFailedException();
                }
            }
        }

        /// <summary>
        /// Trys to deletes the file specifed
        /// </summary>
        /// <param name="fullPath"></param>
        public static void DeleteFile(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException();
            }
            else
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception)
                {
                    throw new FileActionFailedException();
                }
            }
        }

        /// <summary>
        /// Trys to create a new file with 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="name"></param>
        public static void CreateNewFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                throw new FileFoundException();
            }
            else
            {
                try
                {
                    File.Create(fullPath);
                }
                catch (Exception)
                {
                    throw new FileActionFailedException();
                }
            }
        }

        /// <summary>
        /// Open file in defult programme defined from the windows operating system
        /// </summary>
        /// <param name="fullPath"></param>
        public static void OpenFileInProgramme(string fullPath)
        {
            //trys to open file
            try
            { 
                //open file in programme
                Process.Start(fullPath);
            }
            catch (Exception)
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Gets all of the file and folders in the specified directory
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryFolders(string fullPath)
        {
            try
            {
                List<DirectoryItem> items = new List<DirectoryItem>();
                string[] folders = Directory.GetDirectories(fullPath);
                if(folders.Length > 0)
                {
                    foreach(string folder in folders)
                    {
                        if (IsHidden(folder))
                        {
                            items.Add(new DirectoryItem() { FullPath = folder, Type = DirectoryType.Folder, Hidden = Visibility.Collapsed});
                        }
                        else
                        {
                            items.Add(new DirectoryItem() { FullPath = folder, Type = DirectoryType.Folder, Hidden = Visibility.Visible });
                        }
                    }
                }
                return items;
            }catch
            {
                throw new DirectoryNotFoundException();
            }
        }

        private static bool IsHidden(string fullpath)
        {
            DirectoryInfo di = new DirectoryInfo(fullpath);
            return di.Attributes.HasFlag(FileAttributes.Hidden) || di.Attributes.HasFlag(FileAttributes.System);
        }

        public static List<DirectoryItem> GetDirectoryFiles(string fullpath)
        {
            try
            {
                List<DirectoryItem> items = new List<DirectoryItem>();
                string[] files = Directory.GetFiles(fullpath);
                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        items.Add(new DirectoryItem() { FullPath = file, Type = DirectoryType.File });
                    }
                }
                return items;
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }


        /// <summary>
        /// get list of all logic drives from the machine
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            List<DirectoryItem> LogicalDrives = new List<DirectoryItem>();
            //collects the name of all logical dive on the computer and loop through them
            foreach(string drive in Directory.GetLogicalDrives())
            {
                //converts drive into a DirectoryItem and adds to LogicalDrive list
                LogicalDrives.Add(new DirectoryItem { FullPath = drive, Type = DirectoryType.Drive });
            }
            return LogicalDrives;
        }


        #region hepler
        /// <summary>
        /// Find the file or folder name from the full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileOrFolderName(string path)
        {
            //return empty string if no path
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            //makes all slashes, back slashes 
            string normalisedPath = path.Replace('/', '\\');
            int lastIndex = path.LastIndexOf('\\');
            //if no back slashes are found reurnt the path
            if(lastIndex <= 0)
            {
                return path;
            }
            //return the name after the last back slash
            return path.Substring(lastIndex + 1);
        }

        public static string GetPicture(DirectoryType type)
        {
            switch (type)
            {
                case DirectoryType.File:
                    return "pack://application:,,,/Icons/File.png";
                case DirectoryType.Folder:
                    return "pack://application:,,,/Icons/Folder.png";
                case DirectoryType.Drive:
                    return "pack://application:,,,/Icons/Drive.png";
                case DirectoryType.MyDocuments:
                    return "pack://application:,,,/Icons/MyDocuments.png";
                case DirectoryType.MyDownloads:
                    return "pack://application:,,,/Icons/MyDownloads.png";
                case DirectoryType.MyPhotos:
                    return "pack://application:,,,/Icons/MyPhotos.png";
                case DirectoryType.MyVideos:
                    return "pack://application:,,,/Icons/MyVideos.png";
                case DirectoryType.MyMusic:
                    return "pack://application:,,,/Icons/MyMusic.png";
                case DirectoryType.Desktop:
                    return "pack://application:,,,/Icons/Desktop.png";
                case DirectoryType.NUll:
                    return "pack://application:,,,/Icons/Error.png";
                default:
                    return "pack://application:,,,/Icons/Error.png";
            }
        }

        #endregion
    }

        #region custom Exceptions
        /// <summary>
        /// Class for File found, file action failed and directory found, directory action failed
        /// </summary>
        public class FileFoundException : Exception
        {
            public FileFoundException() : base() { }
            public FileFoundException(String Message) : base(Message) { }
            public FileFoundException(String Message, Exception innerException) : base(Message, innerException) { }
            public FileFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
        public class FileActionFailedException : Exception
        {
            public FileActionFailedException() : base() { }
            public FileActionFailedException(String Message) : base(Message) { }
            public FileActionFailedException(String Message, Exception innerException) : base(Message, innerException) { }
            public FileActionFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class DirectoryActionFailedException : Exception
        {
            public DirectoryActionFailedException() : base() { }
            public DirectoryActionFailedException(String message) : base(message) { }
            public DirectoryActionFailedException(String message, Exception innerException) : base(message, innerException) { }
            public DirectoryActionFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class DirectoryFoundException : Exception
        {
            public DirectoryFoundException() : base() { }
            public DirectoryFoundException(String Message) : base(Message) { }
            public DirectoryFoundException(String Message, Exception innerException) : base(Message, innerException) { }
            public DirectoryFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
        #endregion
    }
