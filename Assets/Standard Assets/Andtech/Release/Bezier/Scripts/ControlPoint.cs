using System;
using UnityEngine;

namespace Andtech.Bezier {

	/// <summary>
	/// Represents a standard control point in R3.
	/// </summary>
	[Serializable]
	public class ControlPoint {
		/// <summary>
		/// The position of the control point.
		/// </summary>
		public Vector3 position;
		/// <summary>
		/// The handler vector of the control point.
		/// </summary>
		public Vector3 handler;

		/// <summary>
		/// The normalized Y direction of the control point (using Unity's axis convention).
		/// </summary>
		public Vector3 normal;
		/// <summary>
		/// The normalized Z direction of the control point (using Unity's axis convention).
		/// </summary>
		public Vector3 tangent;
		/// <summary>
		/// The normalized X direction of the control point (using Unity's axis convention).
		/// </summary>
		public Vector3 binormal;

		/// <summary>
		/// The length of the handler.
		/// </summary>
		public float Length {
			get {
				return handler.magnitude;
			}
		}
		/// <summary>
		/// The squared length of the handler.
		/// </summary>
		public float SqrLength {
			get {
				return handler.sqrMagnitude;
			}
		}
		/// <summary>
		/// The world-space position of the handler's back tip.
		/// </summary>
		public Vector3 BackHandler {
			get {
				return position - handler;
			}
		}

		/// <summary>
		/// The world-space position of the handler's forward tip.
		/// </summary>
		public Vector3 ForwardHandler {
			get {
				return position + handler;
			}
		}

		private ControlPoint() {}

		/// <summary>
		/// Constructs a control point with an inferred basis.
		/// </summary>
		/// <param name="position">The position of the point.</param>
		/// <param name="handler">The handler of the point.</param>
		public ControlPoint(Vector3 position, Vector3 handler) {
			this.position = position;
			this.handler = handler;

			tangent = handler.normalized;
			Vector3 cross = Vector3.Cross(Vector3.up, tangent);
			normal = Vector3.Cross(tangent, cross);
			binormal = Vector3.Cross(normal, tangent);
		}

		/// <summary>
		/// Constructs a control point with the specified basis.
		/// </summary>
		/// <param name="position">The position of the point.</param>
		/// <param name="handler">The handler of the point.</param>
		/// <param name="up">The "up" basis vector.</param>
		public ControlPoint(Vector3 position, Vector3 handler, Vector3 right, Vector3 up) {
			this.position = position;
			this.handler = handler;

			tangent = handler.normalized;
			normal = up.normalized;
			binormal = right.normalized;
		}

		/// <summary>
		/// Returns a control point group using the specified displacement vector.
		/// </summary>
		/// <param name="displacement">Distance between the origin and terminus.</param>
		/// <param name="controlPointA">The output storage for the first control point.</param>
		/// <param name="controlPointB">The output storage for the seconds control point.</param>
		public static void Line(Vector3 displacement, out ControlPoint controlPointA, out ControlPoint controlPointB) {
			Line(Vector3.zero, displacement, out controlPointA, out controlPointB);
		}

		/// <summary>
		/// Returns a control point group which connects the two points.
		/// </summary>
		/// <param name="from">The origin of the line.</param>
		/// <param name="to">The terminus of the line.</param>
		/// <param name="controlPointA">The output storage for the first control point.</param>
		/// <param name="controlPointB">The output storage for the seconds control point.</param>
		public static void Line(Vector3 origin, Vector3 terminus, out ControlPoint controlPointA, out ControlPoint controlPointB) {
			Vector3 displacement = terminus - origin;

			controlPointA = new ControlPoint(origin, displacement);
			controlPointB = new ControlPoint(terminus, displacement);
		}

		public static Bounds GetVolume(ControlPoint controlPointA, ControlPoint controlPointB) {
			Vector3 min = new Vector3() {
				x = Mathf.Min(
					controlPointA.position.x,
					controlPointA.position.x + controlPointA.handler.x,
					controlPointB.position.x,
					controlPointB.position.x - controlPointB.handler.x
				),
				y = Mathf.Min(
					controlPointA.position.y,
					controlPointA.position.y + controlPointA.handler.y,
					controlPointB.position.y,
					controlPointB.position.y - controlPointB.handler.y
				),
				z = Mathf.Min(
					controlPointA.position.z,
					controlPointA.position.z + controlPointA.handler.z,
					controlPointB.position.z,
					controlPointB.position.z - controlPointB.handler.z
				)
			};
			Vector3 max = new Vector3() {
				x = Mathf.Max(
				  controlPointA.position.x,
				  controlPointA.position.x + controlPointA.handler.x,
				  controlPointB.position.x,
				  controlPointB.position.x - controlPointB.handler.x
			  ),
				y = Mathf.Max(
				  controlPointA.position.y,
				  controlPointA.position.y + controlPointA.handler.y,
				  controlPointB.position.y,
				  controlPointB.position.y - controlPointB.handler.y
			  ),
				z = Mathf.Max(
				  controlPointA.position.z,
				  controlPointA.position.z + controlPointA.handler.z,
				  controlPointB.position.z,
				  controlPointB.position.z - controlPointB.handler.z
			  )
			};

			Bounds bounds = new Bounds();
			bounds.SetMinMax(min, max);

			return bounds;
		}

		#region OPERATOR
		/// <summary>
		/// Translates the control point by <paramref name="offset"/> units.
		/// </summary>
		/// <param name="controlPoint">The control point to translate.</param>
		/// <param name="offset">The offset vector.</param>
		/// <returns>The translated control point.</returns>
		public static ControlPoint operator +(ControlPoint controlPoint, Vector3 offset) {
			return new ControlPoint() {
				position = controlPoint.position + offset,
				handler = controlPoint.handler,
				normal = controlPoint.normal,
				tangent = controlPoint.tangent,
				binormal = controlPoint.binormal
			};
		}

		/// <summary>
		/// Translates the control point by <paramref name="offset"/> units.
		/// </summary>
		/// <param name="offset">The offset vector.</param>
		/// <param name="controlPoint">The control point to translate.</param>
		/// <returns>The translated control point.</returns>
		public static ControlPoint operator +(Vector3 offset, ControlPoint controlPoint) {
			return controlPoint + offset;
		}

		/// <summary>
		/// Rotates the control point by the <paramref name="quaternion"/> around <see cref="position"/>.
		/// </summary>
		/// <param name="quaternion">The rotation quaternion.</param>
		/// <param name="controlPoint">The control point to rotate.</param>
		/// <returns>The rotated control point.</returns>
		public static ControlPoint operator *(Quaternion quaternion, ControlPoint controlPoint) {
			return new ControlPoint() {
				position = quaternion * controlPoint.position,
				handler = quaternion * controlPoint.handler,
				normal = quaternion * controlPoint.normal,
				tangent = quaternion * controlPoint.tangent,
				binormal = quaternion * controlPoint.binormal
			};
		}
		#endregion OPERATOR
	}
}
