using System;

namespace Andtech.InputSystem {

	[Flags]
	public enum KeystrokeModifier {
		None = 0,
		Alt = 1 << 0,
		Ctrl = 1 << 1,
		Cmd = 1 << 2,
		Shift = 1 << 3
	}
}
