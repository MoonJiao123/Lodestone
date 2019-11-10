using UnityEngine;

namespace Andtech {

	/// <summary>
	/// Base class for defining singleton MonoBehaviours.
	/// </summary>
	/// <typeparam name="T">The type of the singleton instance.</typeparam>
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T> {
		public static T Instance {
			get => instance as T;
			set => instance = value;
		}
		public static bool HasInstance => !(instance is null);

		private static T instance;

		#region MONOBEHAVIOUR
		protected virtual void OnEnable() {
			Instance = (T)this;
		}

		protected virtual void OnDisable() {
			if (ReferenceEquals(Instance, this))
				Instance = null;
		}
		#endregion MONOBEHAVIOUR
	}
}
