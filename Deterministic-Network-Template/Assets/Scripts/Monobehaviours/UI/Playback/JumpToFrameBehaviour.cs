using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpToFrameBehaviour : MonoBehaviour, IPausedListener {
	
	private Button button;

	//private reference to the time context
	protected TimeContext _timeContext;

	// Use this for initialization
	void Start () {

		//get the time context
		_timeContext = Contexts.sharedInstance.time;

		//creating an entity to represent this
		Contexts.sharedInstance.time.CreateEntity().AddPausedListener(this);

		button = GetComponent<Button> ();
		button.enabled = false;

	}

	// Update is called once per frame
	public void OnPaused (TimeEntity e) {
		button.enabled = e.isPaused;
	}

	public void GoToTick (int delta) {

		if (button.enabled)
			_timeContext.ReplaceJumpInTime (_timeContext.jumpInTime.targetTick + delta);

	}

}

