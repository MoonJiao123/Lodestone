
namespace Andtech.Core {

	public interface IDiff {
		object Key {
			get;
		}
		object OldValue {
			get;
		}
		object NewValue {
			get;
		}
	}

	public interface IDiff<TKey, TValue> {
		TKey Key {
			get;
		}
		TValue OldValue {
			get;
		}
		TValue NewValue {
			get;
		}
	}
}
