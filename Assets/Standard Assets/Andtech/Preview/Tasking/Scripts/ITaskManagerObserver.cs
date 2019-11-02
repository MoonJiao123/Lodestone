
namespace Andtech.Tasking {

	public interface ITaskManagerObserver {

		/// <summary>
		/// Message called after the task is released from an undo/redo system.
		/// </summary>
		void OnRelease();
	}
}
