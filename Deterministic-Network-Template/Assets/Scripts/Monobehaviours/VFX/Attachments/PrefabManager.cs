using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {

	public PrefabContainer container;

	private Dictionary <string, GameObject> prefabLookUp;

	public void Start () {

		prefabLookUp = new Dictionary<string, GameObject> ();

		if (container.prefabs.Length == 0) 
			return;

		foreach (FXData data in container.prefabs) {

			GameObject go = Instantiate <GameObject> (data.prefab);
			go.transform.parent = this.transform;
			go.transform.localPosition = data.positionOffset;
			go.transform.localScale = Vector3.one;
			go.SetActive (data.startActive);

			prefabLookUp.Add (data.groupID, go);

		}

	}

	public GameObject GetGameObject (string key) {
	
		if (prefabLookUp.ContainsKey (key)) {

			return prefabLookUp [key];

		}

		return null;

	}

	public void ManageGameObject (string key, bool isActive) {

		if (prefabLookUp.ContainsKey (key))
			prefabLookUp [key].SetActive (isActive);

	}

	public void InitializeGameObject (string key, LogicEntity e, Transform owner) {

		if (prefabLookUp.ContainsKey (key)) {

			FXBehaviour fxBehaviour = prefabLookUp [key].GetComponent <FXBehaviour> ();
			if (fxBehaviour != null)
				fxBehaviour.Initialize (e, owner);


		}

	}

}
