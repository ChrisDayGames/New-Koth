using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ParticleSystem))]
public class ClearParticleSystem : MonoBehaviour {

	public bool clearOnEnable = false;
	public bool clearOnDisable = false;
	public bool clearChildren = true;

	ParticleSystem ps;

	void Awake () {
		ps =  GetComponent<ParticleSystem> ();
	}

	void OnEnable () {

		ps.Clear ();

	}

//	void Update () {
//
//		if (fuckYou) {
//			
//			ps.Clear ();	
//
//		}
//
//	}

	void OnDisable () {


		ps.Clear ();
		

	}


}
