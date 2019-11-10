using UnityEngine;

namespace Andtech {

	public class Mover : MonoBehaviour {
		[SerializeField]
		private string axisNameHorizontal = "Horizontal";
		[SerializeField]
		private string axisNameVertical = "Vertical";
		[SerializeField]
		private CharacterController controller;
		[SerializeField]
		private Transform head;

		private Vector2 input;
		[SerializeField]
		private float speed;

		#region MONOBEHAVIOUR
		protected virtual void Reset() {
			controller = GetComponentInChildren<CharacterController>();
			speed = 3.0F;
		}

		protected virtual void Update() {
			input.x = Input.GetAxisRaw(axisNameHorizontal);
			input.y = Input.GetAxisRaw(axisNameVertical);
		}

		protected virtual void LateUpdate() {
		}

		protected virtual void FixedUpdate() {
			Vector3 localDirection = new Vector3 {
				x = input.x,
				y = 0.0F,
				z = input.y
			};
			Vector3 localVelocity = localDirection * speed;
			Quaternion rotation = Quaternion.LookRotation(VectorUtility.ProjectOnPlaneY(head.forward));
			Vector3 velocity = rotation * localVelocity;

			controller.Move(velocity * Time.fixedDeltaTime);
		}
		#endregion
	}
}
