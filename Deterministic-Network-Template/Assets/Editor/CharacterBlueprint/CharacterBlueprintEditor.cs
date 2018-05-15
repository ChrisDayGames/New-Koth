using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor ( typeof (CharacterBlueprint) ) ]
public class CharacterBlueprintEditor : Editor {

	public override void OnInspectorGUI () {

		serializedObject.Update ();

		EditorGUILayout.Space ();

		DrawPropertiesExcluding (serializedObject, "m_Script");

		EditorGUILayout.Space();

		serializedObject.ApplyModifiedProperties ();

	}

}
