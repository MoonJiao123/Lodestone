using UnityEngine;

namespace Anctech {

	/// <summary>
	/// Unpacks a hierarchy by placing all ancestors on the same level.
	/// </summary>
	public class Unpacker : MonoBehaviour {

		public void Unpack() {
			Unpack(transform, transform);
		}

		#region PIPELINE
		private void Unpack(Transform root, Transform current) {
			// Helper locals
			int childCount = current.childCount;
			
			// Iterate through all children.
			for (int i = 0; i < childCount; i++) {
				// Always use the 0th child
				// Once this child is processed, the 1st child becomes the 0th
				Transform child = current.GetChild(0);

				// Reorganize heirarchy
				child.SetParent(root);
				child.SetAsLastSibling();

				// Prepend path to GameObject's name
				if (!ReferenceEquals(root, current))
					child.name = string.Format("{0}/{1}", current.name, child.name);

				// Unpack child's children
				Unpack(root, child);
			}
		}
		#endregion PIPELINE
	}
}
