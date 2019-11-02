
namespace Andtech.Automata.Collections {

	/// <summary>
	/// Container for data in a <see cref="StateDictionary{S, V}"/> or <see cref="TransitionDictionary{S, A, V}"/>.
	/// </summary>
	/// <typeparam name="V"></typeparam>
	internal struct Entry<V> {
		public readonly V Value;
		public readonly TransitionDirection Direction;
		public readonly TransitionCondition Condition;

		public Entry(V value, TransitionDirection direction, TransitionCondition condition) {
			this.Value = value;
			this.Direction = direction;
			this.Condition = condition;
		}
	}
}
