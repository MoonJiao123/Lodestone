using Andtech.Core;
using System;

namespace Andtech.Tasking {

	/// <summary>
	/// Base class for standard database operational tasks.
	/// </summary>
	public class DatabaseTask<T> : ITask, ITaskObserver, ITaskManagerObserver, IGuidIndexable where T : IGuidIndexable {
		protected readonly Database<T> database;
		protected readonly T data;
		protected readonly T dataPreserved;
		protected readonly CrudOperation operation;

		protected DatabaseTask(Database<T> database, T data, T dataPreserved, CrudOperation operation) {
			this.database = database;
			this.data = data;
			this.dataPreserved = dataPreserved;
			this.operation = operation;
		}

		/// <summary>
		/// Automatically creates a CREATE database task.
		/// </summary>
		/// <param name="database">The destination database.</param>
		/// <param name="data">The element to add to the database.</param>
		/// <returns>The database task.</returns>
		public static DatabaseTask<T> NewCreateTask(Database<T> database, T data) {
			return new DatabaseTask<T>(database, data, default, CrudOperation.Create);
		}

		/// <summary>
		/// Automatically creates an UPDATE database task.
		/// </summary>
		/// <param name="database">The destination database.</param>
		/// <param name="data">The element which will replace the database.</param>
		/// <returns>The database task.</returns>
		public static DatabaseTask<T> NewUpdateTask(Database<T> database, T data) {
			T dataPreserved = database.Get(data.Guid);

			return new DatabaseTask<T>(database, data, dataPreserved, CrudOperation.Update);
		}

		/// <summary>
		/// Automatically creates a DELETE database task.
		/// </summary>
		/// <param name="database">The destination database.</param>
		/// <param name="data">The element to be deleted from the database.</param>
		/// <returns>The database task.</returns>
		public static DatabaseTask<T> NewDeleteTask(Database<T> database, T data) {
			return new DatabaseTask<T>(database, data, default, CrudOperation.Delete);
		}

		#region VIRTUAL
		/// <summary>
		/// This is how the task creates an element in the database.
		/// </summary>
		protected virtual void Create() {
			database.Add(data);
		}

		/// <summary>
		/// This is how the task updates an element in the database.
		/// </summary>
		protected virtual void Update() {
			database.Remove(Guid);
			database.Add(data);
		}

		/// <summary>
		/// This is how the task reverts an element in the database.
		/// </summary>
		protected virtual void Revert() {
			database.Remove(Guid);
			database.Add(dataPreserved);
		}

		/// <summary>
		/// This is how the task deletes an element from the database.
		/// </summary>
		protected virtual void Delete() {
			database.Remove(Guid);
		}
		#endregion VIRTUAL

		#region INTERFACE
		/// <summary>
		/// The GUID associated with the task.
		/// </summary>
		public Guid Guid {
			get {
				return data.Guid;
			}
		}
		public Action Action {
			get {
				switch (operation) {
					case CrudOperation.Create:
						return Create;
					case CrudOperation.Update:
						return Update;
					case CrudOperation.Delete:
						return Delete;
				}

				return () => { };
			}
		}
		public Action ActionInverse {
			get {
				switch (operation) {
					case CrudOperation.Create:
						return Delete;
					case CrudOperation.Update:
						return Revert;
					case CrudOperation.Delete:
						return Create;
				}

				return () => { };
			}
		}

		/// <summary>
		/// Message called after an EXECUTE operation.
		/// </summary>
		public virtual void OnPostExecute() {

		}

		/// <summary>
		/// Message called after an UNDO operation.
		/// </summary>
		public virtual void OnPostUndo() {

		}

		/// <summary>
		/// Message called after a REDO operation.
		/// </summary>
		public virtual void OnPostRedo() {

		}

		/// <summary>
		/// Message called after the task is released from an undo/redo system.
		/// </summary>
		public virtual void OnRelease() {

		}
		#endregion INTERFACE
	}
}
