using System;

namespace Andtech {

	/// <summary>
	/// A value with support for events.
	/// </summary>
	public struct EventValue<T> {

		public EventValue(T initialValue) {
			ValueChanged = null;
			value = initialValue;
		}

		private T value;

		public void Set(T value) {
			this.value = value;

			OnSet(EventArgs.Empty);
		}

		#region OVERRIDE
		public override string ToString() {
			return value.ToString();
		}
		#endregion OVERIDE

		#region OPERATOR
		/// <summary>
		/// Unbox operator.
		/// </summary>
		/// <param name="valueManager">The value manager to unbox.</param>
		public static implicit operator T(EventValue<T> valueManager) {
			return valueManager.value;
		}
		#endregion OPERATOR

		#region EVENT
		public event EventHandler ValueChanged;		

		private void OnSet(EventArgs e) => ValueChanged?.Invoke(this, e);
		#endregion EVENT
	}
}
