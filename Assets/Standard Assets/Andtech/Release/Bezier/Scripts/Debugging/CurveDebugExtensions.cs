using UnityEngine;

namespace Andtech.Bezier.Debugging {

	public static class CurveDebugExtensions {

		/// <summary>
		/// Draws the curve on the screen.
		/// </summary>
		/// <param name="curve">The curve to draw.</param>
		public static void Draw(this Curve curve) {
			Draw(curve, Color.black, Color.white);
		}

		/// <summary>
		/// Draws the curve on the screen.
		/// </summary>
		/// <param name="curve">The curve to draw.</param>
		/// <param name="startColor">The color of the beginning of the curve.</param>
		/// <param name="endColor">The color of the end of the curve.</param>
		public static void Draw(this Curve curve, Color startColor, Color endColor) {
			for (int i = 0; i < curve.Count - 1; i++) {
				float alpha = (float)i / (curve.Count - 1);

				Color color = Color.Lerp(startColor, endColor, alpha);
				Debug.DrawLine(curve[i].position, curve[i + 1].position, color);
			}
		}

		/// <summary>
		/// Draws the curve on the screen.
		/// </summary>
		/// <param name="curve">The curve to draw.</param>
		/// <param name="startColor">The color of the beginning of the curve.</param>
		/// <param name="endColor">The color of the end of the curve.</param>
		/// <param name="duration">How long the lines should be visible for.</param>
		public static void Draw(this Curve curve, Color startColor, Color endColor, float duration) {
			for (int i = 0; i < curve.Count - 1; i++) {
				float alpha = (float)i / (curve.Count - 1);

				Color color = Color.Lerp(startColor, endColor, alpha);
				Debug.DrawLine(curve[i].position, curve[i + 1].position, color, duration);
			}
		}

		/// <summary>
		/// Draws a basis gizmos at each point.
		/// </summary>
		/// <param name="curve">The curve to draw.</param>
		public static void DrawBasisGizmos(this Curve curve) {
			foreach (OrientedPoint orientedPoint in curve) {
				Debug.DrawRay(orientedPoint.position, orientedPoint.TransformDirection(Vector3.right), Color.red);
				Debug.DrawRay(orientedPoint.position, orientedPoint.TransformDirection(Vector3.up), Color.green);
				Debug.DrawRay(orientedPoint.position, orientedPoint.TransformDirection(Vector3.forward), Color.blue);
			}
		}

		/// <summary>
		/// Draws a basis gizmos at each point.
		/// </summary>
		/// <param name="curve">The curve to draw.</param>
		/// <param name="duration">How long the gizmo should be visible for.</param>
		public static void DrawBasisGizmos(this Curve curve, float duration) {
			foreach (OrientedPoint orientedPoint in curve) {
				Debug.DrawRay(orientedPoint.position, orientedPoint.TransformDirection(Vector3.right), Color.red, duration);
				Debug.DrawRay(orientedPoint.position, orientedPoint.TransformDirection(Vector3.up), Color.green, duration);
				Debug.DrawRay(orientedPoint.position, orientedPoint.TransformDirection(Vector3.forward), Color.blue, duration);
			}
		}

		/// <summary>
		/// Draws the control point on the screen.
		/// </summary>
		/// <param name="controlPoint">The control point to draw.</param>
		/// <param name="drawBackHandler">Should the back handler also be drawn?</param>
		public static void Draw(this ControlPoint controlPoint, bool drawBackHandler = false) {
			Draw(controlPoint, Color.black, Color.white, drawBackHandler);
		}

		/// <summary>
		/// Draws the control point on the screen.
		/// </summary>
		/// <param name="controlPoint">The control point to draw.</param>
		/// <param name="startColor">The color of the beginning of the curve.</param>
		/// <param name="endColor">The color of the end of the curve.</param>
		/// <param name="drawBackHandler">Should the back handler also be drawn?</param>
		public static void Draw(this ControlPoint controlPoint, Color startColor, Color endColor, bool drawBackHandler = false) {
			int count = 8;

			for (int i = (drawBackHandler) ? -count : 0; i < count; i++) {
				float s = (float)i / count;
				float t = (float)(i + 1) / count;
				Color color = Color.Lerp(startColor, endColor, s);

				Debug.DrawLine(controlPoint.position + s * controlPoint.handler, controlPoint.position + t * controlPoint.handler, color);
			}
		}

		/// <summary>
		/// Draws the control point on the screen.
		/// </summary>
		/// <param name="controlPoint">The control point to draw.</param>
		/// <param name="startColor">The color of the beginning of the curve.</param>
		/// <param name="endColor">The color of the end of the curve.</param>
		/// <param name="duration">How long the lines should be visible for.</param>
		/// <param name="drawBackHandler">Should the back handler also be drawn?</param>
		public static void Draw(this ControlPoint controlPoint, Color startColor, Color endColor, float duration, bool drawBackHandler = false) {
			int count = 8;

			for (int i = (drawBackHandler) ? -count : 0; i < count; i++) {
				float s = (float)i / count;
				float t = (float)(i + 1) / count;
				Color color = Color.Lerp(startColor, endColor, s);

				Debug.DrawLine(controlPoint.position + s * controlPoint.handler, controlPoint.position + t * controlPoint.handler, color, duration);
			}
		}

		/// <summary>
		/// Draws a basis gizmo on the screen.
		/// </summary>
		/// <param name="controlPoint">The control point to draw.</param>
		public static void DrawBasisGizmos(this ControlPoint controlPoint) {
			Debug.DrawRay(controlPoint.position, controlPoint.binormal, Color.red);
			Debug.DrawRay(controlPoint.position, controlPoint.normal, Color.green);
			Debug.DrawRay(controlPoint.position, controlPoint.tangent, Color.blue);
		}

		/// <summary>
		/// Draws a basis gizmo on the screen.
		/// </summary>
		/// <param name="controlPoint">The control point to draw.</param>
		/// <param name="duration">How long the gizmo should be visible for.</param>
		public static void DrawBasisGizmos(this ControlPoint controlPoint, float duration) {
			Debug.DrawRay(controlPoint.position, controlPoint.binormal, Color.red, duration);
			Debug.DrawRay(controlPoint.position, controlPoint.normal, Color.green, duration);
			Debug.DrawRay(controlPoint.position, controlPoint.tangent, Color.blue, duration);
		}
	}
}