using System;
using System.Collections.Generic;
using UnityEngine;

namespace Andtech.InputSystem {

	public class KeystrokeModule : MonoBehaviour {
		protected readonly Dictionary<KeystrokeCondition, Action> actions = new Dictionary<KeystrokeCondition, Action>();

		#region MONOBEHAVIOUR
		protected virtual void Update() {
			KeystrokeModifier modifier = GetKeystrokeModifier();

			foreach (KeyValuePair<KeystrokeCondition, Action> pair in actions) {
				KeystrokeCondition condition = pair.Key;
				Action action = pair.Value;

				if (modifier.Equals(condition.Modifier) && condition.Test())
					action();
			}
		}
		#endregion MONOBEHAVIOUR

		#region VIRTUAL
		public virtual void Add(KeystrokeCondition condition, Action action) {
			actions.Add(condition, action);
		}

		public virtual void Remove(KeystrokeCondition condition) {
			actions.Remove(condition);
		}

		public virtual void Clear() {
			actions.Clear();
		}
		#endregion VIRTUAL

		#region PIPELINE
		protected static KeystrokeModifier GetKeystrokeModifier() {
			bool pressingAlt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftAlt);
			bool pressingCtrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			bool pressingCmd = Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand);
			bool pressingShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

			KeystrokeModifier modifier = KeystrokeModifier.None;
			if (pressingAlt)
				modifier |= KeystrokeModifier.Alt;
			if (pressingCtrl)
				modifier |= KeystrokeModifier.Ctrl;
			if (pressingCmd)
				modifier |= KeystrokeModifier.Cmd;
			if (pressingShift)
				modifier |= KeystrokeModifier.Shift;

			return modifier;
		}
		#endregion PIPELINE
	}
}
