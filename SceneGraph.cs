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
		const float PI = 3.1415926535f;

		// create shaders
		Shader shader = new Shader("../../shaders/vs.glsl", "../../shaders/fs.glsl");
        // load a texture
        Texture wood = new Texture("../../assets/wood.jpg");

		float a = 0;
        Stopwatch timer = new Stopwatch();

        public void Init()
		{
			Node Child = new Node(new Mesh("../../assets/floor.obj", new Vector3(0, 0, 0), new Vector3(0, PI, 0), new Vector3(1,1,1)));
			Node Child2 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(-2, -1.9f, 0), new Vector3(0, 0, 0), new Vector3(1,2,1)));
			Child.AddChild(Child2);
			Scene.AddChild(Child);
			Child = new Node(new Mesh("../../assets/teapot.obj", new Vector3(20, 0, 0), new Vector3(0, 0, 0), new Vector3(1,1,1)));
			Child2 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(0, 0, 0), new Vector3(0, 0, .5f* PI), new Vector3(1,1,1)));
			Child.AddChild(Child2);
			Scene.AddChild(Child);
		}

        public void Render(Matrix4 CameraMatrix)
        {
            Game.target.Bind();
			foreach (Node child in Scene.Children)
                RenderChildren(child, CameraMatrix);
            Game.target.Unbind();
            Game.quad.Render(Game.postproc, Game.target.GetTextureID());
        }

        public void RenderChildren(Node parent, Matrix4 LocalMatrix)
		{
			Matrix4 parentMatrix = Matrix4.Zero;
			parentMatrix.Diagonal = new Vector4(1, 1, 1, 1);
			parentMatrix.Row3 = new Vector4(parent.mesh.offset, 1);
			//LocalMatrix *= parentMatrix;
			LocalMatrix *= Matrix4.CreateTranslation(parent.mesh.offset);
			LocalMatrix *= Matrix4.CreateScale(parent.mesh.scale);
			LocalMatrix *= Matrix4.CreateRotationX(parent.mesh.Rotation.X);
			LocalMatrix *= Matrix4.CreateRotationY(parent.mesh.Rotation.Y);
			LocalMatrix *= Matrix4.CreateRotationZ(parent.mesh.Rotation.Z);
			Console.WriteLine("Matrix: " + LocalMatrix);
			Vector4 row0 = LocalMatrix.Row0;
            //LocalMatrix = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            //LocalMatrix.Row0 = new Vector4((float)Math.Cos(Math.Acos(row0.X) + parent.mesh.Rotation.X), 0, (float)Math.Sin(Math.Asin(row0.Z)+parent.mesh.Rotation.X), (float)Math.Sin(Math.Asin(row0.W)+parent.mesh.Rotation.X));

            parent.mesh.Render(shader, LocalMatrix, wood);
            // render quad

            foreach (Node child in parent.Children)
                RenderChildren(child, LocalMatrix);
        }
    }
}
