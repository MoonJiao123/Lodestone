using System;

namespace Andtech.Tasking {
	
	/// <summary>
	/// Represents an invertible task.
	/// </summary>
	public interface ITask {
		/// <summary>
		/// The normal (forward) operation.
		/// </summary>
		Action Action {
			get;
		}
		
		/// <summary>
		/// The inverse operation.
		/// </summary>
		Action ActionInverse {
			get;
		}
	}
}
