using UnityEngine;

namespace Andtech.Extensions {

	public static class Vector3IntExtensions {

		/// <summary>
		/// Reduces the vector to its minimum equivalent representation.
		/// </summary>
		/// <param name="vector">The vector to reduce.</param>
		/// <returns>The minimum equivalent vector.</returns>
		public static Vector3Int Reduce(this Vector3Int vector) {
			if (vector == Vector3Int.zero)
				return Vector3Int.zero;

			// Calculate reduced vector
			int gcd = MathUtility.GCD(
				Mathf.Abs(vector.x),
				Mathf.Abs(vector.y),
				Mathf.Abs(vector.z)
			);
			Vector3Int reduced = new Vector3Int() {
				x = vector.x / gcd,
				y = vector.y / gcd,
				z = vector.z / gcd
			};

			return reduced;
		}

		/// <summary>
		/// Reduces the vector to its minimum equivalent representation.
		/// </summary>
		/// <param name="vector">The vector to reduce.</param>
		/// <param name="count">The multiplier required to reconstruct the original vector.</param>
		/// <returns>The minimum equivalent vector.</returns>
		public static Vector3Int Reduce(this Vector3Int vector, out int scale) {
			if (vector == Vector3Int.zero) {
				scale = 0;
				return Vector3Int.zero;
			}

			// Calculate reduced vector
			Vector3Int reduced = Reduce(vector);

			// Calculate scale
			long sqrScale = vector.sqrMagnitude / reduced.sqrMagnitude;
			scale = Mathf.RoundToInt(Mathf.Sqrt(sqrScale));

			return reduced;
		}

		/// <summary>
		/// Dot product of two vectors.
		/// </summary>
		/// <param name="lhs">Left-hand side.</param>
		/// <param name="rhs">Right-hand side.</param>
		/// <returns>The dot product.</returns>
		public static int Dot(Vector3Int lhs, Vector3Int rhs) {
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		/// <summary>
		/// Returns a copy of the vector which points in the opposite direction.
		/// </summary>
		/// <param name="vector">The vector to reverse.</param>
		/// <returns>The reversed vector.</returns>
		public static Vector3Int Reverse(this Vector3Int vector) {
			return new Vector3Int() {
				x = -vector.x,
				y = -vector.y,
				z = -vector.z
			};
		}

		/// <summary>
		/// Applies a rotation of
		/// <paramref name="zTurns"/> around the z axis,
		/// <paramref name="xTurns"/> around the x axis, and
		/// <paramref name="yTurns"/> around the y axis (in that order).
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="turns">A vector of per-axis 90 degree clockwise turns.</param>
		/// <returns>The rotated vector.</returns>
		public static Vector3Int Rotate(this Vector3Int vector, Vector3Int turns) {
			return vector.Rotate(turns.x, turns.y, turns.z);
		}

		/// <summary>
		/// Applies a rotation of
		/// <paramref name="zTurns"/> around the z axis,
		/// <paramref name="xTurns"/> around the x axis, and
		/// <paramref name="yTurns"/> around the y axis (in that order).
		/// </summary>
		/// <param name="vector">The vector to rotate</param>
		/// <param name="xTurns">90 degree turns clockwise around the z axis.</param>
		/// <param name="yTurns">90 degree turns clockwise around the x axis.</param>
		/// <param name="zTurns">90 degree turns clockwise around the y axis.</param>
		/// <returns>The rotated vector.</returns>
		public static Vector3Int Rotate(this Vector3Int vector, int xTurns, int yTurns, int zTurns) {
			xTurns = xTurns.Repeat(0, 3);
			yTurns = yTurns.Repeat(0, 3);
			zTurns = zTurns.Repeat(0, 3);

			Vector3Int temp;

			temp = vector;
			switch (zTurns) {
				case 1:
					vector.y = temp.x;
					vector.x = -temp.y;
					break;
				case 2:
					vector.y = -temp.y;
					vector.x = -temp.x;
					break;
				case 3:
					vector.y = -temp.x;
					vector.x = temp.y;
					break;
			}

			temp = vector;
			switch (xTurns) {
				case 1:
					vector.z = temp.y;
					vector.y = -temp.z;
					break;
				case 2:
					vector.z = -temp.z;
					vector.y = -temp.y;
					break;
				case 3:
					vector.z = -temp.y;
					vector.y = temp.z;
					break;
			}

			temp = vector;
			switch (yTurns) {
				case 1:
					vector.x = temp.z;
					vector.z = -temp.x;
					break;
				case 2:
					vector.x = -temp.x;
					vector.z = -temp.z;
					break;
				case 3:
					vector.x = -temp.z;
					vector.z = temp.x;
					break;
			}

			return vector;
		}

		public static Vector2Int To2D(this Vector3Int vector) {
			return new Vector2Int() {
				x = vector.x,
				y = vector.y
			};
		}

		public static Vector3Int Swap(this Vector3Int vector) {
			return new Vector3Int() {
				x = vector.x,
				y = vector.z,
				z = vector.y
			};
		}
	}
}
