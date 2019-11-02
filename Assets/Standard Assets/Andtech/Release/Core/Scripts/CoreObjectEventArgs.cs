using System;

namespace Andtech.Core {

	/// <summary>
	/// Event data for use with core objects.
	/// </summary>
	public class CoreObjectEventArgs : EventArgs, IGuidIndexable {
		public readonly CoreObject CoreObject;

		public CoreObjectEventArgs(CoreObject coreObject) {
			this.CoreObject = coreObject;
		}

		#region INTERFACE
		/// <summary>
		/// The GUID associated with the core object.
		/// </summary>
		Guid IGuidIndexable.Guid => (CoreObject as IGuidIndexable).Guid;
		#endregion INTERFACE
	}
}
