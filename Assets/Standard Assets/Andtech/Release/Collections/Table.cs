using System.Collections.Generic;

namespace Andtech.Collections {

	/// <summary>
	/// A doubly-indexed implementation of <see cref="MultiDictionary{K, V}"/>.
	/// </summary>
	/// <typeparam name="K1">The key 1 type.</typeparam>
	/// <typeparam name="K2">The key 2 type.</typeparam>
	/// <typeparam name="V">The value type.</typeparam>
	public class Table<K1, K2, V> {
		public ICollection<V> this[K1 key1, K2 key2] => rows[key1][key2];

		private readonly IDictionary<K1, MultiDictionary<K2, V>> rows;

		public Table() {
			rows = new Dictionary<K1, MultiDictionary<K2, V>>();
		}

		public Table(ICollection<K1> keys1, ICollection<K2> keys2) {
			rows = new Dictionary<K1, MultiDictionary<K2, V>>(keys1.Count);
			foreach (K1 key1 in keys1) {
				rows.Add(key1, new MultiDictionary<K2, V>(keys2.Count));
			}
		}

		/// <summary>
		/// Adds the specified key combination and value to the <see cref="Table{K1, K2, V}"/>.
		/// </summary>
		/// <param name="key1">The first key of the element to add.</param>
		/// <param name="key2">The second key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		public void Add(K1 key1, K2 key2, V value) {
			if (!rows.ContainsKey(key1))
				rows.Add(key1, new MultiDictionary<K2, V>());

			rows[key1].Add(key2, value);
		}

		/// <summary>
		/// Removes all value with the specified first key.
		/// </summary>
		/// <param name="key1">The first key.</param>
		public void Remove(K1 key1) {
			rows.Remove(key1);
		}

		/// <summary>
		/// Removes all values with the specified key combination.
		/// </summary>
		/// <param name="key1">The first key.</param>
		/// <param name="key2">The second key.</param>
		public void Remove(K1 key1, K2 key2) {
			MultiDictionary<K2, V> row = rows[key1];
			row.Remove(key2);

			if (row.Values.Count == 0)
				Remove(key1);
		}

		/// <summary>
		/// Removes the value with the specified key combination.
		/// </summary>
		/// <param name="key1">The first key.</param>
		/// <param name="key2">The second key.</param>
		/// <param name="value">The value to remove.</param>
		public void Remove(K1 key1, K2 key2, V value) {
			MultiDictionary<K2, V> row = rows[key1];
			row.Remove(key2, value);

			if (row.Values.Count == 0)
				Remove(key1);
		}

		/// <summary>
		/// Clears all elements from the table.
		/// </summary>
		public void Clear() {
			rows.Clear();
		}

		/// <summary>
		/// Determines whether the <see cref="Table{K1, K2, V}"/> contains the specified key combination.
		/// </summary>
		/// <param name="key1">The first key to locate in the <see cref="Table{K1, K2, V}"/>.</param>
		/// <param name="key2">The second key to locate in the <see cref="Table{K1, K2, V}"/>.</param>
		/// <returns>The <see cref="Table{K1, K2, V}"/> contains the key combination.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public bool ContainsKey(K1 key1, K2 key2) {
			if (!rows.ContainsKey(key1))
				return false;

			if (!rows[key1].ContainsKey(key2))
				return false;

			return true;
		}

		/// <summary>
		/// Determines whether the <see cref="Table{K1, K2, V}"/> contains a value with the specified key combination.
		/// </summary>
		/// <param name="key1">The first key to locate in the <see cref="Table{K1, K2, V}"/>.</param>
		/// <param name="key2">The second key to locate in the <see cref="Table{K1, K2, V}"/>.</param>
		/// <param name="value">The value to locate in the <see cref="Table{K1, K2, V}"/></param>
		/// <returns>The <see cref="Table{K1, K2, V}"/> contains the value/key combination.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public bool ContainsFull(K1 key1, K2 key2, V value) {
			if (!ContainsKey(key1, key2))
				return false;

			return this[key1, key2].Contains(value);
		}
	}
}
