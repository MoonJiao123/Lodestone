using UnityEngine;

namespace PhysicsSystem {

	public class MagnetTarget : MonoBehaviour, IBeginMagnetizeHandler, IEndMagnetizeHandler {
		[SerializeField]
		private new Rigidbody rigidbody;

		[SerializeField]
		private Renderer renderer;

		public Rigidbody Rigidbody => rigidbody;

		#region VIRTUAL
		public virtual Vector3 Position {
			get => transform.position;
			set => transform.position = value;
		}
		#endregion

		#region INTERFACE
		void IBeginMagnetizeHandler.OnBeginMagnetize() {
			renderer.material.color = Color.yellow;
		}

		void IEndMagnetizeHandler.OnEndMagnetize() {
			renderer.material.color = Color.grey;
		}
		#endregion
	}
}
