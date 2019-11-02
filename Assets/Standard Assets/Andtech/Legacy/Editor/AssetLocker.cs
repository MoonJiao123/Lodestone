#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Andtech {

	/// <summary>
	/// Editor class which allows locking (i.e. marking files as read-only) of Standard Assets.
	/// </summary>
	public class AssetLocker : MonoBehaviour {
		public static readonly string path = "Assets/Standard Assets";

		[MenuItem("Andtech/Lock Standard Assets")]
		public static void LockAll() {
			SetIsReadOnlyAll(path, true);
			Say("All Standard Assets locked!", "Dismiss");
		}

		[MenuItem("Andtech/Unlock Standard Assets")]
		public static void UnlockAllInteractive() {
			if (Ask("Are you sure you want to unlock all Standard Assets?", "Yes", "No")) {
				SetIsReadOnlyAll(path, false);
				if (!Say("All Standard Assets unlocked", "Dismiss"))
					return;
			}
		}

		#region PIPELINE
		private static void SetIsReadOnlyAll(string root, bool isReadOnly) {
			foreach (string guid in AssetDatabase.FindAssets(string.Empty, new string[] { root })) {
				string path = AssetDatabase.GUIDToAssetPath(guid);

				FileInfo fileInfo = new FileInfo(path) {
					IsReadOnly = isReadOnly
				};
			}
		}

		private static bool Say(string statement, string response, string title = "Asset Locker") {
			return EditorUtility.DisplayDialog(title, statement, response);
		}

		private static bool Ask(string question, string optionPositive, string optionNegative, string title = "Asset Locker") {
			return EditorUtility.DisplayDialog(title, question, optionPositive, optionNegative);
		}
		#endregion PIPELINE
	}
}
#endif
