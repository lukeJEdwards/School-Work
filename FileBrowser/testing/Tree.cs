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

        public string[] BredthSearch(string value)
        {
            Queue<TreeNode> ToVist = new Queue<TreeNode>();
            List<TreeNode> Visted = new List<TreeNode>();
            List<string> Results = new List<string>();
            ToVist.Enqueue(this.root);
            while(ToVist.Count > 0)
            {
                TreeNode Visting = ToVist.Dequeue();
                if (Visting.Data.Contains(value))
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
            return Results.ToArray();
        }

        public string[] DepthSearch(string value)
        {
            List<TreeNode> visted = new List<TreeNode>();
            List<string> Results = new List<string>();
            DF(ref visted, ref Results, this.root, value);
            return Results.ToArray();
        }

        private void DF(ref List<TreeNode> Visted, ref List<string> Results, TreeNode node, string value)
        {
            if (node.Children.Count > 0)
            {
                Visted.Add(node);
                foreach(TreeNode child in node.Children)
                {
                    if (child.Data.Contains(value))
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
        private string data;
        private DirectoryType type;
        private TreeNode parent;
        private List<TreeNode> children;
        public string Data
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
        public DirectoryType Type
        {
            get { return type; }
            set { type = value; }
        }

        public TreeNode(string FullPath, TreeNode Parent)
        {
            this.parent = Parent;
            this.data = FullPath;
            this.type = DirectoryType.File;
            this.children = new List<TreeNode>();
        }
        public TreeNode(string FullPath, DirectoryType Type, TreeNode Parent)
        {
            this.data = FullPath;
            this.parent = Parent;
            this.type = Type;
            this.children = new List<TreeNode>();
            this.getChildren();
        }

        private void getChildren()
        {
            if(this.type != DirectoryType.File)
            {
               List<DirectoryItem> directoryItems = DirectoryStructure.GetDirectoryContent(this.data);
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
                if (children[mid].Data == value)
                {
                    return mid;
                }else if(string.Compare(children[mid].Data, value) < 0)
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
