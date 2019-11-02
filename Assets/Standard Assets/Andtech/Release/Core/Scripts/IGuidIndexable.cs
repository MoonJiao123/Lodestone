using System;

namespace Andtech.Core {
	
	/// <summary>
	/// Indicates that the type can be indexed by GUID.
	/// </summary>
	public interface IGuidIndexable {

		/// <summary>
		/// The GUID associated with the instance.
		/// </summary>
		Guid Guid {
			get;
		}
	}
}
