using System.Linq;

namespace Andtech.Automata {

	public static class IStateMachineExtensions {

		/// <summary>
		/// Attempts to obtain a single present state.
		/// </summary>
		/// <typeparam name="S">The state type.</typeparam>
		/// <typeparam name="A">The letter type.</typeparam>
		/// <param name="machine">The machine to use.</param>
		/// <param name="state">The output present state.</param>
		/// <returns>Is there a present state?</returns>
		public static bool TryGetPresentState<S, A>(this IStateMachine<S, A> machine, out S state) {
			try {
				state = machine.PresentStates.Single();

				return true;
			}
			catch {
				state = default;

				return false;
			}
		}
	}
}
