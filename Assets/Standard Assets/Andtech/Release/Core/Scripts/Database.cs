using System;
using System.Collections;
using System.Collections.Generic;

namespace Andtech.Core {

	public class DatabaseEventArgs<T> : EventArgs, IGuidIndexable where T : IGuidIndexable {
		public readonly T Element;
		public readonly CrudOperation Operation;

		public DatabaseEventArgs(CrudOperation operation, T element = default) {
			Operation = operation;
			Element = element;
		}

		#region INTERFACE
		public Guid Guid => Element.Guid;
		#endregion INTERFACE
	}

	public delegate void DatabaseEventHandler<T>(DatabaseEventArgs<T> e) where T : IGuidIndexable;

	/// <summary>
	/// Standard storage location for the core system.
	/// </summary>
	public class Database<T> : ICollection<T> where T : IGuidIndexable {
		private Dictionary<Guid, T> elements;

		public Database() {
			elements = new Dictionary<Guid, T>();
		}

        /// <summary>
        /// Replaces the element in the database.
        /// </summary>
        /// <param name="guid">The GUID of the element to replace.</param>
        /// <param name="element">The incoming data.</param>
        /// <returns></returns>
        public bool Replace(Guid guid, T element) {
            // Apply removal
            bool result = elements.Remove(guid);

            // Apply addition
            elements.Add(guid, element);

            Replaced?.Invoke(new DatabaseEventArgs<T>(CrudOperation.Update, element));

            return result;
        }

		/// <summary>
		/// Removes the element from the database.
		/// </summary>
		/// <param name="guid">The GUID of the element to remove.</param>
		/// <returns>Was the element removed successfully?</returns>
		public bool Remove(Guid guid) {
			T element = Get(guid);

			// Apply removal
			bool result = elements.Remove(guid);

			// Fire events
			Removed?.Invoke(new DatabaseEventArgs<T>(CrudOperation.Delete, element));

			return result;
		}

		/// <summary>
		/// Tests whether the database contains a certain element.
		/// </summary>
		/// <param name="guid">The GUID of the element to search for.</param>
		/// <returns>Is the element in the database?</returns>
		public bool ContainsID(Guid guid) {
			return elements.ContainsKey(guid);
		}

		/// <summary>
		/// Retrieves exactly the element with the specified GUID.
		/// </summary>
		/// <param name="guid">The GUID of the element to retrieve.</param>
		/// <returns>The requested element.</returns>
		public T Get(Guid guid) {
			return elements[guid];
		}

		#region INTERFACE
		public int Count => elements.Count;
		public bool IsReadOnly => false;

		public void Add(T element) {
			// Apply insertion
			elements.Add(element.Guid, element);

			// Fire events
			Added?.Invoke(new DatabaseEventArgs<T>(CrudOperation.Create, element));
		}

		public void Clear() {
			elements.Clear();

            Cleared?.Invoke();
		}

		public bool Contains(T item) {
			return elements.ContainsValue(item);
		}

		public void CopyTo(T[] array, int arrayIndex) {
            elements.Values.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item) {
			return Remove(item.Guid);
		}

		public IEnumerator<T> GetEnumerator() {
			return elements.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		#endregion INTERFACE

		#region EVENT
		public event DatabaseEventHandler<T> Added;
        public event DatabaseEventHandler<T> Replaced;
        public event DatabaseEventHandler<T> Removed;
        public event Action Cleared;
        #endregion EVENT
    }
}
