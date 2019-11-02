using Andtech.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Andtech.Automata.Collections {

	/// <summary>
	/// Associates (multiple) values with states.
	/// </summary>
	/// <typeparam name="S">The type of the keys (states).</typeparam>
	/// <typeparam name="V">The type of the values.</typeparam>
	public class StateDictionary<S, V> {
		private readonly MultiDictionary<S, Entry<V>> data;

		public StateDictionary(ICollection<S> states) {
			data = new MultiDictionary<S, Entry<V>>(states.Count);
		}

		public void Add(S state, V value, TransitionDirection direction, TransitionCondition condition = TransitionCondition.Always) {
			Entry<V> entry = new Entry<V>(value, direction, condition);
			data.Add(state, entry);
		}

		public IEnumerable<V> GetEnumerator(S state, TransitionDirection directionMask, TransitionCondition conditionMask = TransitionCondition.Always) {
            // Validate key
            if (!data.ContainsKey(state))
                yield break;

            // Compute query
            var query =
                from Entry<V> entry in data[state]
                where (entry.Direction & directionMask) != 0 && (entry.Condition & conditionMask) != 0
                select entry.Value;

            foreach (V value in query) {
                yield return value;
            }
		}
	}
}
