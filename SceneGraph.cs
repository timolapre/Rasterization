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
        Stopwatch timer = new Stopwatch();
		
        List<Node> childlist = new List<Node>();
		public void Init()
		{
			Node Child = new Node(null);
			Node Child3 = new Node(new MeshGroup("../../assets/teapot.obj", new Vector3(7.5f, 6, 0), new Vector3(0, PI, 0), new Vector3(1f, 1, 1), new Vector3(0, .01f, 0)));
			for (int i = 0; i < 10; i++)
            {
                Node Child2 = Child3;
                Child3 = new Node(Child3.mesh.Copy());
                if (i == 0)
                    Child = Child3;
                Child2.AddChild(Child3);
                childlist.Add(Child3);
                
            }
			Scene.AddChild(Child);

			//Track
			Node newChild = new Node(new MeshGroup("../../assets/AC/Animal Crossing - Summer.obj", new Vector3(0, -20, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
            Scene.AddChild(newChild);

			//Kart
			newChild = new Node(new MeshGroup("../../assets/Kart/Standard Kart.obj", new Vector3(-20, 1, -17), new Vector3(0, .5f*PI, 0), new Vector3(.5f, .5f, .5f)));
			Child = new Node(new MeshGroup("../../assets/Kart/Leaf Tire.obj", new Vector3(-.2f, -.15f, .36f), new Vector3(0, 0, 0), new Vector3(8,8,8)));
			newChild.AddChild(Child);
			Child = new Node(Child.mesh.Copy());
			Child.mesh.offset.X = .2f;
			Child.mesh.Rotation.Y = PI;
			newChild.AddChild(Child);
			Child = new Node(Child.mesh.Copy());
			Child.mesh.offset.X = .225f;
			Child.mesh.offset.Z = -.18f;
			Child.mesh.offset.Y = -.16f;
			Child.mesh.scale = new Vector3(10, 10, 10);
			newChild.AddChild(Child);
			Child = new Node(Child.mesh.Copy());
			Child.mesh.offset.X = -.225f;
			Child.mesh.Rotation.Y = 0;
			newChild.AddChild(Child);
			Scene.AddChild(newChild);

			/*Child = new Node(new Mesh("../../assets/floor.obj", new Vector3(-10, 0, 0), new Vector3(0, PI, 0), new Vector3(1,1,1)));
			Node Child2 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(-2, -1.9f, 0), new Vector3(0, 0, 0), new Vector3(1,1,1)));
			Child.AddChild(Child2);
			Scene.AddChild(Child);
			Child = new Node(new Mesh("../../assets/floor.obj", new Vector3(20, 0, 0), new Vector3(0, 0, 0), new Vector3(1,1,1)));
			Child2 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(3, -2, 0), new Vector3(0, 0, 0), new Vector3(1,1,2)));
			Child3 = new Node(new Mesh("../../assets/floor.obj", new Vector3(0, 10, 0), new Vector3(0, 0, 0), new Vector3(1, 1, .5f)));
			Node Child4 = new Node(new Mesh("../../assets/teapot.obj", new Vector3(3, -2, 0), new Vector3(0, 0, 0), new Vector3(2, 1, 1)));
			Child3.AddChild(Child4);
			Child2.AddChild(Child3);
			Child.AddChild(Child2);
			Scene.AddChild(Child);*/
		}

        public void Tick()
        {
            foreach (Node child in Scene.Children)
            {
                Physics(child);
                //item.mesh.Rotation.Y += .01f;
            }
            //Child.mesh.Rotation.Y += .01f;
            
        }

        public void Render(Matrix4 CameraMatrix)
        {
            
			//Child3.mesh.Rotation.Y += .05f;
            //Game.target.Bind();
			Matrix4 plainMatrix = new Matrix4();
			plainMatrix.Diagonal = new Vector4(1, 1, 1, 1);
			foreach (Node child in Scene.Children)
                RenderChildren(child, CameraMatrix, plainMatrix, plainMatrix);
            //Game.target.Unbind();
            //Game.quad.Render(Game.postproc, Game.target.GetTextureID());
        }

		public void RenderChildren(Node parent, Matrix4 LocalMatrix, Matrix4 rotMatrix, Matrix4 transMatrix)
		{
			Matrix4 localTrans = Matrix4.CreateTranslation(parent.mesh.offset);
			localTrans *= rotMatrix;
            rotMatrix = Matrix4.CreateRotationX(parent.mesh.Rotation.X);
			rotMatrix *= Matrix4.CreateRotationY(parent.mesh.Rotation.Y);
			rotMatrix *= Matrix4.CreateRotationZ(parent.mesh.Rotation.Z);
			LocalMatrix *= Matrix4.CreateScale(parent.mesh.scale);

			Vector4 row0 = LocalMatrix.Row0;
			//LocalMatrix = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
			//LocalMatrix.Row0 = new Vector4((float)Math.Cos(Math.Acos(row0.X) + parent.mesh.Rotation.X), 0, (float)Math.Sin(Math.Asin(row0.Z)+parent.mesh.Rotation.X), (float)Math.Sin(Math.Asin(row0.W)+parent.mesh.Rotation.X));

			parent.mesh.Render(shader, LocalMatrix * rotMatrix * localTrans * transMatrix ,wood);

            foreach (Node child in parent.Children)
				RenderChildren(child, LocalMatrix, rotMatrix, localTrans*transMatrix);
		}

        public void Physics(Node parent)
        {
            parent.mesh.Rotation += parent.mesh.rotVelocity;
            parent.mesh.offset += parent.mesh.posVelocity;
           
            foreach (Node child in parent.Children)
            {
                Physics(child);
            }
        }
    }
}
