using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Template_P3
{
    public interface IMesh
	{
		Vector3 offset { get; set; }
		Vector3 Rotation { get; set; }
		Vector3 scale { get; set; }
		Vector3 rotVelocity { get; set; }
		Vector3 posVelocity { get; set; }

		void Render(Shader shader, Matrix4 transform, Texture texture = null);
    }

	public class MeshGroup : IMesh
	{
		public Vector3 offset { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 scale { get; set; }
		public Vector3 rotVelocity { get; set; }
		public Vector3 posVelocity { get; set; }

		List<Mesh> meshes = new List<Mesh>();

		public MeshGroup(string fileName, Vector3 position, Vector3 Rotation, Vector3 scale, Vector3 RotationalVelocity = new Vector3(), Vector3 Velocity = new Vector3())
		{
			offset = position;// +parent.mesh.offset;
			this.Rotation = Rotation;
			this.scale = scale;

			//physics implementation
			rotVelocity = RotationalVelocity;
			posVelocity = Velocity;

			MeshLoader loader = new MeshLoader();
			loader.Load(this, fileName);
		}

		public void AddMesh(Mesh mesh)
		{
			meshes.Add(mesh);
		}

		public void Render(Shader shader, Matrix4 transform, Texture texture = null)
		{
			for (int i = 0; i < meshes.Count; i++)
			{
				meshes[i].Render(shader, transform, meshes[i].texture);
			}
		}
	}
}
