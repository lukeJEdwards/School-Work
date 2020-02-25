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
        public Tree(string Root)
        {
            this.root = new TreeNode(Root, null);
        }

        public void Search(string value)
        {
            Queue<TreeNode> NotVisted = new Queue<TreeNode>();
            Queue<TreeNode> Visted = new Queue<TreeNode>();
            NotVisted.Enqueue(this.root);
            while(NotVisted.Count > 0)
            {
                TreeNode Visting = NotVisted.Dequeue();
                if(Visting.Children.Count > 0)
                {
                    foreach(TreeNode node in Visting.Children)
                    {
                        NotVisted.Enqueue(node);
                    }
                }
                Visted.Enqueue(Visting);
            }
        }

    }


    public class TreeNode
    {
        private string data;
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

        public TreeNode() { }
        public TreeNode(string FullPath, TreeNode Parent)
        {
            this.data = FullPath;
            this.parent = Parent;
            this.getChildren();
        }

        private void getChildren()
        {
            foreach(string dir in Directory.GetFiles(this.data))
            {
                AddChild(dir, this);
            }
        }

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
