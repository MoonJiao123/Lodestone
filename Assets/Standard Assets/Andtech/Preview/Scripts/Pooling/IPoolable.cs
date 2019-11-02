using System;

namespace Andtech.Pooling {

	/// <summary>
	/// Indicates that the type can be pooled.
	/// </summary>
	public interface IPoolable {

		#region EVENT
		event EventHandler RequestedReclaim;
		#endregion EVENT
	}
}
