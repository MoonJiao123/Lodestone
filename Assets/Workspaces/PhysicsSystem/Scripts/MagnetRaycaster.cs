using Andtech;
using UnityEngine;

namespace PhysicsSystem {

	public class MagnetRaycaster : MonoBehaviour {
		public bool HasTarget { get; private set; }
		public RaycastHit HitInfo { get; private set; }
		public Ray Ray { get; private set; }

		[SerializeField]
		private Transform actionPoint;
		[SerializeField]
		private LayerMask collisionMask;
		[SerializeField]
		private float maxDistance;

		#region MONOBEHAVIOUR
		protected virtual void Update() {
			Ray ray = new Ray(actionPoint.position, actionPoint.forward);
			Ray = ray;
			HasTarget = Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, collisionMask);
			HitInfo = hitInfo;

			if (HasTarget)
				Debug.DrawLine(ray.origin, hitInfo.point, Color.green.Alpha(0.25F));
			else
				Debug.DrawRay(ray.origin, ray.GetPoint(maxDistance), Color.red.Alpha(0.25F));
		}
		#endregion
	}
}
