using Andtech.Collections;
using System.Collections.Generic;

namespace Andtech.Automata {

	/// <summary>
	/// Dynamic version of <see cref="DeltaFunction{S, A}"/>. The state set and alphabet are dynamically constructed.
	/// </summary>
	/// <typeparam name="S">The state type.</typeparam>
	/// <typeparam name="A">The letter type.</typeparam>
	public class DynamicDeltaFunction<S, A> : DeltaFunction<S, A>{

		public DynamicDeltaFunction(bool allowMultipleDefinitions = true) : base(new Table<S, A, S>(), allowMultipleDefinitions) {
			States = new HashSet<S>();
			Alphabet = new HashSet<A>();
		}

		public DynamicDeltaFunction(ICollection<S> states, ICollection<A> alphabet, bool allowMultipleDefinitions = true) : base(new Table<S, A, S>(states, alphabet), allowMultipleDefinitions) {
			States = new HashSet<S>(states);
			Alphabet = new HashSet<A>(alphabet);
		}

		/// <summary>
		/// Manually adds a state to the set of recognizable states.
		/// </summary>
		/// <param name="state">State to recognize.</param>
		public void AddState(S state) {
			if (States.Contains(state))
				return;

			States.Add(state);
		}

		/// <summary>
		/// Manually adds a letter to the set of recognizable letters.
		/// </summary>
		/// <param name="letter">Letter to recognize.</param>
		public void AddLetter(A letter) {
			if (Alphabet.Contains(letter))
				return;

			Alphabet.Add(letter);
		}

		#region OVERRIDE
		/// <summary>
		/// Registers a mapping between state + letter to nextState.
		/// </summary>
		/// <seealso cref="DeltaFunction{S, A}.Define(S, A, S)"/>
		/// <param name="state">The present state.</param>
		/// <param name="letter">The transitioning letter.</param>
		/// <param name="nextState">The next state.</param>
		public override void Define(S state, A letter, S nextState) {
			// Dynamic add to state set and/or alphabet
			AddState(state);
			AddLetter(letter);
			AddState(nextState);

			// Define the transition
			base.Define(state, letter, nextState);
		}
		#endregion OVERRIDE
	}
}
