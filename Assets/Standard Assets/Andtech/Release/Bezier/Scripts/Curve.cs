using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andtech.Bezier {

	/// <summary>
	/// Represents the result of a Bezier interpolation in R3.
	/// </summary>
	public class Curve : IEnumerable<OrientedPoint> {
		/// <summary>
		/// The number of points in the curve.
		/// </summary>
		public int Count {
			get {
				return orientedPoints.Count;
			}
		}
		/// <summary>
		/// The world-space position of the first point.
		/// </summary>
		public Vector3 Origin {
			get {
				return First.position;
			}
		}
		/// <summary>
		/// The world-space position of the last point.
		/// </summary>
		public Vector3 Terminus {
			get {
				return Last.position;
			}
		}
		/// <summary>
		/// Operator for retrieving points.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public OrientedPoint this[int index] {
			get {
				return orientedPoints[index];
			}
		}
		/// <summary>
		/// The first point in the curve.
		/// </summary>
		public OrientedPoint First {
			get {
				return this[0];
			}
		}
		/// <summary>
		/// The last point in the curve.
		/// </summary>
		public OrientedPoint Last {
			get {
				return this[Count - 1];
			}
		}
		/// <summary>
		/// The (approximate) arc length of the curve.
		/// </summary>
		public float Length {
			get {
				return distances[Count - 1];
			}
		}
		/// <summary>
		/// The distance between the last position and first position.
		/// </summary>
		public Vector3 Displacement {
			get {
				return Terminus - Origin;
			}
		}

		/// <summary>
		/// Returns the local space version of the curve.
		/// </summary>
		public Curve localized {
			get {
				return this - this[0].position;
			}
		}

		/// <summary>
		/// The set of <see cref="OrientedPoint"/>s along the curve.
		/// </summary>
		public readonly List<OrientedPoint> orientedPoints;
		/// <summary>
		/// The set of distances between points.
		/// </summary>
		/// <remarks>The <i>i</i>th element represents the distance between the <i>(i - 1)</i>th point and the <i>i</i>th point.</remarks>
		public readonly List<float> deltas;
		/// <summary>
		/// The set of cumulative distances for each point.
		/// </summary>
		/// <remarks>The <i>i</i>th element represents the distance between the first and the <i>i</i>th point.</remarks>
		public readonly List<float> distances;
	
		public Curve(List<OrientedPoint> orientedPoints) {
			int n = orientedPoints.Count;
			this.orientedPoints = orientedPoints;

			deltas = new List<float>(n) {
				0.0F
			};
			distances = new List<float>(n) {
				0.0F
			};
			for (int i = 0; i < n - 1; i++) {
				float delta = Vector3.Distance(orientedPoints[i].position, orientedPoints[i + 1].position);
				deltas.Insert(i + 1, delta);
				distances.Insert(i + 1, distances[i] + delta);
			}
		}


		#region OPERATOR
		/// <summary>
		/// Translates the curve by <paramref name="offset"/> units.
		/// </summary>
		/// <param name="curve">The curve to translate.</param>
		/// <param name="offset">The offset vector.</param>
		/// <returns>The translated curve.</returns>
		public static Curve operator +(Curve curve, Vector3 offset) {
			List<OrientedPoint> orientedPoints = new List<OrientedPoint>(curve.Count);
			for (int i = 0; i < curve.Count; i++) {
				orientedPoints.Add(curve[i] + offset);
			}

			return new Curve(orientedPoints);
		}

		/// <summary>
		/// Translates the curve by <paramref name="offset"/> units.
		/// </summary>
		/// <param name="offset">The offset vector.</param>
		/// <param name="curve">The curve to translate.</param>
		/// <returns>The translated curve.</returns>
		public static Curve operator +(Vector3 offset, Curve curve) {
			return curve + offset;
		}

		/// <summary>
		/// Translates the curve by <paramref name="offset"/> units in the opposite direction.
		/// </summary>
		/// <param name="curve">The curve to translate.</param>
		/// <param name="offset">The offset vector.</param>
		/// <returns>The translated curve.</returns>
		public static Curve operator -(Curve curve, Vector3 offset) {
			return curve + -offset;
		}

		public static Curve operator *(Quaternion rotation, Curve curve) {
			List<OrientedPoint> orientedPoints = new List<OrientedPoint>(curve.Count);
			for (int i = 0; i < curve.Count; i++) {
				orientedPoints.Add(rotation * curve[i]);
			}
			return new Curve(orientedPoints);
		}
		#endregion OPERATOR

		#region INTERFACE
		public IEnumerator<OrientedPoint> GetEnumerator() {
			return orientedPoints.GetEnumerator() as IEnumerator<OrientedPoint>;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		#endregion INTERFACE
	}
}
