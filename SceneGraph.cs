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
        Node Scene = new Node(null);

        // create shaders
        Shader shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
        // load a texture
        Texture wood = new Texture( "../../assets/wood.jpg" );

        float a = 0;
        Stopwatch timer = new Stopwatch();

        public void Init()
		{
			Node Child = new Node(new Mesh("../../assets/floor.obj", new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
			Node Child2 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(0, -2, 0), new Vector3(.5f, 0, 0)));
			Child.AddChild(Child2);
			Scene.AddChild(Child);
			Child = new Node(new Mesh("../../assets/teapot.obj", new Vector3(0, 20, 0), new Vector3(0, 0, 0)));
            Scene.AddChild(Child);
		}

        public void Render(Matrix4 CameraMatrix)
        {
            foreach(Node child in Scene.Children)
                RenderChildren(child, CameraMatrix);
        }

        public void RenderChildren(Node parent, Matrix4 LocalMatrix)
        {
            LocalMatrix.Row3 += new Vector4(parent.mesh.offset, parent.mesh.offset.Z);
            Vector4 row0 = LocalMatrix.Row0;
            //LocalMatrix = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            //LocalMatrix.Row0 = new Vector4((float)Math.Cos(Math.Acos(row0.X) + parent.mesh.Rotation.X), 0, (float)Math.Sin(Math.Asin(row0.Z)+parent.mesh.Rotation.X), (float)Math.Sin(Math.Asin(row0.W)+parent.mesh.Rotation.X));
            parent.mesh.Render(shader, LocalMatrix, wood);
            foreach (Node child in parent.Children)
                RenderChildren(child, LocalMatrix);
        }
    }
}
