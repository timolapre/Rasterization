using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_P3;

namespace template_P3
{
    public class Hierarchy
    {
        public Mesh mesh;
        public List<Hierarchy> Children;

        public Hierarchy(Mesh mesh, List<Hierarchy> Children = null)
        {
            this.mesh = mesh;
            if(Children == null)
                 this.Children = new List<Hierarchy>();
            else
                this.Children = Children;
        }

        public void AddChild(Hierarchy child)
        {
            Children.Add(child);
        }

        public void RemoveChild(Hierarchy child)
        {
            Children.Remove(child);
        }

        public void RemoveChild(int childIndex)
        {
            Children.RemoveAt(childIndex);
        }
    }
}
