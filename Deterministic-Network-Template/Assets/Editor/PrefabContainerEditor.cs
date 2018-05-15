using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( PrefabContainer ) )]
public class PrefabContainerEditor : Editor {

	SerializedProperty prefabs;

	private void OnEnable ()
	{
		prefabs = serializedObject.FindProperty( "prefabs" );
	}

//	public override void OnInspectorGUI ()
//	{
//		serializedObject.Update();
//
//		var iterator = prefabs.Copy();
//
//		iterator.Next( true );
//		iterator.Next( true );
//
//		EditorGUILayout.PropertyField( iterator, GUIContent.none );
//		EditorGUILayout.Space();
//
//		while ( iterator.Next( false ) )
//		{
//			EditorGUILayout.PropertyField( iterator, GUIContent.none );
//		}
//
//		serializedObject.ApplyModifiedProperties();
//	}

}
