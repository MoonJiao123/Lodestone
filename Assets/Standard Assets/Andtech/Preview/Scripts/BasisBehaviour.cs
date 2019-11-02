using UnityEngine;

namespace Andtech {

	public class BasisBehaviour : MonoBehaviour {
		public Basis Basis {
			get => basis;
			set {
				basis = value;
				transform.position = basis.Origin;
				transform.rotation = basis.Rotation;
				transform.localScale = basis.Scale;
			}
		}

		private Basis basis;

		#region MONOBEHAVIOUR
		protected virtual void Awake() {
			Vector3 basis0 = transform.right;
			Vector3 basis1 = transform.up;
			Vector3 basis2 = transform.forward;

			basis0 *= transform.localScale.x;
			basis1 *= transform.localScale.y;
			basis2 *= transform.localScale.z;

			Setup(basis0, basis1, basis2, transform.position);
		}

		public void Setup(Vector3 basis0, Vector3 basis1, Vector3 basis2, Vector3 origin) {
			Basis = new Basis(basis0, basis1, basis2, origin);
		}
		#endregion
	}
}
