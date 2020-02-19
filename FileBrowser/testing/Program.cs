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
            List<DirectoryItem> a = DirectoryStructure.GetLogicalDrives();
            List<DirectoryItem> b = DirectoryStructure.GetDirectoryContent(a[2].FullPath);
            List<DirectoryItem> c = DirectoryStructure.GetDirectoryContent(a[1].FullPath);
            DirectoryNavTree d = new DirectoryNavTree();
            d.AddNode(a.ToArray());
            d.Current = d.Root.children[1];
            d.AddNode(c.ToArray());
            d.Current = d.Root.children[2];
            d.AddNode(b.ToArray());
            printChildren(d.Root);
        }

        static void printChildren(Node a)
        {
            if(a.children.Count > 0)
            {
                foreach(Node child in a.children)
                {
                    Console.WriteLine($"Name: {child.value.Name}, Type: {child.value.Type}");
                    printChildren(child);
                }
            }
        }
    }
}
