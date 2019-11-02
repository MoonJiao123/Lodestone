using System.Collections.Generic;
using UnityEngine;

namespace Andtech.Bezier {

	internal struct CalculusHelper {
		public readonly Vector3 A;
		public readonly Vector3 B;
		public readonly Vector3 C;
		public readonly Vector3 D;

		public CalculusHelper(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
			A = -a + 3.0F * b + -3.0F * c + d;
			B = 3.0F * a + -6.0F * b + 3.0F * c;
			C = -3.0F * a + 3.0F * b;
			D = a;
		}

		public Vector3 CalculatePosition(float t) {
			return A * t * t * t + B * t * t + C * t + D;
		}

		public Vector3 CalculateVelocity(float t) {
			return 3.0F * A * t * t + 2.0F * B * t + C;
		}

		public Vector3 CalculateAcceleration(float t) {
			return 6.0F * A * t + 2.0F * B;
		}

		public Vector3 CalculateTangent(float t) {
			Vector3 velocity = CalculateVelocity(t);

			return velocity;
		}

		public Vector3 CalculateNormal(float t) {
			Vector3 velocity = CalculateVelocity(t);
			float speed = velocity.magnitude;
			Vector3 acceleration = CalculateAcceleration(t);

			Vector3 numerator = acceleration * speed - velocity / speed * Vector3.Dot(velocity, acceleration);
			float denominator = 1.0F / (speed * speed);

			return numerator * denominator;
		}

		public Vector3 CalculateRadius(float t) {
			Vector3 v = CalculateVelocity(t);
			Vector3 a = CalculateAcceleration(t);

			Vector3 cross = Vector3.Cross(v, a);
			float numerator = cross.magnitude;
			float slope = v.magnitude;
			float denominator = slope * slope * slope;
			float curvature = numerator / denominator;
			float radius = 1.0F / curvature;

			return radius * CalculateNormal(t).normalized;
		}
	}

	/// <summary>
	/// Performs cubic bezier interpolation.
	/// </summary>
	public static class CubicBezier {
		/// <summary>
		/// The default total number of points during interpolation.
		/// </summary>
		public static int defaultPointCount = 8;

		/// <summary>
		/// Returns a curve which is a line connecting two points.
		/// </summary>
		/// <param name="from">The origin of the line.</param>
		/// <param name="to">The terminus of the line.</param>
		/// <returns>A curve connecting the origin and terminus.</returns>
		public static Curve Line(Vector3 from, Vector3 to) {
			// Explicit computation of rotation
			Quaternion rotation = Quaternion.LookRotation(to - from);

			// Explicit computation of oriented points
			List<OrientedPoint> orientedPoints = new List<OrientedPoint>() {
				new OrientedPoint(from, rotation),
				new OrientedPoint(to, rotation)
			};

			return new Curve(orientedPoints);
		}

		/// <summary>
		/// Performs Bezier interpolation in R3 with the default point count.
		/// </summary>
		/// <param name="controlPointA">The first control point to use.</param>
		/// <param name="controlPointB">The second control point to use.</param>
		/// <returns>The computed Bezier curve.</returns>
		public static Curve Evaluate(ControlPoint controlPointA, ControlPoint controlPointB, bool use2DMode = false) {
			return Evaluate(controlPointA, controlPointB, defaultPointCount, use2DMode);
		}

