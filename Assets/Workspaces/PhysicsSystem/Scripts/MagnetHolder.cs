using UnityEngine;

namespace PhysicsSystem {

	public class MagnetHolder : MonoBehaviour {
		[SerializeField]
		private Rigidbody rigidbody;
		[SerializeField]
		private CharacterController controller;

		public Rigidbody Rigidbody => rigidbody;
		public CharacterController Controller => controller;

		public void AddForce(Vector3 force) {
			Controller.Move(force * Time.deltaTime);
		}
	}
}
