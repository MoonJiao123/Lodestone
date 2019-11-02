using UnityEngine;

namespace Andtech.Extensions {

	public static class Vector2Extensions {

		/// <summary>
		/// Divides the current vector component-wise by another.
		/// </summary>
		/// <param name="a">The divident vector.</param>
		/// <param name="b">The divisor vector</param>
		/// <returns>A vector of component-wise quotients.</returns>
		public static Vector2 DivideBy(this Vector2 a, Vector2 b) {
			return new Vector2() {
				x = a.x / b.x,
				y = a.y / b.y
			};
		}

		public static Vector2 Reciprocal(this Vector2 vector) {
			return new Vector2() {
				x = 1.0F / vector.x,
				y = 1.0F / vector.y
			};
		}

		public static Vector2 Round(this Vector2 vector) {
			return new Vector2() {
				x = Mathf.Round(vector.x),
				y = Mathf.Round(vector.y)
			};
		}

		public static Vector2Int RoundToInt(this Vector2 vector) {
			return new Vector2Int() {
				x = Mathf.RoundToInt(vector.x),
				y = Mathf.RoundToInt(vector.y)
			};
		}

		public static Vector2 Floor(this Vector2 vector) {
			return new Vector2() {
				x = Mathf.Floor(vector.x),
				y = Mathf.Floor(vector.y)
			};
		}

		public static Vector2Int FloorToInt(this Vector2 vector) {
			return new Vector2Int() {
				x = Mathf.FloorToInt(vector.x),
				y = Mathf.FloorToInt(vector.y)
			};
		}

		public static Vector2 Ceil(this Vector2 vector) {
			return new Vector2() {
				x = Mathf.Ceil(vector.x),
				y = Mathf.Ceil(vector.y)
			};
		}

		public static Vector2Int CeilToInt(this Vector2 vector) {
			return new Vector2Int() {
				x = Mathf.CeilToInt(vector.x),
				y = Mathf.CeilToInt(vector.y)
			};
		}

		public static void Normalize(this ref Vector2 vector, out float length) {
			length = vector.magnitude;
			vector /= length;
		}
	}
}
