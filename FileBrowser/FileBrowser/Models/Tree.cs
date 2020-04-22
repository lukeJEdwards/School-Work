using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class Tree
    {
        private TreeNode root;
        public TreeNode Root
        {
            get { return root; }
            set { root = value; }
        }

        public Tree() { }
        public Tree(string Root, DirectoryType type)
        {
            this.root = new TreeNode(Root, type, null);
        }

        public List<DirectoryItem> BredthSearch(string value)
        {
            Queue<TreeNode> ToVist = new Queue<TreeNode>();
            List<TreeNode> Visted = new List<TreeNode>();
            List<DirectoryItem> Results = new List<DirectoryItem>();
            ToVist.Enqueue(this.root);
            while(ToVist.Count > 0)
            {
                TreeNode Visting = ToVist.Dequeue();
                if (Visting.Data.FullPath.Contains(value))
                {
                    Results.Add(Visting.Data);
                }
                if(Visting.Children.Count> 0)
                {
                    foreach(TreeNode child in Visting.Children)
                    {
                        ToVist.Enqueue(child);
                    }
                }
                Visted.Add(Visting);
            }
            return Results;
        }

        public List<DirectoryItem> DepthSearch(string value)
        {
            List<TreeNode> visted = new List<TreeNode>();
            List<DirectoryItem> Results = new List<DirectoryItem>();
            DF(ref visted, ref Results, this.root, value);
            return Results;
        }

        private void DF(ref List<TreeNode> Visted, ref List<DirectoryItem> Results, TreeNode node, string value)
        {
            if (node.Children.Count > 0)
            {
                Visted.Add(node);
                foreach(TreeNode child in node.Children)
                {
                    if (child.Data.FullPath.Contains(value))
                    {
                        Results.Add(child.Data);
                    }
                    DF(ref Visted, ref Results, child, value);
                }
            }
        }

    }


    public class TreeNode
    {
        private DirectoryItem data;
        private TreeNode parent;
        private List<TreeNode> children;
        public DirectoryItem Data
        {
            get { return data; }
        }
        public List<TreeNode> Children
        {
            get { return children; }
            set { children = value; }
        }
        public TreeNode Parent
        {
            get { return parent; }
        }

        public TreeNode(string FullPath, TreeNode Parent)
        {
            this.parent = Parent;
            this.data = new DirectoryItem { FullPath = FullPath, Type = DirectoryType.File };
            this.children = new List<TreeNode>();
        }
        public TreeNode(string FullPath, DirectoryType Type, TreeNode Parent)
        {
            this.data = new DirectoryItem { FullPath = FullPath, Type = Type };
            this.parent = Parent;
            this.children = new List<TreeNode>();
            this.getChildren();
        }

        private void getChildren()
        {
            if(this.Data.Type != DirectoryType.File)
            {
               List<DirectoryItem> directoryItems = DirectoryStructure.GetDirectoryFolders(this.data.FullPath);
                foreach(DirectoryItem item in directoryItems)
                {
                    if(item.Type == DirectoryType.File)
                    {
                        AddChild(item.FullPath, this);
                    }
                    else
                    {
                        AddChild(item.FullPath, item.Type, this);
                    }
                }

            }
        }

        public void AddChild(string Value, DirectoryType Type, TreeNode parent) => children.Add(new TreeNode(Value, Type, parent));
        public void AddChild(string Value, TreeNode parent) => children.Add(new TreeNode(Value, parent));
        public void RemoveChild(string Value) => children.RemoveAt(FindChild(Value));

        public int FindChild(string value)
        {
            int high = children.Count - 1;
            int low = 0;
            int mid = (high + low) / 2;
            while (high > low)
            {
                mid = (high + low) / 2;
                if (children[mid].Data.FullPath == value)
                {
                    return mid;
                }else if(string.Compare(children[mid].Data.FullPath, value) < 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return -1;
        }
    }
}
