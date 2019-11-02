
namespace Andtech.Extrusion {

	/// <summary>
	/// Instructions for the extruder to create an edge between vertices.
	/// </summary>
	public struct Edge {
		/// <summary>
		/// The first vertex.
		/// </summary>
		public readonly int vertexA;
		/// <summary>
		/// The second vertex.
		/// </summary>
		public readonly int vertexB;

		public Edge(int vertexA, int vertexB) {
			this.vertexA = vertexA;
			this.vertexB = vertexB;
		}
	}
}
