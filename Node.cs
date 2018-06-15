using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_P3;

namespace template_P3
{
    public class Node
    {
        public IMesh mesh;
        public List<Node> Children;

        public Node(IMesh mesh, List<Node> Children = null)
        {
            this.mesh = mesh;
            if (Children == null)
                this.Children = new List<Node>();
            else
                this.Children = Children;
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }

        public void RemoveChild(Node child)
        {
            Children.Remove(child);
        }

        public void RemoveChild(int childIndex)
        {
            Children.RemoveAt(childIndex);
        }
    }
}
