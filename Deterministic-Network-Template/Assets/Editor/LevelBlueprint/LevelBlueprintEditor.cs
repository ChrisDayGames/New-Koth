using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof (LevelBlueprint) ) ]
public class LevelBlueprintEditor : Editor {


	public override void OnInspectorGUI () {

		serializedObject.Update ();

		EditorGUILayout.Space ();

		DrawPropertiesExcluding (serializedObject, "m_Script");

		EditorGUILayout.Space();

		serializedObject.ApplyModifiedProperties ();
			
	}

}
