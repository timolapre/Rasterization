using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Template_P3
{
    interface IMesh
    {
        void Render(Shader shader, Matrix4 transform, Texture texture = null);
    }

    class MeshGroup : IMesh
    {
        Mesh[] meshes;
        Texture[] textures;

        public void Render(Shader shader, Matrix4 transform, Texture texture = null)
        {
            for(int i = 0; i< meshes.Length;i++)
            {
                meshes[i].Render(shader, transform, textures[i]);
            }
        }
    }
}
