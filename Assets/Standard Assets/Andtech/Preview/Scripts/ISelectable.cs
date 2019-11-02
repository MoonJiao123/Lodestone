using System;

namespace Andtech {

	public interface ISelectable {

		#region EVENT
		event Action RequestedSelect;
		event Action RequestedDeselect;
		#endregion
	}
}
