using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class DirectoryNavTree
    {
        public Node Root;
        public Node Current;

        public DirectoryNavTree()
        {
            this.Root = new Node(new DirectoryItem() { FullPath = "Root", Type = DirectoryType.NUll });
            this.Current = this.Root;
        }

        public void AddNode(params DirectoryItem[] children)
        {
            foreach(DirectoryItem child in children)
            {
                if(child.Type != DirectoryType.File)
                {
                    this.Current.AddChlid(new Node(child));
                }
                else
                {
                    this.Current.AddChlid(new Leaf(child));
                }
            }
        }

        public void RemoveNode(Node Child) => this.Current.RemoveChild(Child.value.FullPath);


    }

    public class Node
    {
        public DirectoryItem value;
        public List<Node> children { private set; get; }

        public Node(DirectoryItem value)
        {
            this.value = value;
            this.children = new List<Node>();
        }

        virtual public void AddChlid(Node child)
        {
            this.children.Add(child);
            this.children = Algorithms.quickSort(this.children, 0, this.children.Count-1);
        }

        virtual public void RemoveChild(string Path)
        {
            int index = Algorithms.binarySearch(this.children, Path);
            this.children.RemoveAt(index);
        }
    }

    public class Leaf : Node
    {
        public Leaf(DirectoryItem value) : base(value)
        {
        }

        public override void AddChlid(Node child)
        {
            throw new NotSupportedException();
        }

        public override void RemoveChild(string Path)
        {
            throw new NotSupportedException();
        }
    }
}