		/// <summary>
		/// Performs Bezier interpolation in R3 with the specified point count.
		/// </summary>
		/// <param name="controlPointA">The first control point to use.</param>
		/// <param name="controlPointB">The second control point to use.</param>
		/// <param name="pointCount">The desired point count.</param>
		/// <returns>The computed Bezier curve.</returns>
		public static Curve Evaluate(ControlPoint controlPointA, ControlPoint controlPointB, int pointCount, bool use2DMode = false) {
			// Precompute t-parameter increment
			float t = 0.0F;
			float increment = 1.0F / (pointCount - 1);

			// Helper variables
			Vector3 a = controlPointA.position;
			Vector3 b = controlPointA.ForwardHandler;
			Vector3 c = controlPointB.BackHandler;
			Vector3 d = controlPointB.position;
			CalculusHelper helper = new CalculusHelper(a, b, c, d);

			// Compute each oriented point
			List<OrientedPoint> orientedPoints = new List<OrientedPoint>(pointCount);
			for (int i = 0; i < pointCount; i++, t += increment) {
				// Optimization variables
				float opt = 1.0F - t;
				float optSqr = opt * opt;
				float tSqr = t * t;

				// Compute point vectors
				Vector3 position =
					optSqr * opt * a
					+ 3.0F * optSqr * t * b
					+ 3.0F * opt * tSqr * c
					+ tSqr * t * d;

				Vector3 tangent = helper.CalculateTangent(t);
				Vector3 normal = helper.CalculateNormal(t);
				Vector3 up;
				if (use2DMode)
					up = Vector2.Perpendicular(tangent);
				else
					up = Vector3.Lerp(controlPointA.normal, controlPointB.normal, t);
				Quaternion rotation = Quaternion.LookRotation(tangent, up);

				// Calculate curvature
				Vector3 radius = helper.CalculateRadius(t);

				// Add the oriented point to the list
				OrientedPoint orientedPoint = new OrientedPoint(position, rotation, radius.magnitude);
				orientedPoint.radius = radius;
				orientedPoints.Add(orientedPoint);

				//Debug.DrawRay(position, tangent, Color.cyan);
				//Debug.DrawRay(position, normal, Color.magenta);				
				//Debug.DrawRay(position, orientedPoint.radius, Color.yellow);
				//Debug.DrawRay(position, Vector3.Project(orientedPoint.radius, orientedPoint.TransformDirection(Vector3.right)), Color.red);
				//Debug.DrawRay(position, Vector3.Project(orientedPoint.radius, orientedPoint.TransformDirection(Vector3.up)), Color.green);
				//Debug.DrawRay(position, Vector3.Project(orientedPoint.radius, orientedPoint.TransformDirection(Vector3.forward)), Color.blue);
			}

			return new Curve(orientedPoints);
		}

		/// <summary>
		/// Evaluates the Bezier interpolation in R3 at <paramref name="t">t</paramref>.
		/// </summary>
		/// <param name="controlPointA">The first control point.</param>
		/// <param name="controlPointB">The second control point.</param>
		/// <param name="t">The Bezier position (normalized).</param>
		/// <returns>The position on the curve at the t-value.</returns>
		public static Vector3 GetPoint(ControlPoint controlPointA, ControlPoint controlPointB, float t) {
			float opt = 1.0F - t;
			float optSqr = opt * opt;
			float tSqr = t * t;
			return
				optSqr * opt * controlPointA.position
				+ 3.0F * optSqr * t * controlPointA.ForwardHandler
				+ 3.0F * opt * tSqr * controlPointB.BackHandler
				+ tSqr * t * controlPointB.position;
		}

		/// <summary>
		/// Evaluates the Bezier interpolation in R3 at <paramref name="t">t</paramref>.
		/// </summary>
		/// <param name="controlPointA">The first control point.</param>
		/// <param name="controlPointB">The second control point.</param>
		/// <param name="t">The Bezier position (normalized).</param>
		/// <returns>The position on the curve at the t-value.</returns>
		public static Quaternion GetRotation(ControlPoint controlPointA, ControlPoint controlPointB, float t) {
			// Helper variables
			Vector3 a = controlPointA.position;
			Vector3 b = controlPointA.ForwardHandler;
			Vector3 c = controlPointB.BackHandler;
			Vector3 d = controlPointB.position;
			CalculusHelper helper = new CalculusHelper(a, b, c, d);

			Vector3 tangent = helper.CalculateTangent(t);

			return Quaternion.LookRotation(tangent);
		}
	}
}
