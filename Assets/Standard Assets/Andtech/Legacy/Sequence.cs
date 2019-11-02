using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Andtech {

	public class Sequence : MonoBehaviour {
		[HideInInspector]
		public List<UnityEvent> events;

		public void Invoke() {
			foreach (UnityEvent unityEvent in events) {
				unityEvent.Invoke();
			}
		}
	}
}