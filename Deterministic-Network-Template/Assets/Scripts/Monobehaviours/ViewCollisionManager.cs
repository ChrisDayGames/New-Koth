using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Determinism;

public class ViewCollisionManager : MonoBehaviour, ICollisionInfoListener{

	private Dictionary <int, bool> collisions = new Dictionary<int, bool> ();

	// Use this for initialization
	void Start () {
		Contexts.sharedInstance.logic.CreateEntity ().AddCollisionInfoListener (this);
	}
	
	public void OnCollisionInfo (LogicEntity e, RaycastCollisionInfo info) {


		if (collisions.ContainsKey (e.id.value)) {

			print ("yeah im here");


		} else {

			collisions.Add (e.id.value, true);

		}

	}

}
