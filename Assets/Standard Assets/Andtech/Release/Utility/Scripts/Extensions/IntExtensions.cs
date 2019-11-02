
namespace Andtech.Extensions {

	public static class IntExtensions {

		/// <summary>
		/// Repeats the value between <paramref name="min"/> (inclusive) and <paramref name="max"/> (inclusive).
		/// </summary>
		/// <param name="x">The value to repeat.</param>
		/// <param name="min">The minimum value for the result.</param>
		/// <param name="max">The maximum value for the result.</param>
		/// <returns>The repeated value.</returns>
		public static int Repeat(this int x, int min, int max) {
			int rangePlusOne = (max - min) + 1;

			if (x < min) {
				int distance = max - x;
				return max - (distance % rangePlusOne);
			}
			else {
				int distance = x - min;
				return (distance % rangePlusOne) + (min);
			}
		}

		/// <summary>
		/// Repeats the value between <paramref name="min"/> (inclusive) and <paramref name="max"/> (inclusive).
		/// </summary>
		/// <param name="x">The value to repeat.</param>
		/// <param name="min">The minimum value for the result.</param>
		/// <param name="max">The maximum value for the result.</param>
		/// <param name="repeatCounts">The required number of times the value would need to repeat.</param>
		/// <returns>The repeated value.</returns>
		public static int Repeat(this int x, int min, int max, out int repeatCounts) {
			int rangePlusOne = (max - min) + 1;

			if (x < min) {
				int distance = max - x;
				repeatCounts = distance / rangePlusOne;
				return max - (distance % rangePlusOne);
			}
			else {
				int distance = x - min;
				repeatCounts = distance / rangePlusOne;
				return (distance % rangePlusOne) + (min);
			}
		}
	}
}
