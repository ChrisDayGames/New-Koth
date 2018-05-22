using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Renderer))]
public class FogObject : MonoBehaviour {

	public Renderer rend;

	// Use this for initialization
	void Start () {

		rend = GetComponent <Renderer> ();

	}


	void OnValidate () {

		if (rend == null)
			rend = GetComponent <Renderer> ();

	}

	public void SetFogImage (Texture2D tex) {

		rend.material.SetTexture ("_FogTex", tex);

	}

	public void SetFogColor (Color c) {

		rend.material.SetColor ("_OverrideColor", c);

	}

	public void SetMaterial (Material mat) {

		rend.sharedMaterial = mat;

	}

}
