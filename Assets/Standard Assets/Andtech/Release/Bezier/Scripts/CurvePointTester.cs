using System.Collections.Generic;
using UnityEngine;

namespace Andtech.Bezier {

	/// <summary>
	/// Determines whether points are close enough to a curve.
	/// </summary>
	public struct CurvePointTester {
		public float maxDistance;

		private readonly Curve curve;
		private readonly Vector3[] tangents;
		private int n {
			get {
				return curve.Count;
			}
		}

		public CurvePointTester(Curve curve, float maxDistance) {
			this.curve = curve;
			this.maxDistance = maxDistance;

			int n = curve.Count;

			// Precompute tangents (normalized)
			tangents = new Vector3[n - 1];
			for (int i = 0; i + 1 < n; i++) {
				tangents[i] = curve[i].TransformDirection(Vector3.forward);
			}
		}

		/// <summary>
		/// Tests whether the point is maxDistance units from the curve.
		/// </summary>
		/// <param name="position">The position to test.</param>
		/// <returns>The position is close enough to the curve.</returns>
		public bool TestPoint(Vector3 position) {
			// Helper locals
			float maxDistanceSqr = maxDistance * maxDistance;
			Vector3[] deltas = new Vector3[n];
			for (int i = 0; i < n; i++) {
				deltas[i] = position - curve[i].position;
			}

			// Sphere testing
			if (ContainsLessThanOrEqualTo(deltas, maxDistance))
				return true;

			// Cylinder testing
			for (int i = 0; i + 1 < n; i++) {
				Vector3 delta = deltas[i];
				Vector3 tangent = tangents[i];

				// Ensure vector is within the cylindrical slice
				if (Vector3.Dot(deltas[i], tangent) < 0.0F)
					continue;

				if (Vector3.Dot(deltas[i + 1], -tangent) < 0.0F)
					continue;

				// Ensure the radius is acceptable
				Vector3 orthogonal = VectorUtility.ProjectOnPlaneOptimized(delta, tangent);
				float normalDistanceSqr = orthogonal.sqrMagnitude;
				if (normalDistanceSqr <= maxDistanceSqr)
					return true;
			}

			return false;
		}

		#region PIPELINE
		private static bool ContainsLessThanOrEqualTo(IEnumerable<Vector3> vectors, float maxLength) {
			float maxLengthSqr = maxLength * maxLength;
			foreach (Vector3 vector in vectors) {
				if (vector.sqrMagnitude <= maxLengthSqr)
					return true;
			}

			return false;
		}
		#endregion PIPELINE
	}
}
