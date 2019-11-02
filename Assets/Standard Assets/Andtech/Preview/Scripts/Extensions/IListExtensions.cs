using System.Collections;
using System.Collections.Generic;

namespace Andtech.Extensions {

	/// <summary>
	/// Useful extension methods for lists.
	/// </summary>
	public static class IListExtensions {

		/// <summary>
		/// Returns all elements from the index <paramref name="first"/> (inclusive) to the last index (inclusive).
		/// </summary>
		/// <typeparam name="T">The type of the data.</typeparam>
		/// <param name="list">The list to slice.</param>
		/// <param name="first">The index of the first element in the sliced portion.</param>
		/// <returns>The sliced portion.</returns>
		public static IList Slice<T>(this IList<T> list, int first) {
			return Slice(list, first, list.Count - 1);
		}

		/// <summary>
		/// Returns all elements from the index <paramref name="first"/> (inclusive) to the index <paramref name="last"/> (inclusive).
		/// </summary>
		/// <typeparam name="T">The type of the data.</typeparam>
		/// <param name="list">The list to slice.</param>
		/// <param name="first">The index of the first element in the sliced portion.</param>
		/// <param name="last">The index of the last element in the sliced portion.</param>
		/// <returns>The sliced portion.</returns>
		public static IList Slice<T>(this IList<T> list, int first, int last) {
			if (first < 0)
				first = 0;
			if (last > list.Count - 1)
				last = list.Count - 1;
			if (last < first)
				return new T[0];

			int n = last - first + 1;
			IList sliced = new T[n];
			for (int i = 0; i < n; i++) {
				sliced[i] = list[first + i];
			}
			return sliced;
		}

		/// <summary>
		/// Returns <paramref name="count"/> elements.
		/// </summary>
		/// <typeparam name="T">The type of the data.</typeparam>
		/// <param name="list">The list to slice.</param>
		/// <param name="count">The count of elements to get.</param>
		/// <returns>The sliced portion.</returns>
		public static IList GetRange<T>(this IList<T> list, int count) {
			return GetRange(list, 0, count);
		}

		/// <summary>
		/// Returns <paramref name="count"/> elements starting from the index <paramref name="first"/>.
		/// </summary>
		/// <typeparam name="T">The type of the data.</typeparam>
		/// <param name="list">The list to slice.</param>
		/// <param name="first">The index of the first element in the sliced portion.</param>
		/// <param name="count">The count of elements to get.</param>
		/// <returns>The sliced portion.</returns>
		public static IList GetRange<T>(this IList<T> list, int first, int count) {
			int last = first + count - 1;
			if (first < 0)
				first = 0;
			if (last > list.Count - 1)
				last = list.Count - 1;
			IList sliced = new T[count];
			for (int i = 0; i < count; i++) {
				sliced[i] = list[first + i];
			}
			return sliced;
		}
	}
}
