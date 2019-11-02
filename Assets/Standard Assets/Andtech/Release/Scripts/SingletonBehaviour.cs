using UnityEngine;

namespace Andtech {

	/// <summary>
	/// Base class for defining singleton MonoBehaviours.
	/// </summary>
	/// <typeparam name="T">The type of the singleton instance.</typeparam>
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T> {
		public static T Current {
			get => current as T;
			set => current = value;
		}
		public static bool HasSingleton => !(current is null);

		private static MonoBehaviour current;

		#region MONOBEHAVIOUR
		protected virtual void OnEnable() {
			Current = (T)this;
		}

		protected virtual void OnDisable() {
			if (ReferenceEquals(Current, this))
				Current = null;
		}
		#endregion MONOBEHAVIOUR
	}
}
