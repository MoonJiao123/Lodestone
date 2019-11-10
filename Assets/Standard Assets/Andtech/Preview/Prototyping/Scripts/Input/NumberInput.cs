using System;
using UnityEngine;
using UnityEngine.Events;

namespace Andtech {

	[Serializable]
	public class IntEvent : UnityEvent<int> { }

	public class NumberInput : MonoBehaviour {
		public IntEvent onSelect;

		#region MONOBEHAVIOUR
		protected virtual void Update() {
			foreach (int i in Enumeration.Range(9)) {
				KeyCode keyCodeAlpha = KeyCode.Alpha0 + i;
				KeyCode keyCodeNumpad = KeyCode.Keypad0 + i;

				bool pressed = Input.GetKeyDown(keyCodeAlpha) || Input.GetKeyDown(keyCodeNumpad);

				if (pressed)
					onSelect.Invoke(i);
			}
		}
		#endregion MONOBEHAVIOUR
	}
}
