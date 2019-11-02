using System;
using System.Collections.Generic;

namespace Andtech.Tasking.Collections {

	/// <summary>
	/// A stack implementation which allows popped elements to be restored.
	/// </summary>
	/// 
	/// Example
	/// The capacity is 4. The stack below is read left-to-right, where the left
	///		represents the "top" of the stack.
	/// The restore cursor (first restored element) is represented by 'R'.
	/// The invert cursor (next element to invert) is represented by 'I'.
	/// Each element is added with a non-inverted state. When popped,
	///		the inverse is pushed onto the stack. This is represented by a negative
	///		sign (-). The original remains in the stack.
	///	When the stack reaches capacity, elements are released from the bottom. This
	///		ensures a consistent number of poppable elements.
	/// --------------------------------------------------
	/// RI
	/// A
	/// 
	/// RI
	/// B  A
	/// 
	/// RI
	/// C  B  A
	/// 
	///    R  I
	/// -C C  B  A
	/// 
	///       R     I
	/// -B -C C  B  A
	/// 
	///          R        I
	/// -A -B -C C  B  A
	/// 
	///       R     I
	/// -B -C C  B  A
	/// 
	/// RI
	/// D  -B -C C
	/// --------------------------------------------------


	public class HistoryStack<T> {
		/// <summary>
		/// The current number of "real" elements (excludes tracked, popped elements).
		/// </summary>
		public int Count {
			get;
			private set;
		}
		public bool CanPop {
			get {
				return cursorInvert != null;
			}
		}
		public bool CanRestore {
			get {
				if (ReferenceEquals(list.First, cursorRestorePoint))
					return false;

				return true;
			}
		}

		private readonly int capacity;
		private readonly LinkedList<HistoryStackElement<T>> list;
		/// <summary>
		/// Cursor to mark where restoration began.
		/// </summary>
		/// <remarks>We cannot restore elements past the restore point!</remarks>
		private LinkedListNode<HistoryStackElement<T>> cursorRestorePoint;
		/// <summary>
		/// Cursor to mark the next element to be inverted.
		/// </summary>
		private LinkedListNode<HistoryStackElement<T>> cursorInvert;

		/// <summary>
		/// Constructor which accepts a virtual capacity.
		/// </summary>
		/// <param name="capacity">The number of virtual elements in the stack.</param>
		public HistoryStack(int capacity) {
			this.capacity = capacity;
			list = new LinkedList<HistoryStackElement<T>>();
		}

		/// <summary>
		/// Adds the value to the stack.
		/// </summary>
		/// <param name="value">The element to add.</param>
		public void Push(T value) {
			// Add element to top of stack
			list.AddFirst(new HistoryStackElement<T>(value, false));

			// Sets new restore/inversion point
			cursorRestorePoint = list.First;
			cursorInvert = list.First;

			Clean();
		}

		/// <summary>
		/// Removes the top-most element.
		/// </summary>
		/// <returns>The popped element.</returns>
		/// <remarks>The element is tracked; it can be restored via LIFO calls to <see cref="Restore"/>.</remarks>
		public HistoryStackElement<T> Pop() {
			if (!CanPop)
				throw new InvalidOperationException("No elements can be popped.");

			// Locate element to invert
			HistoryStackElement<T> element = cursorInvert.Value;
			// Increment the invert cursor. (NOTE: if it runs of the end, Restore will catch this)
			cursorInvert = cursorInvert.Next;

			// Push inverse (of target node) to top of stack
			HistoryStackElement<T> toAdd = new HistoryStackElement<T>(element.value, !element.inverted);
			list.AddFirst(toAdd);
			Count--;

			return toAdd;
		}

		/// <summary>
		/// Restores the most recently removed element.
		/// </summary>
		/// <returns>The restored element.</returns>
		public HistoryStackElement<T> Restore() {
			if (!CanRestore)
				throw new InvalidOperationException("No popped elements are being tracked.");

			// Locate the corresponding inversion to restore (always the first)
			HistoryStackElement<T> element = list.First.Value;
			list.RemoveFirst();
			Count++;

			// Update the "inverter" pointer to reflect the restore
			if (cursorInvert == null)
				// The cursor ran off the end. Recover
				cursorInvert = list.Last;
			else
				cursorInvert = cursorInvert.Previous;

			return element;
		}

		/// <summary>
		/// Releases all elements from the stack.
		/// </summary>
		public void Clear() {
			ReleaseAll();

			cursorRestorePoint = null;
			cursorInvert = null;
			Count = 0;
		}

		public IEnumerable<HistoryStackElement<T>> GetEnumerableRaw(bool reverse = false) {
			LinkedListNode<HistoryStackElement<T>> node = (reverse) ? list.Last : list.First;
			while (node != null) {
				yield return node.Value;

				node = (reverse) ? node.Previous : node.Next;
			}
		}

		#region EVENT
		/// <summary>
		/// Event triggered when the stack removed an old element.
		/// </summary>
		public event EventHandler Released;

		protected virtual void OnRelease(EventArgs e) => Released?.Invoke(this, e);
		#endregion EVENT

		#region PIPELINE
		/// <summary>
		/// Removes unnecessary elements (starting with the oldest).
		/// </summary>
		private void Clean() {
			// Begin cleaning from the bottom of stack
			while (list.Count > capacity) {
				// Remove the last element
				HistoryStackElement<T> element = list.Last.Value;
				list.RemoveLast();

				// Trigger events
				HistoryStackEventArgs<T> args = new HistoryStackEventArgs<T>(element.value, element.inverted);
				OnRelease(args);
			}

			Count = list.Count;
		}

		private void ReleaseAll() {
			while (list.Count > 0) {
				// Remove the last element
				HistoryStackElement<T> element = list.Last.Value;
				list.RemoveLast();

				// Trigger events
				HistoryStackEventArgs<T> args = new HistoryStackEventArgs<T>(element.value, element.inverted);
				OnRelease(args);
			}
		}
		#endregion PIPELINE
	}
}
