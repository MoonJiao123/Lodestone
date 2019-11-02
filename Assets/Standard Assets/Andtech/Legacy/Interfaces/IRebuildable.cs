
namespace Andtech {

	/// <summary>
	/// Indicates that objects of this type must be manually reconstructed.
	/// </summary>
	public interface IRebuildable {

		/// <summary>
		/// Forces the instance to reconstruct itself.
		/// </summary>
		void Rebuild();
	}
}
