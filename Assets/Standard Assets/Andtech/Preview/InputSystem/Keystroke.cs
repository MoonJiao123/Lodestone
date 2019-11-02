using UnityEngine;

namespace Andtech.InputSystem {

	public struct Keystroke {
		public KeystrokeModifier Modifier {
			get {
				return modifier;
			}
		}
		public KeyCode KeyCode {
			get {
				return keyCode;
			}
		}

		private readonly KeystrokeModifier modifier;
		private readonly KeyCode keyCode;

		public Keystroke(KeyCode keyCode) : this(default, keyCode) {}

		public Keystroke(KeystrokeModifier modifier, KeyCode keyCode) {
			this.modifier = modifier;
			this.keyCode = keyCode;
		}

		#region OVERRIDE
		public override bool Equals(object obj) {
			if (!(obj is Keystroke))
				return false;

			var keystroke = (Keystroke)obj;
			return Modifier == keystroke.Modifier &&
				   KeyCode == keystroke.KeyCode;
		}

		public override int GetHashCode() {
			int value = (int)KeyCode << 2 | (int)Modifier;

			return value.GetHashCode();
		}

		public override string ToString() {
			if (Modifier.Equals(KeystrokeModifier.None))
				return string.Format("{1}", Modifier, KeyCode);

			return string.Format("{0} + {1}", Modifier, KeyCode);
		}
		#endregion OVERRIDE

		#region OPERATOR
		public static implicit operator Keystroke(KeyCode keyCode) {
			return new Keystroke(keyCode);
		}
		#endregion OPERATOR
	}
}
