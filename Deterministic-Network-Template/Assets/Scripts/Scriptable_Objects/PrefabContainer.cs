using UnityEngine;

[System.Serializable]
public class FXData {

	public string groupID;
	public Vector3 positionOffset;
	public GameObject prefab;
	public bool startActive =  false;

}

[CreateAssetMenu()]
public class PrefabContainer : ScriptableObject {

	public FXData[] prefabs;

}
