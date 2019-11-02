using System;

namespace Andtech.Core {

	/// <summary>
	/// Event data for use in core data processing.
	/// </summary>
	public class DataProcessEventArgs : EventArgs, IGuidIndexable {
		public readonly DataOp DataOp;

		private readonly Guid guid;

		public DataProcessEventArgs(Guid guid) : this(guid, default) { }

		public DataProcessEventArgs(Guid guid, DataOp dataOp) {
			this.guid = guid;
			DataOp = dataOp;
		}

		#region INTERFACE
		public Guid Guid => guid;
		#endregion INTERFACE
	}
}
