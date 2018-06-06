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

        Matrix4 rotation = new Matrix4(new Vector4(1, 0, 0, 0), new Vector4(0, (float)Math.Cos(4), 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 1));
        Matrix4 translation = new Matrix4(new Vector4(0,0,0,0), new Vector4(0,0,0,0), new Vector4(0,0,0,0), new Vector4(5,0,0,0));

        public void Init()
        {
            Hierarchy Child = new Hierarchy(new Mesh("../../assets/teapot.obj", new Vector3(0, 0, 0), new Vector3(.5f, 0, 0)));
            //Child.AddChild(new Hierarchy(new Mesh("../../assets/floor.obj", new Vector3(0,0,0), new Vector3(0, 0, 0),Child)));
            Child.AddChild(new Hierarchy(new Mesh("../../assets/teapot.obj", new Vector3(15, 0, 0), new Vector3(0, 0, 0))));
            Child.AddChild(new Hierarchy(new Mesh("../../assets/teapot.obj", new Vector3(-15, 0, 20), new Vector3(0, 0, 0))));
            Child.AddChild(new Hierarchy(new Mesh("../../assets/teapot.obj", new Vector3(-15, -10, 5), new Vector3(0, 0, 0))));
            Child.AddChild(new Hierarchy(new Mesh("../../assets/teapot.obj", new Vector3(0, 20, 0), new Vector3(0, 0, 0))));
            Scene.AddChild(Child);
        }

        public void Render(Matrix4 CameraMatrix)
        {
            foreach(Hierarchy child in Scene.Children)
                RenderChildren(child, CameraMatrix);
        }

        public void RenderChildren(Hierarchy parent, Matrix4 LocalMatrix)
        {
            LocalMatrix.Row3 += new Vector4(parent.mesh.offset.X, parent.mesh.offset.Y, parent.mesh.offset.Z, parent.mesh.offset.Z);
            Vector4 row0 = LocalMatrix.Row0;
            //LocalMatrix = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            //LocalMatrix.Row0 = new Vector4((float)Math.Cos(Math.Acos(row0.X) + parent.mesh.Rotation.X), 0, (float)Math.Sin(Math.Asin(row0.Z)+parent.mesh.Rotation.X), (float)Math.Sin(Math.Asin(row0.W)+parent.mesh.Rotation.X));
            parent.mesh.Render(shader, LocalMatrix, wood);
            foreach (Hierarchy child in parent.Children)
                RenderChildren(child, LocalMatrix);
        }
        
        public void Add(Mesh mesh)
        {
            MeshList.Add(mesh);
        }
    }
}
