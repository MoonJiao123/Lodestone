using System;
using UnityEngine;
using UnityEngine.Events;

namespace Andtech {

	public class UnityInitializer : MonoBehaviour {
		/// <summary>
		/// How should the initializer be invoked?
		/// </summary>
		[Tooltip("How should the initializer be invoked?")]
		public Invocation initializationMode;
		/// <summary>
		/// List of initialization methods.
		/// </summary>
		[Tooltip("List of initialization methods.")]
		public UnityEvent onInitialize;

		public void Initialize() {
			try {
				onInitialize.Invoke();
			}
			catch (Exception ex) {
				Debug.LogFormat(gameObject, "Initializer failed {0}", ex.Message);
			}
		}

		#region MONOBEHAVIOUR
		protected virtual void Awake() {
			if (initializationMode == Invocation.OnAwake)
				Initialize();
		}

		protected virtual void Start() {
			if (initializationMode == Invocation.OnStart)
				Initialize();
		}
		#endregion MONOBEHAVIOUR
	}
}
