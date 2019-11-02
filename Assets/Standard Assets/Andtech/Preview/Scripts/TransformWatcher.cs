using UnityEngine;
using UnityEngine.Events;

namespace Andtech {

	[DisallowMultipleComponent, ExecuteInEditMode]
	public class TransformWatcher : MonoBehaviour {
		public UnityEvent onTransformChanged;

		protected virtual void LateUpdate() {
			if (transform.hasChanged) {
				onTransformChanged.Invoke();
				transform.hasChanged = false;
			}
		}
	}
}
