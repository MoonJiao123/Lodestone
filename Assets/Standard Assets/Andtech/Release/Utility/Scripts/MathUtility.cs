namespace Andtech {

	public static class MathUtility {

		/// <summary>
		/// Returns the greatest common divisor between <paramref name="a"/> and <paramref name="b"/>.
		/// </summary>
		/// <param name="a">The first number.</param>
		/// <param name="b">The second number.</param>
		/// <returns>The greatest common divisor.</returns>
		public static int GCD(int a, int b) {
			return b == 0 ? a : GCD(b, a % b);
		}

		/// <summary>
		/// Returns the greatest common divisor between all values.
		/// </summary>
		/// <param name="values">The set of values.</param>
		/// <returns>The greates common divisor.</returns>
		public static int GCD(params int[] values) {
			int lastGCD = values[0];
			int n = values.Length;
			for (int i = 1; i < n; i++) {
				lastGCD = GCD(lastGCD, values[i]);
			}
			return lastGCD;
		}
	}
}
