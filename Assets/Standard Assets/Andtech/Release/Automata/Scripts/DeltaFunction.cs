using Andtech.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Andtech.Automata {

	/// <summary>
	/// Defines a mapping from state/transition combinations to states.
	/// </summary>
	/// <typeparam name="S">The state type.</typeparam>
	/// <typeparam name="A">The letter type.</typeparam>
	public class DeltaFunction<S, A> : IEnumerable<ValueTuple<S, A, S>> {
		/// <summary>
		/// Can the function have more than 1 definition per state/letter combination?
		/// </summary>
		public readonly bool allowMultipleDefinitions;
		/// <summary>
		/// The set of recognized states.
		/// </summary>
		public ICollection<S> States {
			get;
			protected set;
		}
		/// <summary>
		/// The set of recognized letters.
		/// </summary>
		public ICollection<A> Alphabet {
			get;
			protected set;
		}

		protected Table<S, A, S> Table {
			get;
			private set;
		}

		protected DeltaFunction(Table<S, A, S> table, bool allowMultipleDefinitions = true) {
			Table = table;
			this.allowMultipleDefinitions = allowMultipleDefinitions;
		}

		public DeltaFunction(ICollection<S> states, ICollection<A> alphabet, bool allowMultipleDefinitions = true) {
			Table = new Table<S, A, S>(states, alphabet);
			States = states;
			Alphabet = alphabet;
			this.allowMultipleDefinitions = allowMultipleDefinitions;
		}

		/// <summary>
		/// Returns the result of function.
		/// </summary>
		/// <param name="state">The present state.</param>
		/// <param name="letter">The transitioning letter.</param>
		/// <returns>The next state.</returns>
		public ICollection<S> Evaluate(S state, A letter) {
			Validate(state);
			Validate(letter);

			try {
				return Table[state, letter];
			}
			catch (KeyNotFoundException) {
				throw new UndefinedTransitionException(state, letter);
			}
		}

		#region VIRTUAL
		/// <summary>
		/// Registers a mapping between state + letter to nextState.
		/// </summary>
		/// <param name="state">The present state.</param>
		/// <param name="letter">The transitioning letter.</param>
		/// <param name="nextState">The next state.</param>
		public virtual void Define(S state, A letter, S nextState) {
			Validate(state);
			Validate(letter);
			Validate(nextState);

			// Check for multiple definitions
			if (Contains(state, letter)) {
				if (!allowMultipleDefinitions)
					throw new MultipleDefinitionException(state, letter);

				if (Table.ContainsFull(state, letter, nextState))
					throw new MultipleDefinitionException(state, letter);
			}

			// Add definition to function
			Table.Add(state, letter, nextState);
		}

		/// <summary>
		/// Does the function contain a definition for the state/letter combination?
		/// </summary>
		/// <param name="state">The present state.</param>
		/// <param name="letter">The transition letter.</param>
		public virtual bool Contains(S state, A letter) {
			return Table.ContainsKey(state, letter);
		}
		#endregion VIRTUAL

		#region INTERFACE
		IEnumerator<(S, A, S)> IEnumerable<(S, A, S)>.GetEnumerator() {
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			throw new NotImplementedException();
		}
		#endregion INTERFACE

		#region PIPELINE
		private void Validate(A letter) {
			if (!Alphabet.Contains(letter))
				throw new ArgumentException(string.Format("The letter {0} is not recognized", letter));
		}

		private void Validate(S state) {
			if (!States.Contains(state))
				throw new ArgumentException(string.Format("The state {0} is not recognized", state));
		}
		#endregion PIPELIN
	}
}
