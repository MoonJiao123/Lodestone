using Andtech.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Andtech.InputSystem {

	public delegate void ReceiveAxis2(Vector2 input);

	public class Joystick2 : MonoBehaviour {
		private readonly MultiDictionary<Tuple<string, string>, ReceiveAxis2> axes = new MultiDictionary<Tuple<string, string>, ReceiveAxis2>();

		public void Add(string axisName0, string axisName1, ReceiveAxis2 receive) {
			Tuple<string, string> key = new Tuple<string, string>(axisName0, axisName1);
			axes.Add(key, receive);
		}

		public void Remove(string axisName0, string axisName1) {
			Tuple<string, string> key = new Tuple<string, string>(axisName0, axisName1);
			axes.Remove(key);
		}

		public void Clear() {
			axes.Clear();
		}

		#region MONOBEHAVIOUR		
		protected virtual void Update() {
			foreach (KeyValuePair<Tuple<string, string>, ReceiveAxis2> pair in axes) {
				string axisName0 = pair.Key.Item1;
				string axisName1 = pair.Key.Item2;

				Vector2 input;
				input.x = Input.GetAxis(axisName0);
				input.y = Input.GetAxis(axisName1);

				foreach (ReceiveAxis2 receive in axes[pair.Key]) {
					receive(input);
				}
			}
		}
		#endregion MONOBEHAVIOUR
	}
}
