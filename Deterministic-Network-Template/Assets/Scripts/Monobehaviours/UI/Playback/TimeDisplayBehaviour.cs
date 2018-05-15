using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayBehaviour : MonoBehaviour, ITickListener {

	private Text display;

	// Use this for initialization
	void Start () {

		display = GetComponent<Text> ();

		Contexts.sharedInstance
			.time.CreateEntity ()
			.AddTickListener(this);
		
	}
	
	// Update is called once per frame
	public void OnTick (TimeEntity e, int currentTick) {
		display.text = currentTick.ToString ();
	}

}
