
namespace Andtech {

	/// <summary>
	/// Indicates that objects of this type require initialization.
	/// </summary>
	public interface IInitializable {

		/// <summary>
		/// Presets the object to an initial state.
		/// </summary>
		void Initialize();
	}
}
