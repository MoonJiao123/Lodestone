using System;
using UnityEngine;

namespace Andtech.InputSystem {

	public class KeystrokeCondition {
		public Func<bool> Test {
			get;
			set;
		}
		public KeystrokeModifier Modifier {
			get;
			set;
		}
		public bool UIOcclusion {
			get;
			set;
		}
		
		public KeystrokeCondition(Func<bool> test, KeystrokeModifier modifier = KeystrokeModifier.None, bool uiOcclusion = false) {
			Test = test;
			Modifier = modifier;
			UIOcclusion = uiOcclusion;
		}
		
		public static KeystrokeCondition GetKeyDown(KeyCode keyCode, KeystrokeModifier modifier = KeystrokeModifier.None) {
			return new KeystrokeCondition(() => Input.GetKeyDown(keyCode), modifier);
		}

		public static KeystrokeCondition GetKey(KeyCode keyCode, KeystrokeModifier modifier = KeystrokeModifier.None) {
			return new KeystrokeCondition(() => Input.GetKey(keyCode), modifier);
		}

		public static KeystrokeCondition GetKeyUp(KeyCode keyCode, KeystrokeModifier modifier = KeystrokeModifier.None) {
			return new KeystrokeCondition(() => Input.GetKeyUp(keyCode), modifier);
		}
	}
}
