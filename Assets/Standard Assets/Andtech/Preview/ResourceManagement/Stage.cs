using System;

namespace Andtech.ResourceManagement {

	/// <summary>
	/// Base class for exposed storage space.
	/// </summary>
	public class Stage {
		/// <summary>
		/// Uploads any changes made to the stage.
		/// </summary>
		public void Apply() {
			OnApply(EventArgs.Empty);
		}

		#region EVENT
		/// <summary>
		/// Triggered whenever the stage is updated.
		/// </summary>
		public event EventHandler Applied;

		protected virtual void OnApply(EventArgs e) => Applied?.Invoke(this, e);
		#endregion EVENT
	}
}
