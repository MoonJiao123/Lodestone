using UnityEngine;
using UnityEngine.Events;

namespace Andtech {

	/// <summary>
	/// Invokes actions when a button is activated.
	/// </summary>
	public class ButtonInput : MonoBehaviour {
		[SerializeField]
		private string axisName = "Fire1";
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
				p |= Input.GetButtonDown(axisName) ? KeyPhase.Down : KeyPhase.None;
				p |= Input.GetButtonUp(axisName) ? KeyPhase.Up : KeyPhase.None;
				p |= Input.GetButton(axisName) ? KeyPhase.Pressed : KeyPhase.Unpressed;

				return p;
			}
			#endregion
		}
		#endregion MONOBEHAVIOUR
	}
}
