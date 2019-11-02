using Andtech.Bezier;
using System.Collections.Generic;
using UnityEngine;

namespace Andtech.Extrusion {

	/// <summary>
	/// Performs extrusion along a curve.
	/// </summary>
	public static class Extruder {

		/// <summary>
		/// Extrudes the cross section along the curve.
		/// </summary>
		/// <param name="curve">The curve to extrude along.</param>
		/// <param name="crossSection">The cross section of the extrusion.</param>
		/// <returns>A structure containing the result.</returns>
		public static ExtruderResult Extrude(Curve curve, CrossSection2D crossSection, LineTextureMode textureMode = LineTextureMode.Tile) {
			float distanceCounter = 0.0F;
			return Extrude(curve, crossSection, ref distanceCounter, textureMode);
		}

		/// <summary>
		/// Extrudes the cross section along the curve.
		/// </summary>
		/// <param name="curve">The curve to extrude along.</param>
		/// <param name="crossSection">The cross section of the extrusion.</param>
		/// <param name="distanceCounter">A counter for customizing longitudinal texture coordinates.</param>
		/// <returns>A structure containing the result.</returns>
		public static ExtruderResult Extrude(Curve curve, CrossSection2D crossSection, ref float distanceCounter, LineTextureMode textureMode = LineTextureMode.Tile) {
			// Calculate counting parameters
			int sliceCount = curve.Count;
			int segmentCount = sliceCount - 1;
			int vertexCount = crossSection.vertexCount * sliceCount;
			int triangleCount = 2 * crossSection.edgeCount * segmentCount;
			int triangleIndexCount = 3 * triangleCount;

			// Declare storage
			List<Vector3> vertices = new List<Vector3>(vertexCount);
			List<Vector3> normals = new List<Vector3>(vertexCount);
			List<Vector4> tangents = new List<Vector4>(vertexCount);
			List<Vector2> uv = new List<Vector2>(vertexCount);
			List<int> triangles = new List<int>(triangleIndexCount);

			// Compute spatial vectors
			foreach (OrientedPoint orientedPoint in curve) {
				// Add per-slice vertices
				foreach (Vector3 vertex in crossSection.vertices) {
					vertices.Add(orientedPoint.TransformPoint(vertex));
				}
				// Add per-slice normals
				foreach (Vector3 normal in crossSection.normals) {
					normals.Add(orientedPoint.TransformDirection(normal));
				}
				// Add per-slice tangents
				Vector3 tangent = orientedPoint.TransformDirection(Vector3.forward);
				for (int i = 0; i < crossSection.vertexCount; i++) {
					tangents.Add(tangent);
				}
			}

			// Compute texture vectors
			for (int i = 0; i < curve.Count; i++) {
				distanceCounter += curve.deltas[i];

				// Add per-slice uvs
				foreach (Vector2 crossSectionUV in crossSection.uv) {
					float u, v;
					switch (textureMode) {
						case LineTextureMode.Stretch:
							u = curve.distances[i] / curve.Length;
							break;
						case LineTextureMode.DistributePerSegment:
							u = (float)i / curve.Count;
							break;
						case LineTextureMode.RepeatPerSegment:
							u = i;
							break;
						default:
							u = distanceCounter;
							break;
					}

					v = crossSectionUV.y;
					
					Vector2 texCoord = new Vector2(u, v);
					uv.Add(texCoord);
				}
			}

			// Compute triangle indices
			for (int i = 0, baseIndex = 0; i < segmentCount; i++, baseIndex += crossSection.vertexCount) {
				// Add per-segment triangles (triangle indices)
				foreach (Vector2Int edge in crossSection.edges) {
					int a = baseIndex + edge.x;
					int b = baseIndex + edge.y;
					int c = baseIndex + edge.x + crossSection.vertexCount;
					int d = baseIndex + edge.y + crossSection.vertexCount;

					triangles.Add(a);
					triangles.Add(b);
					triangles.Add(d);
					triangles.Add(a);
					triangles.Add(d);
					triangles.Add(c);
				}
			}

			return new ExtruderResult(
				vertices,
				normals,
				tangents,
				uv,
				triangles
			);
		}
	}
}
