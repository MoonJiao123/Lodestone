
namespace Andtech {

	/// <summary>
	/// Base class for defining singletons.
	/// </summary>
	/// <typeparam name="T">The type of the singleton instance.</typeparam>
	public abstract class Singleton<T> where T : Singleton<T> {
		public static T Current {
			get => current as T;
			set => current = value;
		}
		public static bool HasSingleton => !(current is null);

		private static T current;
	}
}
