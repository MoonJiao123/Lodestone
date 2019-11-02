using Andtech.Automata.Collections;
using System;
using System.Collections.Generic;

namespace Andtech.Automata {

	/// <summary>
	/// <see cref="StateMachine{S, A}"/> with support for intercepted events.
	/// </summary>
	/// <typeparam name="S">The type of the states.</typeparam>
	/// <typeparam name="A">The type of the letters.</typeparam>
	public class EventStateMachine<S, A> : StateMachine<S, A> {
		/// <summary>
		/// Should we invoke enter actions upon leaving a state forcefully?
		/// </summary>
		public bool InvokeEnterActionsOnJump;
		/// <summary>
		/// Should we invoke exit actions upon leaving a state forcefully?
		/// </summary>
		public bool InvokeExitActionsOnJump;

		private readonly StateDictionary<S, Action> stateActions;
		private readonly TransitionDictionary<S, A, Action> transitionActions;

		public EventStateMachine(DeltaFunction<S, A> deltaFunction, params S[] startStates) : this(deltaFunction, (ICollection<S>)startStates) { }

		public EventStateMachine(DeltaFunction<S, A> deltaFunction, ICollection<S> startStates) : base(deltaFunction, startStates) {
			stateActions = new StateDictionary<S, Action>(deltaFunction.States);
			transitionActions = new TransitionDictionary<S, A, Action>(deltaFunction.States, deltaFunction.Alphabet);
			InvokeEnterActionsOnJump = true;
			InvokeExitActionsOnJump = true;
		}

		/// <summary>
		/// Registers an action to a state.
		/// </summary>
		/// <param name="state">The target state.</param>
		/// <param name="action">The action to be invoked.</param>
		/// <param name="direction">The directional constraint.</param>
		/// <param name="condition">The conditional constraint.</param>
		public void Add(S state, Action action, TransitionDirection direction = TransitionDirection.Enter, TransitionCondition condition = TransitionCondition.Always) {
			stateActions.Add(state, action, direction, condition);
		}

		/// <summary>
		/// Registers an action to a transition.
		/// </summary>
		/// <param name="state">The target state.</param>
		/// <param name="letter">The target transitioning letter.</param>
		/// <param name="action">The action to be invoked.</param>
		/// <param name="direction">The directional constraint.</param>
		/// <param name="condition">The conditional constraint.</param>
		public void Add(S state, A letter, Action action, TransitionDirection direction = TransitionDirection.Exit, TransitionCondition condition = TransitionCondition.Always) {
			transitionActions.Add(state, letter, action, direction, condition);
		}

		#region OVERRIDE
		/// <summary>
		/// Forces the machine to have the specified present states.
		/// </summary>
		/// <param name="states">The new present states.</param>
		public override void Set(params S[] states) {
			Set(states as IEnumerable<S>);
		}

		/// <summary>
		/// Forces the machine to have the specified present states.
		/// </summary>
		/// <param name="states">The new present states.</param>
		public override void Set(IEnumerable<S> states) {
			ICollection<Action> onExit = new LinkedList<Action>();
			ICollection<Action> onEnter = new LinkedList<Action>();

			if (InvokeExitActionsOnJump) {
				foreach (S state in Cursors) {
					EnqueueExitCallbacks(stateActions.GetEnumerator(state, TransitionDirection.Exit));
				}
			}

			if (InvokeEnterActionsOnJump) {
				foreach (S state in states) {
					EnqueueEnterCallbacks(stateActions.GetEnumerator(state, TransitionDirection.Enter));
				}
			}

			// Invoke exit callbacks
			foreach (Action action in onExit) {
				action();
			}

			base.Set(states);

			// Invoke enter callbacks
			foreach (Action action in onEnter) {
				action();
			}

			// Local functions
			void EnqueueExitCallbacks(IEnumerable<Action> enumerable) {
				foreach (Action callback in enumerable) {
					onExit.Add(callback);
				}
			}

			void EnqueueEnterCallbacks(IEnumerable<Action> enumerable) {
				foreach (Action callback in enumerable) {
					onEnter.Add(callback);
				}
			}
		}

		/// <summary>
		/// Advance the machine to the next state(s).
		/// </summary>
		/// <seealso cref="StateMachine{S, A}.Step(A)"/>
		/// <param name="letter">The letter used for transitioning.</param>
		protected override void Step(A letter) {
			ICollection<Action> onExit = new LinkedList<Action>();
			ICollection<Action> onEnter = new LinkedList<Action>();

			foreach (S state in Cursors) {
				if (DeltaFunction.Contains(state, letter)) {
					foreach (S nextState in DeltaFunction.Evaluate(state, letter)) {
						TransitionCondition condition = nextState.Equals(state) ? TransitionCondition.Self : TransitionCondition.NotSelf;

						EnqueueExitCallbacks(stateActions.GetEnumerator(state, TransitionDirection.Exit, condition));
						EnqueueExitCallbacks(transitionActions.GetEnumerator(state, letter, TransitionDirection.Exit, condition));
						EnqueueEnterCallbacks(transitionActions.GetEnumerator(nextState, letter, TransitionDirection.Enter, condition));
						EnqueueEnterCallbacks(stateActions.GetEnumerator(nextState, TransitionDirection.Enter, condition));
					}
				}
				else {
					if (!RetainCursorsOnDeadTransition) {
						EnqueueExitCallbacks(stateActions.GetEnumerator(state, TransitionDirection.Exit));
						EnqueueExitCallbacks(transitionActions.GetEnumerator(state, letter, TransitionDirection.Exit));
					}
				}
			}

			// Invoke exit callbacks
			foreach (Action action in onExit) {
				action();
			}

			// Update the machine
			base.Step(letter);

			// Invoke enter callbacks
			foreach (Action action in onEnter) {
				action();
			}

			// Local functions
			void EnqueueExitCallbacks(IEnumerable<Action> enumerable) {
				foreach (Action callback in enumerable) {
					onExit.Add(callback);
				}
			}

			void EnqueueEnterCallbacks(IEnumerable<Action> enumerable) {
				foreach (Action callback in enumerable) {
					onEnter.Add(callback);
				}
			}
		}
		#endregion OVERRIDE
	}
}
