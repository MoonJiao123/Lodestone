
namespace Andtech.Pooling {

	public interface IPoolObserver {

		/// <summary>
		/// Invoked after the object had been dispatched by the pooling system.
		/// </summary>
		void OnDispatch();

		/// <summary>
		/// Invoked after the object had been reclaimed by the pooling system.
		/// </summary>
		void OnReclaim();
	}
}
