using System;
using UnityEngine;

namespace Andtech.Core {

	/// <summary>
	/// Surrogate object which represents data.
	/// </summary>
	public abstract class CoreObject : MonoBehaviour, IGuidIndexable {
		protected Guid targetGuid;

		#region OVERRIDE
		public override int GetHashCode() => targetGuid.GetHashCode();
		#endregion OVERRIDE

		#region VIRTUAL
		/// <summary>
		/// Associates this instance with the target data.
		/// </summary>
		/// <param name="guid">The GUID of the target data.</param>
		public virtual void Link(Guid guid) {
			targetGuid = guid;
		}
		#endregion VIRTUAL

		#region INTERFACE
		/// <summary>
		/// The GUID of the target data.
		/// </summary>
		Guid IGuidIndexable.Guid => targetGuid;
		#endregion INTERFACE
	}
}
