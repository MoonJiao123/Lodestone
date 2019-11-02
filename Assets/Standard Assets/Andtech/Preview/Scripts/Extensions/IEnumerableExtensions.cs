using System;
using System.Collections.Generic;
using System.Linq;

namespace Andtech {

	public static class LinqExtensions {

		public static bool TryGetFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T value) {
			bool contains = enumerable.Any(predicate);
			if (contains)
				value = enumerable.First(predicate);
			else
				value = default;

			return contains;
		}
	}
}
