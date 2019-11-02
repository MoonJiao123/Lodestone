using System;

namespace Andtech {

	public interface IHighlightable {

		#region EVENT
		event Action RequestedHighlight;
		event Action RequestedDehighlight;
		#endregion
	}
}
