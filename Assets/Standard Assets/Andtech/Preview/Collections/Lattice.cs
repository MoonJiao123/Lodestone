using UnityEngine;

namespace Andtech.Collections {

	/// <summary>
	/// A 3-dimensional indexed data structure.
	/// </summary>
	/// <typeparam name="T">The type of the elements.</typeparam>
	/// <remarks>Each element spans exactly 1 element.</remarks>
	public class Lattice<T> {
		public readonly T[,,] data;
		public virtual T this[int x, int y, int z] {
			get {
				return data[x, y, z];
			}
			set {
				data[x, y, z] = value;
			}
		}
		public virtual T this[Vector3Int position] {
			get {
				return this[position.x, position.y, position.z];
			}
			set {
				this[position.x, position.y, position.z] = value;
			}
		}
		public int Width {
			get {
				return data.GetLength(0);
			}
		}
		public int Height {
			get {
				return data.GetLength(1);
			}
		}
		public int Depth {
			get {
				return data.GetLength(2);
			}
		}

		public Lattice(int width, int height, int depth) {
			data = new T[width, height, depth];
		}

		public bool IsValidCoordinate(Vector3Int position) {
			return IsValidCoordinate(position.x, position.y, position.z);
		}

		public bool IsValidCoordinate(int x, int y, int z) {
			if (x < 0 || x >= Width)
				return false;

			if (y < 0 || y >= Height)
				return false;

			if (z < 0 || z >= Height)
				return false;

			return true;
		}
	}
}
