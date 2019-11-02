
namespace Andtech.Automata {

	public static class StateMachineExtensions {

		/// <summary>
		/// Brings the machine to the initial state.
		/// </summary>
		/// <typeparam name="S">The state type.</typeparam>
		/// <typeparam name="A">The action type.</typeparam>
		/// <param name="machine">The state machine to reset.</param>
		public static void Reset<S, A>(this StateMachine<S, A> machine) {
			machine.Set(machine.StartStates);
		}
	}
}
