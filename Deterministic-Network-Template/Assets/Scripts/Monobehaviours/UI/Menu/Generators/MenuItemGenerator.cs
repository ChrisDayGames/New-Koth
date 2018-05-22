using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public interface IGeneratable {

	void Generate (int i);
	int GetMaxObjects ();
	MonoBehaviour GetScript ();
	Transform GetTransform ();

}
	
public class MenuItemGenerator : MonoBehaviour {

	public MonoBehaviour mono;

	private IGeneratable prototype;
	private int objectsToSpawn {get {return prototype.GetMaxObjects ();}}

	// Use this for initialization
	void Awake () {
		prototype = (IGeneratable) mono;
		GenerateObjects ();
	}

	[Button]
	protected virtual void GenerateObjects () {
		
		if (mono == null) return;
		prototype = mono as IGeneratable;

		DestroyObjects ();
		for (int i = 0; i < objectsToSpawn; i++) {

			IGeneratable obj = (IGeneratable) Instantiate <MonoBehaviour> (prototype.GetScript ());
			obj.Generate (i);
			obj.GetTransform ().SetParent (this.transform, false);
			obj.GetTransform ().localScale = Vector3.one;

		}

	}

	[Button]
	protected virtual void DestroyObjects () {

		transform.DestroyChildrenImmediate ();

	}

	public void LookForGeneratables () {

		foreach (MonoBehaviour m  in mono.gameObject.GetComponents <MonoBehaviour> ()) {

			if (m is IGeneratable) {

				mono = m;
				return;

			}

		}

		Debug.LogError (mono.name + "does not implement the interface IGeneratable.");
		mono = null;

	}

	void OnValidate() {

		if (Application.isPlaying) return;
		if (mono != null) return;
		LookForGeneratables ();

	}

	void Reset () {

		LookForGeneratables ();

	}

}
