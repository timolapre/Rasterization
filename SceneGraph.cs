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

        public List<Mesh> MeshList = new List<Mesh>();

        // create shaders
        Shader shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
        // load a texture
        Texture wood = new Texture( "../../assets/wood.jpg" );

        float a = 0;
        Stopwatch timer = new Stopwatch();

<<<<<<< HEAD
        public void Init()
		{
			Node Child = new Node(new Mesh("../../assets/floor.obj", new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
			Node Child2 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(0, -2, 0), new Vector3(.5f, 0, 0)));
			Child.AddChild(Child2);
			Scene.AddChild(Child);
			Child = new Node(new Mesh("../../assets/teapot.obj", new Vector3(0, 20, 0), new Vector3(0, 0, 0)));
            Scene.AddChild(Child);
		}

=======
>>>>>>> 0032aa5cd4ddebb8910c9ddd7a12400297634737
        public void Render(Matrix4 CameraMatrix)
        {
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

                item.Render(shader, CameraMatrix, wood);
            }
        }
        public void Add(Mesh mesh)
        {
            MeshList.Add(mesh);
        }
    }
}
