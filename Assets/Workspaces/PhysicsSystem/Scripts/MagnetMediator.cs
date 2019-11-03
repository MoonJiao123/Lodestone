using Andtech;
using UnityEngine;

namespace PhysicsSystem {

	public class MagnetMediator : MonoBehaviour {
		[SerializeField]
		private Magnet magnet;

		[SerializeField]
		private float push;

		private MagnetTarget Target {
			get => target;
			set {
				bool same = ReferenceEquals(value, Target);
				if (same)
					return;

				if (target != null)
					UnlinkTarget(target);

				target = value;
				if (target != null)
					LinkTarget(target);

				#region LOCAL_FUNCTION
				void LinkTarget(MagnetTarget t) {
					t.Rigidbody.useGravity = false;
					(t as IBeginMagnetizeHandler)?.OnBeginMagnetize();
				}

				void UnlinkTarget(MagnetTarget t) {
					t.Rigidbody.useGravity = true;
					(t as IEndMagnetizeHandler)?.OnEndMagnetize();
				}
				#endregion 
			}
		}

		private MagnetTarget target;

		protected virtual void Update() {
			if (Target != null)
				Mediate(magnet, Target);
		}

		public void Mediate(Magnet magnet, MagnetTarget target) {
			Vector3 delta = target.Position - magnet.Affector.Position;
			Vector3 deltaParallel = Vector3.Project(delta, magnet.Raycaster.Ray.direction);
			Vector3 deltaPerpendicular = delta - deltaParallel;

			Debug.DrawRay(magnet.Affector.Position, delta, Color.yellow);
			Debug.DrawRay(magnet.Affector.Position, deltaParallel, Color.yellow.Alpha(0.5F));
			Debug.DrawRay(magnet.Affector.Position + deltaParallel, deltaPerpendicular, Color.yellow.Alpha(0.5F));

			if (target.Rigidbody.mass > magnet.Holder.Rigidbody.mass) {
				magnet.Holder.AddForce(deltaParallel);
			}
			else {
				target.Rigidbody.velocity = deltaParallel.normalized * push;

				(target as IMagnetizeHandler)?.OnMagnetize();
			}
		}

		#region CALLBACK
		private void HandleFoundTarget(MagnetTarget target) {
			Target = target;
		}

		private void HandleNoTarget() {
			Target = null;
		}
		#endregion

		#region PIPELINE
		protected virtual void OnEnable() {
			magnet.Affector.FoundTarget += HandleFoundTarget;
			magnet.Affector.NoTarget += HandleNoTarget;
		}

		protected virtual void OnDisable() {
			magnet.Affector.FoundTarget -= HandleFoundTarget;
			magnet.Affector.NoTarget -= HandleNoTarget;
		}
		#endregion
	}
}
