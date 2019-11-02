
namespace Andtech {

	/// <summary>
	/// Indicates that objects of this type can appear and disappear.
	/// </summary>
	public interface IRenderable {

		/// <summary>
		/// Make the object appear.
		/// </summary>
		void Appear();

		/// <summary>
		/// Make the object appear instantly (no animation).
		/// </summary>
		void AppearImmediate();

		/// <summary>
		/// Make the object disappear.
		/// </summary>
		void Disappear();

		/// <summary>
		/// Make the object disappear instantly (no animation).
		/// </summary>
		void DisappearImmediate();
	}
}
