using Andtech.Extensions;
using UnityEngine;

namespace Andtech {

	/// <summary>
	/// A coordinate system which supports position snapping.
	/// </summary>
	public class SnapGrid {
		public readonly Vector3 atomicScale;

		private Matrix4x4 prefix;
		private Matrix4x4 suffix;

		/// <summary>
		/// Constructs a snapping grid.
		/// </summary>
		/// <param name="atomicScale">The smallest possible cell size.</param>
		/// <param name="snapSize">The snapping size (worldspace).</param>
		/// <param name="snapOffset">The snapping offset (worldspace)</param>
		public SnapGrid(Vector3 atomicScale, Vector3 snapSize, Vector3 snapOffset) {
			this.atomicScale = atomicScale;

			// Remove snapping offset
			Matrix4x4 prefixA = Matrix4x4.Translate(-snapOffset);
			// Normalize to snap coordinate space
			Matrix4x4 prefixB = Matrix4x4.Scale(snapSize.Reciprocal());
			prefix = prefixB * prefixA;

			// Denormalize from snap coordinate space
			Matrix4x4 suffixA = Matrix4x4.Scale(snapSize);
			// Reapply snapping offset
			Matrix4x4 suffixB = Matrix4x4.Translate(snapOffset);
			// Denormalize from atomic coordinate space
			Matrix4x4 suffixC = Matrix4x4.Scale(atomicScale.Reciprocal());
			suffix = suffixC * suffixB * suffixA;
		}

		/// <summary>
		/// Converts from worldspace coordinates to gridspace coordinates. The current snap settings are used.
		/// </summary>
		/// <param name="vector">The worldspace coordinates to snap.</param>
		/// <returns>A count vector (number of <see cref="atomicScale"/> vectors required to reconstruct <paramref name="vector"/>).</returns>
		public Vector3 Quantize(Vector3 vector) {
			Vector3 pre = prefix.MultiplyPoint3x4(vector);
			Vector3 snapped = pre.Round();
			Vector3 post = suffix.MultiplyPoint3x4(snapped);

			return post;
		}

		/// <summary>
		/// Converts from gridspace coordinates to worldspace coordinates.
		/// </summary>
		/// <param name="vector">The vector (in gridspace coordinates) to construct.</param>
		/// <returns>The equivalent coordinates in worldspace.</returns>
		public Vector3 Dequantize(Vector3 vector) {
			// Denormalize (atomic normalization) coordinate space
			return Vector3.Scale(vector, atomicScale);
		}
	}
}
