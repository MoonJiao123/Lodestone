using UnityEngine;

namespace Andtech.SceneManagement {

	[DefaultExecutionOrder(-9999)]
	public class Booter : MonoBehaviour {
		[SerializeField]
		private Bootstrapper bootstrapper = default;

		#region MONOBEHAVIOUR
		protected virtual void Awake() {
			bootstrapper.OnPreMount();
		}

		protected virtual void Start() {
			SceneLoader.Mount(bootstrapper);
		}
		#endregion MONOBEHAVIOUR
	}
}
