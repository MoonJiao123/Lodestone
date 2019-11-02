using UnityEngine;

namespace Andtech.SceneManagement {

	public abstract class Bootstrapper : MonoBehaviour {

		#region VIRTUAL
		public virtual void OnPreMount() {

		}
		#endregion VIRTUAL

		#region ABSTRACT
		public abstract void OnMount();

		public abstract void OnDismount();
        #endregion ABSTRACT
	}
}
