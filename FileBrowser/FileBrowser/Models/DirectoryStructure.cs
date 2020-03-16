using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
        public static List<DirectoryItem> GetDirectoryContent(string fullPath)
        {
            //create empty item list
            List<DirectoryItem> items = new List<DirectoryItem>();
            List<string> SDirs = Enum.GetNames(typeof(Environment.SpecialFolder)).ToList<string>();
            string[] dirs;
            #region Gets folders
            //trys to get directories that are in fullPath
            dirs = Directory.GetDirectories(fullPath);
            try
            {
                if (dirs.Length > 0)
                {
                    //loop through Directories and converts them to directoryitem and adds them to item list
                    foreach (string dir in dirs)
                    {
                        DirectoryInfo info = new DirectoryInfo(dir);
                        if (!Flag(info))
                        {
                            if (SDirs.Contains(GetFileOrFolderName(dir).Replace(" ", "")))
                            {
                                items.Add(new DirectoryItem() { FullPath = dir, Type = DirectoryType.SpecialFolder });
                            }
                            else
                            {
                                items.Add(new DirectoryItem() { FullPath = dir, Type = DirectoryType.Folder });
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw new DirectoryNotFoundException();
            }
            #endregion
            #region Get Files
            //trys to ge files that are in directory
            try
            {
                //gets all file names in directory
                string[] files = Directory.GetFiles(fullPath);
                if(files.Length > 0)
                {
                    //loop through file names converts them to directory item and add them to tiems
                    foreach(string file in files)
                    {
                        FileInfo info = new FileInfo(file);
                        if (!Flag(info))
                        {
                            items.Add(new DirectoryItem() { FullPath = file, Type = DirectoryType.File });
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw new FileNotFoundException();
            }
            return items;
            #endregion
        }

        private static bool Flag(DirectoryInfo e) => e.Attributes.HasFlag(FileAttributes.Hidden) || e.Attributes.HasFlag(FileAttributes.System) || e.Attributes.HasFlag(FileAttributes.Archive);
        private static bool Flag(FileInfo e) => e.Attributes.HasFlag(FileAttributes.Hidden) || e.Attributes.HasFlag(FileAttributes.System);


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
