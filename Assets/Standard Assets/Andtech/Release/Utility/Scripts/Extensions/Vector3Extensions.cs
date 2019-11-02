using UnityEngine;

namespace Andtech.Extensions {

	public static class Vector3Extensions {

		/// <summary>
		/// Divides the current vector component-wise by another.
		/// </summary>
		/// <param name="a">The divident vector.</param>
		/// <param name="b">The divisor vector</param>
		/// <returns>A vector of component-wise quotients.</returns>
		public static Vector3 DivideBy(this Vector3 a, Vector3 b) {
			return new Vector3() {
				x = a.x / b.x,
				y = a.y / b.y,
				z = a.z / b.z
			};
		}

		public static Vector3 Reciprocal(this Vector3 vector) {
			return new Vector3() {
				x = 1.0F / vector.x,
				y = 1.0F / vector.y,
				z = 1.0F / vector.z
			};
		}

		public static Vector3 Round(this Vector3 vector) {
			return new Vector3() {
				x = Mathf.Round(vector.x),
				y = Mathf.Round(vector.y),
				z = Mathf.Round(vector.z)
			};
		}

		public static Vector3Int RoundToInt(this Vector3 vector) {
			return new Vector3Int() {
				x = Mathf.RoundToInt(vector.x),
				y = Mathf.RoundToInt(vector.y),
				z = Mathf.RoundToInt(vector.z)
			};
		}

		public static Vector3 Floor(this Vector3 vector) {
			return new Vector3() {
				x = Mathf.Floor(vector.x),
				y = Mathf.Floor(vector.y),
				z = Mathf.Floor(vector.z)
			};
		}

		public static Vector3Int FloorToInt(this Vector3 vector) {
			return new Vector3Int() {
				x = Mathf.FloorToInt(vector.x),
				y = Mathf.FloorToInt(vector.y),
				z = Mathf.FloorToInt(vector.z)
			};
		}

		public static Vector3 Ceil(this Vector3 vector) {
			return new Vector3() {
				x = Mathf.Ceil(vector.x),
				y = Mathf.Ceil(vector.y),
				z = Mathf.Ceil(vector.z)
			};
		}

		public static Vector3Int CeilToInt(this Vector3 vector) {
			return new Vector3Int() {
				x = Mathf.CeilToInt(vector.x),
				y = Mathf.CeilToInt(vector.y),
				z = Mathf.CeilToInt(vector.z)
			};
		}

		public static Vector3 Swap(this Vector3 vector) {
			return new Vector3() {
				x = vector.x,
				y = vector.z,
				z = vector.y
			};
		}

		public static void Normalize(this ref Vector3 vector, out float length) {
			length = vector.magnitude;
			vector /= length;
		}
	}
}
