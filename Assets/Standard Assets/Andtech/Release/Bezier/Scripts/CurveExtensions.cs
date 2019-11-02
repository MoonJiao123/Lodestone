using System.Collections.Generic;

namespace Andtech.Bezier {

	public static class CurveExtensions {

		/// <summary>
		/// Performs a uniform scaling of the points in the curve.
		/// </summary>
		/// <param name="scale">The amount to scale.</param>
		/// <returns>A scaled curve.</returns>
		public static Curve Scale(this Curve curve, float scale) {
			List<OrientedPoint> orientedPoints = new List<OrientedPoint>(curve.Count);
			foreach (OrientedPoint orientedPoint in curve) {
				orientedPoints.Add(new OrientedPoint(orientedPoint.position * scale, orientedPoint.rotation));
			}

			return new Curve(orientedPoints);
		}

		/// <summary>
		/// Retrieves the segment at <paramref name="distance"/>distance.
		/// </summary>
		/// <param name="distance">The distance from the beginning of the curve.</param>
		/// <param name="index">The output index of the lower point.</param>
		/// <returns>The index of the segment.</returns>
		public static bool TryGetSegment(this Curve curve, float distance, out int index) {
			index = curve.distances.BinarySearch(distance);
			if (index < 0)
				index = ~index + 1;

			return index < curve.Count;
		}
	}
}