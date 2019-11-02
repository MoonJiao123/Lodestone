using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Andtech.InputSystem {

	public class UIKeystrokeModule : KeystrokeModule {
		private EventSystem EventSystem {
			get {
				if (eventSystem == null)
					eventSystem = EventSystem.current;

				return eventSystem;
			}
		}

		private EventSystem eventSystem;

		#region OVERRIDE
		protected override void Update() {
			bool blocked = EventSystem.IsPointerOverGameObject();
			bool selecting = EventSystem.currentSelectedGameObject;

			KeystrokeModifier modifier = GetKeystrokeModifier();
			foreach (KeyValuePair<KeystrokeCondition, Action> pair in actions) {
				KeystrokeCondition condition = pair.Key;
				Action action = pair.Value;

				if (modifier.Equals(condition.Modifier) && condition.Test()) {
					if (condition.UIOcclusion) {
						if (!blocked)
							action();
					}
					else {
						if (!selecting)
							action();
					}
				}
			}
		}
		#endregion OVERRIDE
	}
}
