using System.Collections.Generic;

namespace Andtech.Automata {

	public struct BoolStateMachine : IStateMachine<bool, bool> {
		private bool state;

		private readonly bool initialState;

		public BoolStateMachine(bool initialState) {
			this.initialState = initialState;
			state = initialState;
		}

		#region INTERFACE
		IEnumerable<bool> IStateMachine<bool, bool>.PresentStates {
			get {
				yield return state;
			}
		}

		void IStateMachine<bool, bool>.Step(bool letter) {
			state = !state;
		}
		#endregion INTERFACE

		#region OPERATOR
		public static implicit operator bool(BoolStateMachine machine) {
			return machine.state;
		}
		#endregion OPERATOR
	}
}
