using System;

namespace Andtech.Automata {

	/// <summary>
	/// Description of a direction between state.
	/// </summary>
	[Flags]
	public enum TransitionDirection {
		Enter		= (1 << 0),
		Exit		= (1 << 1),
		Any			= -1
	}
}
