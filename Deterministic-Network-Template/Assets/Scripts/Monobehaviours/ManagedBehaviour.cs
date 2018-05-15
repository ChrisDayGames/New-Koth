using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedBehaviour : MonoBehaviour, IExecuteBehaviour, IFixedExecuteBehaviour {

	public void OnEnable () {
		UpdateManager.behaviours.Add (this);
		Initialize ();
	}

	public void OnDisable () {
		UpdateManager.behaviours.Remove (this);
	}

	public virtual void Initialize () {
		
	}

	public virtual void Execute (float alpha) {
		
	}
		
	public virtual void FixedExecute () {

	}

}
