
namespace Andtech.Core {

	/// <summary>
	/// Stores a single difference between a key and value.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the values.</typeparam>
	public struct Diff<TKey, TValue> : IDiff, IDiff<TKey, TValue> {
		public TKey Key { get; private set; }
		public TValue OldValue { get; private set; }
		public TValue NewValue { get; private set; }

		public Diff(TKey key, TValue oldValue, TValue newValue) {
			Key = key;
			OldValue = oldValue;
			NewValue = newValue;
		}

		#region OVERRIDE
		public override string ToString() {
			return string.Format("{0}:	{1} ▶ {2}", Key, OldValue, NewValue);
		}
		#endregion OVERRIDE

		#region INTERFACE
		object IDiff.Key => Key;
		object IDiff.OldValue => OldValue;
		object IDiff.NewValue => NewValue;
		#endregion INTERFACE
	}
}
