using System;
using UnityEngine;

namespace PhysicsSystem {

	public class MagnetAffector : MonoBehaviour {
		[SerializeField]
		private Magnet magnet;

		#region MONOBEHAVIOUR
		protected virtual void Update() {
			if (magnet ? (magnet.Raycaster ? magnet.Raycaster.HasTarget : false) : false) {
				var target = magnet.Raycaster.HitInfo.collider.gameObject.GetComponentInParent<MagnetTarget>();
				if (target)
					FoundTarget?.Invoke(target);
				else
					NoTarget?.Invoke();
			}
			else
				NoTarget?.Invoke();
		}
		#endregion

		#region VIRTUAL
		public virtual Vector3 Position {
			get => transform.position;
			set => transform.position = value;
		}
		#endregion

		#region EVENT
		public event Action<MagnetTarget> FoundTarget;
		public event Action NoTarget;
		#endregion
	}
}
