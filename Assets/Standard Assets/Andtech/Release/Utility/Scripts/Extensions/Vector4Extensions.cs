using UnityEngine;

namespace Andtech.Extensions {

	public static class Vector4Extensions {

		/// <summary>
		/// Divides the current vector component-wise by another.
		/// </summary>
		/// <param name="a">The divident vector.</param>
		/// <param name="b">The divisor vector</param>
		/// <returns>A vector of component-wise quotients.</returns>
		public static Vector4 DivideBy(this Vector4 a, Vector4 b) {
			return new Vector4() {
				x = a.x / b.x,
				y = a.y / b.y,
				z = a.z / b.z,
				w = a.z / b.z
			};
		}

		public static Vector4 Reciprocal(this Vector4 vector) {
			return new Vector4() {
				x = 1.0F / vector.x,
				y = 1.0F / vector.y,
				z = 1.0F / vector.z,
				w = 1.0F / vector.w
			};
		}

		public static Vector4 Round(this Vector4 vector) {
			return new Vector4() {
				x = Mathf.Round(vector.x),
				y = Mathf.Round(vector.y),
				z = Mathf.Round(vector.z),
				w = Mathf.Round(vector.w)
			};
		}

		public static Vector4 Floor(this Vector4 vector) {
			return new Vector4() {
				x = Mathf.Floor(vector.x),
				y = Mathf.Floor(vector.y),
				z = Mathf.Floor(vector.z)
			};
		}

		public static Vector4 Ceil(this Vector4 vector) {
			return new Vector4() {
				x = Mathf.Ceil(vector.x),
				y = Mathf.Ceil(vector.y),
				z = Mathf.Ceil(vector.z)
			};
		}
	}
}
