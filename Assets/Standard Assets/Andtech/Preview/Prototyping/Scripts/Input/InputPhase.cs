using System;

namespace Andtech {

	[Flags]
	public enum KeyPhase {
		None = 0,
		Down = 1 << 0,
		Pressed = 1 << 1,
		Up = 1 << 2,
		Unpressed = 1 << 3
	}
}
