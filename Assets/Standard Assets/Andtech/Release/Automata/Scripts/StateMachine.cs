using System.Collections.Generic;

namespace Andtech.Automata {

	/// <summary>
	/// Basic state machine.
	/// </summary>
	/// <typeparam name="S">The type of the states.</typeparam>
	/// <typeparam name="A">The type of the letters.</typeparam>
	public class StateMachine<S, A> : IStateMachine<S, A> {
		/// <summary>
		/// Whether a cursor should be removed when no transitions are available.
		/// </summary>
		public bool RetainCursorsOnDeadTransition;

		protected readonly DeltaFunction<S, A> DeltaFunction;
		protected IEnumerable<S> Cursors {
			get => cursors;
			set {
				cursors.Clear();
				foreach (S state in value) {
					cursors.Add(state);
				}
			}
		}

		private readonly IEnumerable<S> startStates;
		private readonly ICollection<S> cursors;

		public StateMachine(DeltaFunction<S, A> deltaFunction, params S[] startStates) : this(deltaFunction, (IEnumerable<S>)startStates) { }

		public StateMachine(DeltaFunction<S, A> deltaFunction, IEnumerable<S> startStates) {
			this.DeltaFunction = deltaFunction;
			this.startStates = startStates;
			cursors = new HashSet<S>(startStates);
			RetainCursorsOnDeadTransition = false;
		}

		#region VIRTUAL
		public virtual IEnumerable<S> StartStates => startStates;

		/// <summary>
		/// Forces the machine to have the specified present states.
		/// </summary>
		/// <param name="states">The new present states.</param>
		public virtual void Set(params S[] states) => Cursors = states;

		/// <summary>
		/// Forces the machine to have the specified present states.
		/// </summary>
		/// <param name="states">The new present states.</param>
		public virtual void Set(IEnumerable<S> states) => Cursors = states;

		/// <summary>
		/// Advance the machine to the next state(s).
		/// </summary>
		/// <param name="letter">The letter used for transitioning.</param>
		protected virtual void Step(A letter) {
			ICollection<S> nextStates = new LinkedList<S>();

			foreach (S state in cursors) {
				try {
					foreach (S nextState in DeltaFunction.Evaluate(state, letter)) {
						if (!nextStates.Contains(nextState))
							nextStates.Add(nextState);
					}
				}
				catch (UndefinedTransitionException) {
					if (RetainCursorsOnDeadTransition) {
						if (!nextStates.Contains(state))
							nextStates.Add(state);
					}
				}
			}

			Cursors = nextStates;
		}
		#endregion VIRTUAL

		#region INTERFACE
		IEnumerable<S> IStateMachine<S, A>.PresentStates => cursors;

		void IStateMachine<S, A>.Step(A letter) => Step(letter);
		#endregion INTERFACE
	}
}
