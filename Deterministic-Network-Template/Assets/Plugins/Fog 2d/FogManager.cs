using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DistanceFunction {

	SQUARED,
	CUBE,
	SQRT,
	LOG,
	EXP,

}

public class FogManager : MonoBehaviour {

	public FogTemplate fog;

	[Header ("Editor Controls")]
	public bool renderFog = false;

	[Header ("Affected Objects")]
	public FogObject[] fogObjects;

	void Start () {

		SetMaterials ();
		RenderFog ();

	}

	void Update () {

		if (fogObjects == null) return;
		if (!renderFog) return;

		RenderFog ();

	}

	void RenderFog () {

		foreach (FogObject fObj in fogObjects) {

			fog.tint.a = Mathf.Clamp01 (fObj.transform.position.z / fog.maxDistance);
			fObj.SetFogImage (fog.image);
			fObj.SetFogColor (fog.tint);

		}


	}

	void SetMaterials () {

		foreach (FogObject fObj in fogObjects) {

			//fObj.SetMaterial (fog.mat);

		}


	}

	void OnValidate () {

		fogObjects = GetComponentsInChildren <FogObject> ();

	}

}
