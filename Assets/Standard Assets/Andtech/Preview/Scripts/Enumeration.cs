using System.Collections.Generic;

namespace Andtech {

	public static class Enumeration {

		public static IEnumerable<int> Range(int count) {
			foreach (int value in Range(0, count)) {
				yield return value;
			}
		}

		public static IEnumerable<int> Range(int min, int max) {
			for (int i = min; i <= max; i++) {
				yield return i;
			}
		}
	}
}
