
namespace Andtech.Tasking {

	/// <summary>
	/// Provides messages for processing within the task system.
	/// </summary>
	public interface ITaskObserver {

		/// <summary>
		/// Message called after an EXECUTE operation.
		/// </summary>
		void OnPostExecute();

		/// <summary>
		/// Message called after an UNDO operation.
		/// </summary>
		void OnPostUndo();

		/// <summary>
		/// Message called after a REDO operation.
		/// </summary>
		void OnPostRedo();
	}
}
