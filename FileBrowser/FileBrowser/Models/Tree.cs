using Caliburn.Micro;
using FileBrowser.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class Tree
    {
        public TreeNode root;
        public Tree(InspectViewModel viewport)
        {
            root = new TreeNode(viewport.FullPath, viewport.Type, viewport.Name, viewport.Hidden);
        } 

        public List<TreeNode> BredthSearch(string value)
        {
            Queue<TreeNode> ToVist = new Queue<TreeNode>();
            List<TreeNode> results = new List<TreeNode>();
            ToVist.Enqueue(this.root);
            while (ToVist.Count > 0)
            {
                TreeNode visiting = ToVist.Dequeue();
                visiting.GetChildren();
                if (visiting.FullPath.Contains(value))
                {
                    results.Add(visiting);
                }
                if (visiting.Type != DirectoryType.File && visiting.HasChildren())
                {
                    foreach(TreeNode child in visiting.Children)
                    {
                        if (!child.Hidden)
                        {
                            ToVist.Enqueue(child);
                        }
                    }
                }
            }
            ToVist.Clear();
            return results;
        }

        public async Task<List<TreeNode>> DepthSearch(TreeNode CurrentNode, List<TreeNode> results, string value)
        {
            if (CurrentNode.Type != DirectoryType.File)
            {
                CurrentNode.GetChildren();
                foreach (TreeNode child in CurrentNode.Children)
                {
                    if (!results.Contains(child) && child.FullPath.Contains(value))
                    {
                        results.Add(child);
                    }
                    await Task.Run(() => DepthSearch(child, results, value));
                }
            }
            return results;
        }


    }

    public class TreeNode
    {
        public List<TreeNode> Children;
        public string FullPath;
        public DirectoryType Type;
        public bool Hidden;
        public string Name;

        public TreeNode(string FullPath, DirectoryType Type, string Name, bool hidden)
        {
            this.Hidden = hidden;
            this.FullPath = FullPath;
            this.Type = Type;
            this.Name = Name;
        }

        public void GetChildren()
        {
            if (this.Type != DirectoryType.File)
            {
                List<DirectoryItem> Items = DirectoryStructure.GetDirectoryItems(this.FullPath);
                this.Children = Items.Select(x => new TreeNode(x.FullPath, x.Type, x.Name, x.Hidden)).ToList();
            }
        }

        public bool HasChildren() => Children.Count > 0;
        public void AddChild(TreeNode child) => this.Children.Add(child);

    }
}
