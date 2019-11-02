using Andtech.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Andtech.Automata.Collections {

	/// <summary>
	/// Associates (multiple) values with {state, transition} combinations.
	/// </summary>
	/// <typeparam name="S">The type of key 1 (states).</typeparam>
	/// <typeparam name="A">The type of key 2 (transitions).</typeparam>
	/// <typeparam name="V">The type of the values.</typeparam>
	public class TransitionDictionary<S, A, V> {
		private readonly Table<S, A, Entry<V>> data;

		public TransitionDictionary(ICollection<S> states, ICollection<A> alphabet) {
			data = new Table<S, A, Entry<V>>(states, alphabet);
		}

		public void Add(S state, A letter, V value, TransitionDirection direction, TransitionCondition condition = TransitionCondition.Always) {
			Entry<V> entry = new Entry<V>(value, direction, condition);
			data.Add(state, letter, entry);
		}

		public IEnumerable<V> GetEnumerator(S state, A letter, TransitionDirection directionMask, TransitionCondition conditionMask = TransitionCondition.Always) {
            // Validate key combination
            if (!data.ContainsKey(state, letter))
                yield break;

            // Compute query
            var query =
                from Entry<V> entry in data[state, letter]
                where (entry.Direction & directionMask) != 0 && (entry.Condition & conditionMask) != 0
                select entry.Value;

            foreach (V value in query) {
                yield return value;
            }
        }
	}
}
