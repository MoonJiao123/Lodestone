#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Andtech.Bezier.Editor {

	[CustomPropertyDrawer(typeof(ControlPoint))]
	public class ControlPointPropertyDrawer : PropertyDrawer {
		private bool expanded;
		private readonly float preferredHeight = 18;

		// OVERRIDE
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			// Needed for sharing the inspector with other properties
			EditorGUI.BeginProperty(position, label, property);

			// Compute the positions/sizes of each GUI element
			int index = 0;
			Rect foldoutRect = GetRect(position, index++);
			Rect positionLabelRect = GetRect(position, index++);
			Rect positionRect = GetRect(position, index++);
			Rect handlerLabelRect = GetRect(position, index++);
			Rect handlerRect = GetRect(position, index++);
			Rect normalRect = GetRect(position, index++);
			Rect tangentRect = GetRect(position, index++);
			Rect binormalRect = GetRect(position, index++);

			// Only display the sub-properties when needed
			expanded = EditorGUI.Foldout(foldoutRect, expanded, new GUIContent(property.displayName, "A bezier control point"), true);
			if (expanded) {
				EditorGUI.BeginChangeCheck();

				// Display the editable sub-properties
				EditorGUI.LabelField(positionLabelRect, new GUIContent("Position", "The position"));
				EditorGUI.PropertyField(positionRect, property.FindPropertyRelative("position"), GUIContent.none);
				EditorGUI.LabelField(handlerLabelRect, new GUIContent("Handler", "The handler vector"));
				EditorGUI.PropertyField(handlerRect, property.FindPropertyRelative("handler"), GUIContent.none);
				bool changed = EditorGUI.EndChangeCheck();

				if (changed)
					RecalculateTNB(property);

				// Display the inferred control point parameters
				EditorGUI.DropShadowLabel(normalRect, "Normal: " + property.FindPropertyRelative("normal").vector3Value);
				EditorGUI.DropShadowLabel(tangentRect, "Tangent: " + property.FindPropertyRelative("tangent").vector3Value);
				EditorGUI.DropShadowLabel(binormalRect, "Binormal: " + property.FindPropertyRelative("binormal").vector3Value);
			}

			// Needed for sharing the inspector with other properties
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			if (expanded)
				return 8 * preferredHeight;

			else
				return preferredHeight;
		}

		// PIPELINE
		private void RecalculateTNB(SerializedProperty property) {
			Vector3 handler = property.FindPropertyRelative("handler").vector3Value;
			Vector3 tangent = handler.normalized;
			Vector3 cross = Vector3.Cross(Vector3.up, tangent);
			Vector3 normal = Vector3.Cross(tangent, cross);
			Vector3 binormal = Vector3.Cross(normal, tangent);

			// Set the control point parameters
			property.FindPropertyRelative("normal").vector3Value = normal;
			property.FindPropertyRelative("tangent").vector3Value = tangent;
			property.FindPropertyRelative("binormal").vector3Value = binormal;
		}

		private Rect GetRect(Rect position, int index) {
			return new Rect(position.x, position.y + index * preferredHeight, position.width, preferredHeight);
		}
	}
}

#endif
