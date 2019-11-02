using System;
using System.Collections;
using System.Collections.Generic;

namespace Andtech.Collections {
    
	/// <summary>
	/// Dictionary implementation with multiple values per key.
	/// </summary>
	/// <typeparam name="K">The key type.</typeparam>
	/// <typeparam name="V">The value type.</typeparam>
	public class MultiDictionary<K, V> : IEnumerable<KeyValuePair<K, V>> {
		public ICollection<V> this[K key] => dictionary[key];
		public MultiKeyCollection Keys => new MultiKeyCollection(this);
		public MultiValueCollection Values => new MultiValueCollection(this);

		private readonly Dictionary<K, ICollection<V>> dictionary;

		public MultiDictionary() {
			dictionary = new Dictionary<K, ICollection<V>>();
		}

		public MultiDictionary(int capacity) {
			dictionary = new Dictionary<K, ICollection<V>>(capacity);
		}

		/// <summary>
		/// Adds the specified key and value to the dictionary.
		/// </summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		public void Add(K key, V value) {
			if (!dictionary.ContainsKey(key))
				dictionary.Add(key, new LinkedList<V>());

			this[key].Add(value);
		}

		/// <summary>
		/// Removes all value with the specified key.
		/// </summary>
		/// <param name="key">The key of the values to remove.</param>
		public void Remove(K key) {
			dictionary.Remove(key);
		}

		/// <summary>
		/// Removes the specified key and value from the dictionary.
		/// </summary>
		/// <param name="key">The key of the value to remove.</param>
		/// <param name="value">The value to remove.</param>
		public void Remove(K key, V value) {
			ICollection<V> values = this[key];
			values.Remove(value);
			if (values.Count == 0)
				Remove(key);
		}

		/// <summary>
		/// Clears all elements from the dictionary.
		/// </summary>
		public void Clear() {
			dictionary.Clear();
		}

		/// <summary>
		/// Determines whether the <see cref="MultiDictionary{K, V}"/> contains the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="MultiDictionary{K, V}"/>.</param>
		/// <returns>The <see cref="MultiDictionary{K, V}"/> contains the key.</returns>
		public bool ContainsKey(K key) {
			return dictionary.ContainsKey(key);
		}

		#region INTERFACE
		public IEnumerator<KeyValuePair<K, V>> GetEnumerator() {
			foreach (K key in Keys) {
				foreach (V value in this[key]) {
					yield return new KeyValuePair<K, V>(key, value);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			throw new NotImplementedException();
		}
		#endregion INTERFACE

		#region CLASS
		public class MultiKeyCollection : IEnumerable<K> {
			public int Count {
				get {
					return multiDictionary.dictionary.Keys.Count;
				}
			}

			private readonly MultiDictionary<K, V> multiDictionary;

			public MultiKeyCollection(MultiDictionary<K, V> multiDictionary) {
				this.multiDictionary = multiDictionary;
			}

			#region INTERFACE
			public IEnumerator<K> GetEnumerator() {
				foreach (K key in multiDictionary.dictionary.Keys) {
					yield return key;
				}
			}

			IEnumerator IEnumerable.GetEnumerator() {
				return GetEnumerator();
			}
			#endregion INTERFACE
		}

		public class MultiValueCollection : IEnumerable<V> {
			public int Count {
				get {
					return multiDictionary.dictionary.Values.Count;
				}
			}

			private readonly MultiDictionary<K, V> multiDictionary;

			public MultiValueCollection(MultiDictionary<K, V> multiDictionary) {
				this.multiDictionary = multiDictionary;
			}

			#region INTERFACE
			public IEnumerator<V> GetEnumerator() {
				foreach (ICollection<V> valueCollection in multiDictionary.dictionary.Values) {
					foreach (V value in valueCollection) {
						yield return value;
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator() {
				return GetEnumerator();
			}
			#endregion INTERFACE
		}
		#endregion
	}
}
