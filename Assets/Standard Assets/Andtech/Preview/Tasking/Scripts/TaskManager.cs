using Andtech.Tasking.Collections;
using System;

namespace Andtech.Tasking {

	/// <summary>
	/// Module for proper task processing.
	/// </summary>
	public class TaskManager {
		/// <summary>
		/// Is there a task available to be undone?
		/// </summary>
		public bool CanUndo {
			get {
				return stack.CanPop;
			}
		}
		/// <summary>
		/// Is there a task available to be redone?
		/// </summary>
		public bool CanRedo {
			get {
				return stack.CanRestore;
			}
		}

		private readonly HistoryStack<ITask> stack;

		public TaskManager(int capacity) {
			stack = new HistoryStack<ITask>(capacity);
			stack.Released += ProcessReleased;
		}

		#region VIRTUAL
		/// <summary>
		/// Executes a single task.
		/// </summary>
		/// <param name="task">The task to execute.</param>
		public virtual void Execute(ITask task) {
			// Add to collection
			stack.Push(task);

			// Apply execute
			task.Action();
			(task as ITaskObserver)?.OnPostExecute();

			// Fire events
			OnExecute(EventArgs.Empty);
		}

		/// <summary>
		/// Inverts the most recent task.
		/// </summary>
		public virtual void Undo() {
			HistoryStackElement<ITask> element = stack.Pop();

			// Apply undo
			bool invokeInverseAction = element.inverted;
			if (invokeInverseAction)
				element.value.ActionInverse();
			else
				element.value.Action();
			(element.value as ITaskObserver)?.OnPostUndo();

			// Fire events
			OnUndone(EventArgs.Empty);
		}

		/// <summary>
		/// Re-executes a recently undone task.
		/// </summary>
		public virtual void Redo() {
			HistoryStackElement<ITask> element = stack.Restore();

			// Apply redo
			bool invokeInverseAction = !element.inverted;
			if (invokeInverseAction)
				element.value.ActionInverse();
			else
				element.value.Action();
			(element.value as ITaskObserver)?.OnPostRedo();

			// Fire events
			OnRedone(EventArgs.Empty);
		}
		#endregion VIRTUAL

		#region CALLBACK
		protected virtual void ProcessReleased(object sender, EventArgs e) {
			if (e is HistoryStackEventArgs<ITask> args) {
				(args.value as ITaskManagerObserver)?.OnRelease();
			}
		}
		#endregion CALLBACK

		#region EVENT
		public event EventHandler Executed;
		public event EventHandler Undone;
		public event EventHandler Redone;

		protected virtual void OnExecute(EventArgs e) => Executed?.Invoke(this, e);

		protected virtual void OnUndone(EventArgs e) => Undone?.Invoke(this, e);

		protected virtual void OnRedone(EventArgs e) => Redone?.Invoke(this, e);
		#endregion EVENT
	}
}
