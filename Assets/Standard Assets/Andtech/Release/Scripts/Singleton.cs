
namespace Andtech {

	/// <summary>
	/// Base class for defining singletons.
	/// </summary>
	/// <typeparam name="T">The type of the singleton instance.</typeparam>
	public abstract class Singleton<T> where T : Singleton<T> {
		public static T Instance {
			get => instance as T;
			set => instance = value;
		}
		public static bool HasInstance => !(instance is null);

		private static T instance;
	}
}
