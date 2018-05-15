using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Determinism;

[CanEditMultipleObjects]
[CustomPropertyDrawer ( typeof (FixedVector2) ) ]
public class FixedVector2Drawer : PropertyDrawer {

	private static GUIContent xContent = new GUIContent( "X" );
	private static GUIContent yContent = new GUIContent( "Y" );

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		
		return EditorGUIUtility.singleLineHeight;

	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

		if (property.serializedObject.isEditingMultipleObjects)
			return;
		
		var labelRect = position.PadRight (position.width - EditorGUIUtility.labelWidth);
		var controlsRect = position.PadLeft (EditorGUIUtility.labelWidth);

		var xRect = controlsRect.PadRight (controlsRect.width * 0.5f);
		var yRect = controlsRect.PadLeft (controlsRect.width * 0.5f);

		GUI.Label (labelRect, label);

		var labelWidth = EditorGUIUtility.labelWidth;

		EditorGUIUtility.labelWidth = 20f;

		EditorGUI.PropertyField (xRect, property.FindPropertyRelative ("rawX"), xContent );
		EditorGUI.PropertyField (yRect, property.FindPropertyRelative ("rawY"), yContent );

		// just to be safe...
		EditorGUIUtility.labelWidth = labelWidth;

//		EditorGUI.MultiPropertyField (
//			position, 
//			new GUIContent [] {
//			new GUIContent ("x"),
//				new GUIContent ("y") },
//			property.FindPropertyRelative("rawX"),
//			label
//		);

	}

}
