using UnityEngine;

namespace Andtech.Extrusion {

	/// <summary>
	/// Represents a 2D slice which can be used for extrusion.
	/// </summary>
	[CreateAssetMenu(fileName = "Cross Section", menuName = "Custom/Cross Section", order = 999)]
	public class CrossSection2D : ScriptableObject {
		/// <summary>
		/// The vertex positions.
		/// </summary>
		public Vector2[] vertices;
		/// <summary>
		/// The normal directions per each vertex.
		/// </summary>
		public Vector2[] normals;
		/// <summary>
		/// The texture coordinates of each vertex.
		/// </summary>
		public Vector2[] uv;
		/// <summary>
		/// The set of connection pairs between vertices.
		/// </summary>
		public Vector2Int[] edges;
		/// <summary>
		/// The number of vertices.
		/// </summary>
		public int vertexCount => vertices.Length;
		/// <summary>
		/// The number of connection pairs.
		/// </summary>
		public int edgeCount => edges.Length;

		public CrossSection2D(Vector2[] vertices, Vector2[] normals, Vector2[] uv, Vector2Int[] edges) {
			this.vertices = vertices;
			this.normals = normals;
			this.uv = uv;
			this.edges = edges;
		}
	}
}
