using System.Collections.Generic;

namespace Andtech.Automata {

	/// <summary>
	/// A standard state machine type.
	/// </summary>
	/// <typeparam name="S">The state type.</typeparam>
	/// <typeparam name="A">The letter type.</typeparam>
	public interface IStateMachine<S, A> {
		/// <summary>
		/// The set of present states.
		/// </summary>
		IEnumerable<S> PresentStates {
			get;
		}

		/// <summary>
		/// Advance the machine to the next state(s).
		/// </summary>
		/// <param name="letter">The letter used for transitioning.</param>
		void Step(A letter);
	}
}
