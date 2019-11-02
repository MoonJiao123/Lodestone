using UnityEngine;

namespace Andtech {

	/// <summary>
	/// Smoothly blends (interpolates) between 2 transform hierarchies.
	/// </summary>
	public class TransformInterpolator : MonoBehaviour {
		public Transform[] masters;
		public Transform[] slaves;

		[Header("Parameters")]
		public bool useMasterTransforms;
		public float smoothTimeLinear;
		public float smoothTimeAngular;

		[Header("Debugging")]
		public bool bypassInterpolation;

		private Vector3 velocityLinear;
		private Vector3 velocityAngular;

		protected virtual void LateUpdate() {
			// Helper locals
			int n = slaves.Length;
			
			for (int i = 0; i < n; i++) {
				// Helper locals
				Transform master = masters[i];
				Transform slave = slaves[i];

				if (bypassInterpolation) {
					// Copy transform values
					slave.position = master.position;
					slave.rotation = master.rotation;
				}
				else {
					// Move the slave's position towards the master's position
					slave.localPosition = Vector3.SmoothDamp(slave.localPosition, master.localPosition, ref velocityLinear, smoothTimeLinear);

					// Rotate the slave's position towards the master's rotation
					Vector3 localEulerAnglesMaster = master.localEulerAngles;
					Vector3 localEulerAnglesSlave = slave.localEulerAngles;
					Vector3 localEulerAngles = new Vector3() {
						x = Mathf.SmoothDampAngle(localEulerAnglesSlave.x, localEulerAnglesMaster.x, ref velocityAngular.x, smoothTimeAngular),
						y = Mathf.SmoothDampAngle(localEulerAnglesSlave.y, localEulerAnglesMaster.y, ref velocityAngular.y, smoothTimeAngular),
						z = Mathf.SmoothDampAngle(localEulerAnglesSlave.z, localEulerAnglesMaster.z, ref velocityAngular.z, smoothTimeAngular)
					};

					slave.localEulerAngles = localEulerAngles;

				}
			}
		}
	}
}
