using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehaviour :  MonoBehaviour, IPausedListener {

	TimeContext _timeContext;

	// Use this for initialization
	void Start () {

		_timeContext = Contexts.sharedInstance.time;

		_timeContext.CreateEntity()
			.AddPausedListener (this);

		//_context.isPaused = true;
		
	}
	
	// Update is called once per frame
	public void OnPaused (TimeEntity e) {
		
	}

	public void HandlePause () {
		_timeContext.isPaused = !_timeContext.isPaused;
	}


}
