using Andtech.Extensions;
using UnityEngine;

namespace Andtech {

	public static class RotationExtensions {
		/// <summary>
		/// Direction vector around the X axis. The base direction is up.
		/// </summary>
		public static Vector3Int DirectionX(this Rotation rotation) {
			bool even = (rotation.Turns % 2) == 0;
			Vector3Int forward = (even) ? new Vector3Int(0, 1, 0) : new Vector3Int(0, 1, 1);

			return forward.Rotate(0, rotation.Quadrant, 0);
		}
		/// <summary>
		/// Direction vector around the Y axis. The base direction is forward.
		/// </summary>
		public static Vector3Int DirectionY(this Rotation rotation) {
			bool even = (rotation.Turns % 2) == 0;
			Vector3Int forward = (even) ? new Vector3Int(0, 0, 1) : new Vector3Int(1, 0, 1);

			return forward.Rotate(0, rotation.Quadrant, 0);
		}
		/// <summary>
		/// Direction vector around the Z axis. The base direction is up.
		/// </summary>
		public static Vector3Int DirectionZ(this Rotation rotation) {
			bool even = (rotation.Turns % 2) == 0;
			Vector3Int forward = (even) ? new Vector3Int(0, 1, 0) : new Vector3Int(1, 1, 0);

			return forward.Rotate(0, rotation.Quadrant, 0);
		}
	}
}
