using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExecuteBehaviour {
	void Execute (float alpha);
}

public interface IFixedExecuteBehaviour {
	void FixedExecute ();
}

public class UpdateManager : MonoBehaviour {

	public static List<MonoBehaviour> behaviours = new List<MonoBehaviour> ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//calculate alpha
		float alpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
		alpha = Mathf.Clamp01 (alpha);

		//run updates
		foreach (IExecuteBehaviour behaviour in behaviours) {
			behaviour.Execute (alpha);
		}

	}

	void FixedUpdate () {

		foreach (IFixedExecuteBehaviour behaviour in behaviours) {
			behaviour.FixedExecute ();
		}


	}

}
