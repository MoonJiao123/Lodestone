using UnityEngine;

namespace Andtech.InputSystem {

	public static class KeyCodeExtensions {

		/// <summary>
		/// Is the key code a SHIFT?
		/// </summary>
		/// <param name="keyCode">The key code to test.</param>
		/// <returns>The key code is a SHIFT.</returns>
		public static bool IsShift(this KeyCode keyCode) {
			return keyCode.Equals(KeyCode.LeftShift) || keyCode.Equals(KeyCode.RightShift);
		}

		/// <summary>
		/// Is the key code a CTRL?
		/// </summary>
		/// <param name="keyCode">The key code to test.</param>
		/// <returns>The key code is a CTRL.</returns>
		public static bool IsControl(this KeyCode keyCode) {
			return keyCode.Equals(KeyCode.LeftControl) || keyCode.Equals(KeyCode.RightControl);
		}

		/// <summary>
		/// Is the key code a CMD?
		/// </summary>
		/// <param name="keyCode">The key code to test.</param>
		/// <returns>The key code is a CMD.</returns>
		public static bool IsCommand(this KeyCode keyCode) {
			return keyCode.Equals(KeyCode.LeftCommand) || keyCode.Equals(KeyCode.RightCommand);
		}

		/// <summary>
		/// Is the key code a ALT?
		/// </summary>
		/// <param name="keyCode">The key code to test.</param>
		/// <returns>The key code is a ALT.</returns>
		public static bool IsAlt(this KeyCode keyCode) {
			return keyCode.Equals(KeyCode.LeftAlt) || keyCode.Equals(KeyCode.RightAlt);
		}

		/// <summary>
		/// Is the key code a mouse button?
		/// </summary>
		/// <param name="keyCode">The key code to test.</param>
		/// <returns>The key code is a mouse button.</returns>
		public static bool IsMouse(this KeyCode keyCode) {
			if (keyCode.Equals(KeyCode.Mouse0))
				return true;

			if (keyCode.Equals(KeyCode.Mouse1))
				return true;

			if (keyCode.Equals(KeyCode.Mouse2))
				return true;

			if (keyCode.Equals(KeyCode.Mouse3))
				return true;

			if (keyCode.Equals(KeyCode.Mouse4))
				return true;

			if (keyCode.Equals(KeyCode.Mouse5))
				return true;

			if (keyCode.Equals(KeyCode.Mouse6))
				return true;

			return false;
		}
	}
}
