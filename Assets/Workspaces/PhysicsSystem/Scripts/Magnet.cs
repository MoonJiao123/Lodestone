using UnityEngine;

namespace PhysicsSystem {

	public class Magnet : MonoBehaviour {
		[SerializeField]
		private MagnetHolder holder;
		[SerializeField]
		private MagnetRaycaster raycaster;
		[SerializeField]
		private MagnetAffector affector;
		[SerializeField]
		private MagnetMediator mediator;

		public MagnetHolder Holder => holder;
		public MagnetRaycaster Raycaster => raycaster;
		public MagnetAffector Affector => affector;
		public MagnetMediator Mediator => mediator;
	}
}
