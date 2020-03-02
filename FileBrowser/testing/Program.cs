using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree a = new Tree(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryType.Folder);
            string[] b = a.DepthSearch("School");
            foreach(string c in b)
            {
                Console.WriteLine(c);
            }
            Console.ReadLine();
        }
    }
}
