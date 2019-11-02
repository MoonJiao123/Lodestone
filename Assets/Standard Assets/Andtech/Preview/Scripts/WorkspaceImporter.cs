#if UNITY_EDITOR
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Andtech.Preview {
	
	/// <summary>
	/// Helper for quickly importing assets.
	/// </summary>
	public class WorkspaceImporter : MonoBehaviour {
		private static readonly string pathWorkspaces = string.Format("{0}/Workspaces/", Application.dataPath.Replace("/Assets", string.Empty));
		private static readonly Regex regexPath = new Regex(@"(?<directory>[\w/\\]*)[/\\](?<file>[\w. ]*)");

		[MenuItem("Andtech/Link Workspace", priority = 10)]
		public static void Link() {
			// Link workspaces
			string directorySource = GetPathSource();
			string directoryDestination = GetPathDestination();

			// Write settings to file
			Save("DEFAULT", directorySource, directoryDestination);
		}

		[MenuItem("Andtech/Import Workspace &1", priority = 10)]
		public static void Import() {
			string path = string.Format("{0}/{1}", pathWorkspaces, "DEFAULT.work");
			Load(path, out string directorySource, out string directoryDestination);
			
			// Import workspace assets
			Synchronize(directorySource, directoryDestination);
		}

		#region PIPELINE
		/// <summary>
		/// Synchronize the workspace.
		/// </summary>
		private static void Synchronize(string directorySource, string directoryDestination) {
			foreach (string path in Directory.GetFiles(directorySource)) {
				Tokenize(path, out string directory, out string file);

				string pathOutput = string.Format("{0}/{1}", directoryDestination, file);
				bool exists = File.Exists(pathOutput);

				// Clear existing prefabs
				if (exists)
					File.Delete(pathOutput);

				// Import the asset
				FileUtil.CopyFileOrDirectory(path, pathOutput);

				// Debugging
				if (exists)
					Debug.LogFormat("Reimported {0}", file);
				else
					Debug.LogFormat("Imported {0}", file);
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		/// <summary>
		/// Obtains a source path from the user.
		/// </summary>
		/// <returns>The location of the source workspace.</returns>
		private static string GetPathSource() {
			return EditorUtility.OpenFolderPanel("Select source directory", GetCurrentProjectPath(), "");
		}

		/// <summary>
		/// Obtains a destination path from the user.
		/// </summary>
		/// <returns>The location of the destination workspace.</returns>
		private static string GetPathDestination() {
			string folder = GetCurrentProjectPath();

			return EditorUtility.OpenFolderPanel("Select destination directory", folder, "");
		}

		/// <summary>
		/// Attempts to acquire the current location (in the Project view).
		/// </summary>
		/// <returns>A path within the project.</returns>
		private static string GetCurrentProjectPath() {
			string path = Application.dataPath;

			// Analyze the selection to find the location
			foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
				path = AssetDatabase.GetAssetPath(obj);
				if (string.IsNullOrEmpty(path) || !File.Exists(path))
					continue;
				
				path = Path.GetDirectoryName(path);
				break;
			}
			return path;
		}

		/// <summary>
		/// Parse a path string.
		/// </summary>
		/// <param name="path">The string containing the tokens.</param>
		/// <param name="directory">The directory.</param>
		/// <param name="file">The filename.</param>
		private static void Tokenize(string path, out string directory, out string file) {
			Match match = regexPath.Match(path);
			Group group;
			// Extract directory path
			group = match.Groups["directory"];
			directory = group.Value;

			// Extract file path
			group = match.Groups["file"];
			file = group.Value;
		}

		private static void Save(string name, string directorySource, string directoryDestination) {
			// Check for directory
			if (!Directory.Exists(pathWorkspaces))
				Directory.CreateDirectory(pathWorkspaces);
			// Write text to file
			string path = string.Format("{0}/{1}.work", pathWorkspaces, name);
			using (StreamWriter writer = new StreamWriter(path)) {
				writer.WriteLine(directorySource);
				writer.WriteLine(directoryDestination);
			}
		}

		private static void Load(string path, out string directorySource, out string directoryDestination) {
			using (StreamReader reader = new StreamReader(path)) {
				directorySource = reader.ReadLine();
				directoryDestination = reader.ReadLine();
			}
		}
		#endregion PIPELINE
	}
}
#endif
