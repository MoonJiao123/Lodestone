using UnityEngine;

namespace PhysicsSystem {

	public class Magnet : MonoBehaviour {
		[SerializeField]
		private MagnetRaycaster raycaster;
		[SerializeField]
		private MagnetAffector affector;
		[SerializeField]
		private MagnetMediator mediator;

		public MagnetRaycaster Raycaster => raycaster;
		public MagnetAffector Affector => affector;
		public MagnetMediator Mediator => mediator;
	}
}
