using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRootBehaviour : MonoBehaviour, IPausedListener {

	// Use this for initialization
	void Start () {
		Contexts.sharedInstance.time.CreateEntity().AddPausedListener(this);
	}
	
	// Update is called once per frame
	public void OnPaused (TimeEntity e) {
		this.gameObject.SetActive (e.isPaused);
	}

}
