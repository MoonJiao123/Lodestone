using System;

namespace Andtech.Core {

	/// <summary>
	/// Standard base class for data.
	/// </summary>
	public abstract class Data : ICloneable, IGuidIndexable {
		private readonly Guid guid;

		public Data(Guid guid) {
			this.guid = guid;
		}

		#region OVERRIDE
		public override int GetHashCode() => guid.GetHashCode();
		#endregion OVERRIDE

		#region INTERFACE
		/// <summary>
		/// The Globally Unique Identifier (GUID) of this data.
		/// </summary>
		public Guid Guid => guid;

		public virtual object Clone() {
			return MemberwiseClone();
		}
		#endregion INTERFACE
	}
}
