using System;

namespace Andtech.Automata {

	/// <summary>
	/// Description of a transition constraint.
	/// </summary>
	[Flags]
	public enum TransitionCondition {
		Never		= 0,
		/// <summary>
		/// The state must be the same as the original.
		/// </summary>
		Self		= (1 << 0),
		/// <summary>
		/// The state must be different from the original.
		/// </summary>
		NotSelf		= (1 << 1),

		Always		= -1
	}
}
