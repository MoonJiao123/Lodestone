using UnityEngine;

namespace Unsorted {

	public class MouseLook : MonoBehaviour {
		[SerializeField]
		private string axisNameHorizontal = "Mouse X";
		[SerializeField]
		private string axisNameVertical = "Mouse Y";
		[SerializeField]
		private Transform target;

		[SerializeField]
		private float sensitivityX;
		[SerializeField]
		private float sensitivityY;

		private Vector2 input;
		private float yaw;
		private float pitch;

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
			Vector3 velocity = new Vector3 {
				x = sensitivityX * -input.y,
				y = sensitivityY * input.x
			};

			Vector3 eulerAngles = target.localEulerAngles;
			eulerAngles += velocity * Time.smoothDeltaTime;

			pitch += velocity.x * Time.smoothDeltaTime;
			pitch = Mathf.Clamp(pitch, -90.0F, 90.0F);


			eulerAngles.x = pitch;
			target.localEulerAngles = eulerAngles;
		}
		#endregion MONOBEHAVIOUR
	}
}
