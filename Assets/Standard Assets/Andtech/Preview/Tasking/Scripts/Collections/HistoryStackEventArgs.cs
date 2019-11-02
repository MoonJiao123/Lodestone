using System;

namespace Andtech.Tasking.Collections {

	/// <summary>
	/// Events data for use with the history stack.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the history stack.</typeparam>
	public class HistoryStackEventArgs<T> : EventArgs {
		public T value;
		public bool inverted;

		public HistoryStackEventArgs(T value, bool inverted) {
			this.value = value;
			this.inverted = inverted;
		}
	}
}
