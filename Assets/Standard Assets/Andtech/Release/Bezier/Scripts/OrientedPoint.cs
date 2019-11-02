using UnityEngine;

namespace Andtech.Bezier {

	/// <summary>
	/// Oriented points which define the curve.
	/// SOURCE: "Unite 2015 - A coder's guide to spline-based procedural geometry"
	/// </summary>
	public class OrientedPoint {
		// TODO REMOVE curvature
		/// <summary>
		/// The mathematical curvature at the point
		/// </summary>
		public readonly float curvature;
		public Vector3 radius;
		/// <summary>
		/// The position of the point.
		/// </summary>
		public Vector3 position;
		/// <summary>
		/// The rotation of the point.
		/// </summary>
		public Quaternion rotation;

		public OrientedPoint(Vector3 position, Quaternion rotation) : this(position, rotation, Mathf.Infinity) { }

		public OrientedPoint(Vector3 position, Quaternion rotation, float curvature) {
			this.position = position;
			this.rotation = rotation;
			this.curvature = curvature;
		}

		/// <summary>
		/// Transforms the position vector to match the <see cref="OrientedPoint"/>'s orientation.
		/// </summary>
		/// <param name="point">The position to transform (local space).</param>
		/// <returns>The transformed position vector.</returns>
		public Vector3 TransformPoint(Vector3 point) {
			return position + rotation * point;
		}

		/// <summary>
		/// Transforms the direction vector to match the <see cref="OrientedPoint"/>'s orientation.
		/// </summary>
		/// <param name="direction">The direction to transform (local space).</param>
		/// <returns>The transformed direction vector.</returns>
		public Vector3 TransformDirection(Vector3 direction) {
			return rotation * direction;
		}

		#region OPERATOR
		/// <summary>
		/// Translates the oriented point by <paramref name="offset"/> units.
		/// </summary>
		/// <param name="orientedPoint">The oriented point to translate.</param>
		/// <param name="offset">The offset vector.</param>
		/// <returns>The translated oriented point.</returns>
		public static OrientedPoint operator +(OrientedPoint orientedPoint, Vector3 offset) {
			return new OrientedPoint(orientedPoint.position + offset, orientedPoint.rotation, orientedPoint.curvature);
		}

		/// <summary>
		/// Translates the oriented point by <paramref name="offset"/> units.
		/// </summary>
		/// <param name="offset">The offset vector.</param>
		/// <param name="orientedPoint">The oriented point to translate.</param>
		/// <returns>The translated oriented point.</returns>
		public static OrientedPoint operator +(Vector3 offset, OrientedPoint orientedPoint) {
			return orientedPoint + offset;
		}

		/// <summary>
		/// Translates the oriented point by <paramref name="offset"/> units in the opposite direction.
		/// </summary>
		/// <param name="orientedPoint">The oriented point to translate</param>
		/// <param name="offset">The offset vector.</param>
		/// <returns>The translate oriented point.</returns>
		public static OrientedPoint operator -(OrientedPoint orientedPoint, Vector3 offset) {
			return orientedPoint + -offset;
		}

		/// <summary>
		/// Rotates the control point by the <paramref name="quaternion"/> around <see cref="Vector3.zero"/>.
		/// </summary>
		/// <param name="quaternion">The rotation quaternion.</param>
		/// <param name="controlPoint">The oriented point to rotate.</param>
		/// <returns>The rotated oriented point.</returns>
		public static OrientedPoint operator *(Quaternion rotation, OrientedPoint orientedPoint) {
			return new OrientedPoint(rotation * orientedPoint.position, rotation * orientedPoint.rotation, orientedPoint.curvature);
		}
		#endregion OPERATOR
	}
}
