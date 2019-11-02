using System;
using System.Collections;
using System.Collections.Generic;

namespace Andtech.Tasking {

	/// <summary>
	/// Collection of tasks which should be executed together.
	/// </summary>
	public class MultiTask : IEnumerable<ITask>, ITaskObserver {
		private ICollection<ITask> tasks;

		public MultiTask() {
			tasks = new LinkedList<ITask>();
		}

		public void Add(ITask task) {
			tasks.Add(task);
		}

		#region INTERFACE
		public Action Action {
			get {
				return () => {
					foreach (ITask task in tasks) {
						task.Action();
					}
				};
			}
		}
		public Action ActionInverse {
			get {
				return () => {
					foreach (ITask task in tasks) {
						task.ActionInverse();
					}
				};
			}
		}

		public void OnPostExecute() {
			foreach (ITask task in tasks) {
				(task as ITaskObserver)?.OnPostExecute();
			}
		}

		public void OnPostUndo() {
			foreach (ITask task in tasks) {
				(task as ITaskObserver)?.OnPostUndo();
			}
		}

		public void OnPostRedo() {
			foreach (ITask task in tasks) {
				(task as ITaskObserver)?.OnPostRedo();
			}
		}

		public void OnRelease() {
			foreach (ITask task in tasks) {
				(task as ITaskManagerObserver).OnRelease();
			}
		}

		public IEnumerator<ITask> GetEnumerator() {
			return tasks.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		#endregion INTERFACE
	}
}
