using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Template_P3
{
	public class MeshGroup
	{
		public Vector3 offset;
		public Vector3 Rotation;
		public Vector3 scale;
		public Vector3 rotVelocity;
		public Vector3 posVelocity;
		public bool specular = false;

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
			meshes.Capacity = meshes.Count;
		}

		MeshGroup(MeshGroup mg)
		{
			offset = mg.offset;// +parent.mesh.offset;
			this.Rotation = mg.Rotation;
			this.scale = mg.scale;

			//physics implementation
			rotVelocity = mg.rotVelocity;
			posVelocity = mg.posVelocity;

			meshes = mg.meshes;
		}

		public void AddMesh(Mesh mesh)
		{
			meshes.Add(mesh);
		}

		public void Render(Shader shader, Matrix4 transform, Texture texture = null)
		{
			for (int i = 0; i < meshes.Count; i++)
			{
				meshes[i].Render(shader, transform, meshes[i].texture, specular);
			}
		}

		public MeshGroup Copy()
		{
			return new MeshGroup(this);
		}
	}
}
