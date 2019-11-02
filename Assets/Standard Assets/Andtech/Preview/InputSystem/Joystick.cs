using Andtech.Collections;
using UnityEngine;

namespace Andtech.InputSystem {

	public delegate void ReceiveAxis(float input);

	public class Joystick : MonoBehaviour {		
		private readonly MultiDictionary<string, ReceiveAxis> axes = new MultiDictionary<string, ReceiveAxis>();

		public void Add(string axisName, ReceiveAxis receive) {
			axes.Add(axisName, receive);
		}

		public void Remove(string axisName) {
			axes.Remove(axisName);
		}

		public void Clear() {
			axes.Clear();
		}

		#region MONOBEHAVIOUR		
		protected virtual void Update() {
			foreach (string axisName in axes.Keys) {
				float input = Input.GetAxis(axisName);

				foreach (ReceiveAxis receive in axes[axisName]) {
					receive(input);
				}
			}
		}
		#endregion MONOBEHAVIOUR
	}
}
