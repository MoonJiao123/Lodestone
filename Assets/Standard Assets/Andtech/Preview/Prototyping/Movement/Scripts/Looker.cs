using UnityEngine;

namespace Andtech {

	public class Looker : MonoBehaviour {
		[SerializeField]
		private string axisNameHorizontal = "Mouse X";
		[SerializeField]
		private string axisNameVertical = "Mouse Y";
		[SerializeField]
		private Transform yawAnchor;
		[SerializeField]
		private Transform pitchAnchor;

		[SerializeField]
		private float sensitivityX = 720.0F;
		[SerializeField]
		private float sensitivityY = 720.0F;

		private Vector2 input;

		#region MONOBEHAVIOUR
		protected virtual void Start() {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
		}

		protected virtual void Update() {
			input.x = Input.GetAxisRaw(axisNameHorizontal);
			input.y = Input.GetAxisRaw(axisNameVertical);
		}

		protected virtual void LateUpdate() {
			// Compute frame velocity
			Vector3 velocity = new Vector3 {
				x = sensitivityX * -input.y,
				y = sensitivityY * input.x
			};

			// Compute yaw
			Vector3 yaw = yawAnchor.localEulerAngles;
			yaw.y += velocity.y * Time.smoothDeltaTime;
			// Write back to transform
			yawAnchor.localEulerAngles = yaw;

			// Compute pitch
			Vector3 pitch = pitchAnchor.localEulerAngles;
			pitch.x += velocity.x * Time.smoothDeltaTime;
			pitch.x = Mathf.Clamp(NormalizeAngle(pitch.x), -89.0F, 89.0F);

			// Write back to transform
			pitchAnchor.localEulerAngles = pitch;

			#region LOCAL_FUNCTIONS
			float NormalizeAngle(float angle) {
				angle = Mathf.Repeat(angle, 360.0F);
				if (angle > 180.0F)
					angle = -(360.0F - angle);

				return angle;
			}
			#endregion
		}
		#endregion MONOBEHAVIOUR
	}
}
