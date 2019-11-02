#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Andtech {

	[CustomPropertyDrawer(typeof(Rotation))]
	public class RotationPropertyDrawer : PropertyDrawer {
		public readonly float preferredHeight = 18;
		public readonly float alpha = 0.4175F;

		// OVERRIDE
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			// Needed for sharing the inspector with other properties
			EditorGUI.BeginProperty(position, label, property);

			// Compute the positions/sizes of each GUI element
			Rect turnsLabelRect = new Rect(position.x, position.y, 0.5F * position.width, position.height);
			Rect turnsRect = new Rect(position.x + alpha * position.width, position.y, (1.0F - alpha) * position.width, position.height);

			// Display the editable sub-properties
			EditorGUI.LabelField(turnsLabelRect, new GUIContent(property.displayName, "The number of clockwise turns"));

			// Examine sub-properties
			int value = EditorGUI.IntSlider(turnsRect, GUIContent.none, property.FindPropertyRelative("turns").intValue, 0, Rotation.MAX - 1);
			property.FindPropertyRelative("turns").intValue = value;

			// Needed for sharing the inspector with other properties
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return preferredHeight;
		}
	}
}

#endif
