#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Andtech {

	[CustomEditor(typeof(Sequence))]
	public class SequenceCustomEditor : Editor {
		private Sequence sequence;
		private ReorderableList list;

		private static float paddingBottom = 8.0F;

		#region MONOBEHAVIOUR
		private void Awake() {
			sequence = target as Sequence;
		}

		private void OnEnable() {
			list = new ReorderableList(serializedObject, serializedObject.FindProperty("events"), true, true, true, true);
		}
		#endregion MONOBEHAVIOUR

		#region OVERRIDE
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			serializedObject.Update();
			DrawList();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}
		#endregion OVERRIDE

		#region PIPELINE
		private void DrawList() {
			list.drawHeaderCallback =
				(Rect rect) => {
					EditorGUI.LabelField(rect, "Event group order");
				};

			list.drawElementCallback =
				(Rect rect, int index, bool isActive, bool isFocused) => {
					var element = list.serializedProperty.GetArrayElementAtIndex(index);
					GUIContent label = new GUIContent(string.Format("Event group #{0}", index + 1));
					rect.y -= 2;

					EditorGUI.PropertyField(
						 new Rect(rect.x, rect.y, rect.width, EditorGUI.GetPropertyHeight(element, true)),
						 element,
						 label);
				};

			list.elementHeightCallback =
				(int index) => {
					var elementHeight = sequence.events[index].GetPersistentEventCount();
					if (elementHeight >= 1) {
						elementHeight--;
					}

					float heightA = (EditorGUIUtility.singleLineHeight * 5.0F);
					float heightB = elementHeight * (EditorGUIUtility.singleLineHeight * 2.7F);

					return heightA + heightB + paddingBottom;
				};
		}
		#endregion PIPELINE
	}
}
#endif
