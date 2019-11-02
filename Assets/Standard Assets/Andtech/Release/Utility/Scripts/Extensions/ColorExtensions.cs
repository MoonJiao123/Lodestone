using System.Runtime.CompilerServices;
using UnityEngine;

namespace Andtech {

	public static class ColorExtensions {

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		/// <summary>
		/// Set the transparency of the current Color
		/// </summary>
		/// <param name="color">The original color value.</param>
		/// <param name="alpha">The desired alpha (transparency).</param>
		/// <returns>The </returns>
		public static Color Alpha(this Color color, float alpha) {
			color.a = alpha;

			return color;
		}
	}
}
