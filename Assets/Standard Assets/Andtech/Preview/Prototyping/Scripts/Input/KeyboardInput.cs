using UnityEngine;
using UnityEngine.Events;

namespace Andtech {

	/// <summary>
	/// Invokes actions when a key is activated.
	/// </summary>
	public class KeyboardInput : MonoBehaviour {
		[SerializeField]
		private KeyCode keyCode = KeyCode.Space;
		[SerializeField]
		private KeyPhase phase = KeyPhase.Down;

		public UnityEvent onTrigger;

		#region MONOBEHAVIOUR
		protected virtual void Update() {
			KeyPhase mask = GetPhaseMask();
			if (mask.HasFlag(phase))
				onTrigger?.Invoke();

			#region LOCAL_FUNCTIONS
			KeyPhase GetPhaseMask() {
				var p = KeyPhase.None;
				p |= Input.GetKeyDown(keyCode) ? KeyPhase.Down : KeyPhase.None;
				p |= Input.GetKeyUp(keyCode) ? KeyPhase.Up : KeyPhase.None;
				p |= Input.GetKey(keyCode) ? KeyPhase.Pressed : KeyPhase.Unpressed;

				return p;
			}
			#endregion
		}
		#endregion MONOBEHAVIOUR
	}
}
