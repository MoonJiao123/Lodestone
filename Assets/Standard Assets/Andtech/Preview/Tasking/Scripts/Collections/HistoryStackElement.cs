
namespace Andtech.Tasking.Collections {

	/// <summary>
	/// An element in a history stack.
	/// </summary>
	public class HistoryStackElement<T> {
		/// <summary>
		/// The value contained by the element.
		/// </summary>
		public readonly T value;
		/// <summary>
		/// Does the element currently represent an inversion?
		/// </summary>
		public readonly bool inverted;

		public HistoryStackElement(T value, bool inverted) {
			this.value = value;
			this.inverted = inverted;
		}
	}
}
