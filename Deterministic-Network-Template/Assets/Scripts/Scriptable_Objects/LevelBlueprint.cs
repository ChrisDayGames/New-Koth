using UnityEngine;
using Entitas;
using TypeReferences;
using Determinism;
using System.Collections.Generic;
using LevelEditor;

[CreateAssetMenu]
public class LevelBlueprint : ScriptableObject {

	[Header ( "View Data" )]
	public Texture2D texture;
	public Material mat;
	public GameObject skybox;

}