using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_P3;

namespace template_P3
{
    public class SceneGraph
    {
        Hierarchy Scene = new Hierarchy(null);
        public List<Mesh> MeshList = new List<Mesh>();

        // create shaders
        Shader shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
        // load a texture
        Texture wood = new Texture( "../../assets/wood.jpg" );

        float a = 0;
        Stopwatch timer = new Stopwatch();

        public void Init()
        {
            Hierarchy Child = new Hierarchy(new Mesh("../../assets/teapot.obj", new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
            Child.AddChild(new Hierarchy(new Mesh("../../assets/floor.obj", new Vector3(0, -20, 0), new Vector3(0, 0, 0))));
            Scene.AddChild(Child);
        }

        public void Render(Matrix4 CameraMatrix)
        {
            foreach(Hierarchy child in Scene.Children)
                RenderChildren(child, CameraMatrix);

            foreach (Mesh item in MeshList)
            {
                /*// measure frame duration
                float frameDuration = timer.ElapsedMilliseconds;
                timer.Reset();
                timer.Start();

                Matrix4 transform = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
                transform *= Matrix4.CreateTranslation(0, -4, -15);
                transform *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

                // update rotation
                a += 0.01f;
                if (a > 2 * Math.PI) a -= 2 * (float)Math.PI;*/

                //item.Render(shader, CameraMatrix, wood);
            }
        }

        public void RenderChildren(Hierarchy parent, Matrix4 CameraMatrix)
        {
            parent.mesh.Render(shader, CameraMatrix, wood);
            foreach (Hierarchy child in parent.Children)
                RenderChildren(child, CameraMatrix);
        }
        
        public void Add(Mesh mesh)
        {
            MeshList.Add(mesh);
        }
    }
}
