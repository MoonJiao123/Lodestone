using System.Collections.Generic;
using UnityEngine;

namespace Andtech.Extrusion {

	/// <summary>
	/// Output structure for extrusion. (see <see cref="Extruder"/>)
	/// </summary>
	public struct ExtruderResult {
		public readonly List<Vector3> vertices;
		public readonly List<Vector3> normals;
		public readonly List<Vector4> tangents;
		public readonly List<Vector2> uv;
		public readonly List<int> triangles;
		public int vertexCount {
			get {
				return vertices.Count;
			}
		}

		public ExtruderResult(List<Vector3> vertices, List<Vector3> normals, List<Vector4> tangents, List<Vector2> uv, List<int> triangles) {
			this.vertices = vertices;
			this.normals = normals;
			this.tangents = tangents;
			this.uv = uv;
			this.triangles = triangles;
		}

		public IEnumerable<Vector3Int> GetTriangles() {
			int i = 0;
			while (i < triangles.Count) {
				int index0 = triangles[i++];
				int index1 = triangles[i++];
				int index2 = triangles[i++];

				yield return new Vector3Int(index0, index1, index2);
			}
		}

		public Mesh GenerateMesh(int uvChannel = 0, int submesh = 0) {
			Mesh mesh = new Mesh();
			mesh.SetVertices(vertices);
			mesh.SetNormals(normals);
			mesh.SetTangents(tangents);
			mesh.SetUVs(uvChannel, uv);
			mesh.SetTriangles(triangles, submesh);

			mesh.RecalculateBounds();

			return mesh;
		}
	}
}
