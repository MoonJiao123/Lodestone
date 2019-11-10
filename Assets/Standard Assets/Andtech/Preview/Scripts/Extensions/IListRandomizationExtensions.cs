using System;
using System.Collections.Generic;
using UnityEngine;

namespace Andtech {

	public static class IListRandomizationExtensions {

		public static IEnumerable<T> TakeRandom<T>(this IList<T> list) => list.TakeRandom(list.Count);

		public static IEnumerable<T> TakeRandom<T>(this IList<T> list, int count) => list.TakeRandom(count, max => UnityEngine.Random.Range(0, max + 1));

		public static IEnumerable<T> TakeRandom<T>(this IList<T> list, int count, Func<int, int> randomizer) {
			int n = list.Count;
			count = Mathf.Clamp(count, 0, n);
			int upperBound = n - 1;

			while (count-- > 0) {
				int index = randomizer(upperBound);
				yield return list[index];

				list[index] = list[upperBound--];
			}
		}
	}
}
