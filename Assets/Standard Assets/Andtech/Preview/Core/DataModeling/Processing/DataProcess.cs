using System;

namespace Andtech.Core {

	/// <summary>
	/// Representation of a data's lifecycle.
	/// </summary>
	public class DataProcess : IGuidIndexable {
		/// <summary>
		/// Has the data never been created?
		/// </summary>
		private bool nascent;
		/// <summary>
		/// Is the data currently created?
		/// </summary>
		private bool exists;

		/// <summary>
		/// The GUID associated with the process.
		/// </summary>
		private readonly Guid guid;

		public DataProcess(Guid guid) {
			this.guid = guid;
			nascent = true;
			exists = false;
		}

		#region VIRTUAL
		/// <summary>
		/// Relays a DELETE event to the process.
		/// </summary>
		public virtual void OnCreate() => OnCreate(new DataOp(CrudOperation.Create));

		/// <summary>
		/// Relays a CREATE event to the process.
		/// </summary>
		/// <param name="op">Information about the data operation.</param>
		public virtual void OnCreate(DataOp op) {
			bool wasExisting = exists;
			exists = true;

			var args = new DataProcessEventArgs(guid, op);
			if (nascent) {
				nascent = false;
				Created?.Invoke(args);
			}
			else {
				if (!wasExisting)
					Restored?.Invoke(args);
			}
		}

		/// <summary>
		/// Relays a DELETE event to the process.
		/// </summary>
		public virtual void OnUpdate() => OnUpdate(new DataOp(CrudOperation.Update));

		/// <summary>
		/// Relays a UPDATE event to the process.
		/// </summary>
		/// <param name="op">Information about the data operation.</param>
		public virtual void OnUpdate(DataOp op) {
			var args = new DataProcessEventArgs(guid, op);
			Updated?.Invoke(args);
		}

		/// <summary>
		/// Relays a DELETE event to the process.
		/// </summary>
		public virtual void OnDelete() => OnDelete(new DataOp(CrudOperation.Delete));

		/// <summary>
		/// Relays a DELETE event to the process.
		/// </summary>
		/// <param name="op">Information about the data operation.</param>
		public virtual void OnDelete(DataOp op) {
			bool wasExisting = exists;
			exists = false;

			if (exists) {
				var args = new DataProcessEventArgs(guid, op);
				Deleted?.Invoke(args);
			}
		}

		/// <summary>
		/// Releases the process.
		/// </summary>
		public virtual void OnRelease() {
			Released?.Invoke(guid);
		}
		#endregion VIRTUAL

		#region INTERFACE
		/// <summary>
		/// The GUID associated with the process.
		/// </summary>
		public Guid Guid => guid;
		#endregion INTERFACE

		#region EVENT
		/// <summary>
		/// Event fired once the data is created for the first time.
		/// </summary>
		public event Action<DataProcessEventArgs> Created;
		/// <summary>
		/// Event fired whenever the data changes.
		/// </summary>
		public event Action<DataProcessEventArgs> Updated;
		/// <summary>
		/// Event fired whenever the data is deleted.
		/// </summary>
		public event Action<DataProcessEventArgs> Deleted;
		/// <summary>
		/// Event fired whenever the data is restored.
		/// </summary>
		public event Action<DataProcessEventArgs> Restored;
		/// <summary>
		/// Event fired once the data is officially released.
		/// </summary>
		public event Action<Guid> Released;
		#endregion EVENT
	}
}
