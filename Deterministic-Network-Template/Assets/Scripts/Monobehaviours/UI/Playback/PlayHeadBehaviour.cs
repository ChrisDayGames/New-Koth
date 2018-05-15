using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayHeadBehaviour : MonoBehaviour, ITickListener, IPausedListener {

	TimeContext _timeContext;

	private Slider playhead;
	private int totalTicks;

	// Use this for initialization
	void Start () {

		playhead = GetComponent<Slider> ();

		_timeContext = Contexts.sharedInstance.time;

		TimeEntity e = _timeContext.CreateEntity ();

		e.AddTickListener (this);
		e.AddPausedListener (this);

		playhead.enabled = false;

	}

	// Update is called once per frame
	public void OnTick (TimeEntity e, int currentTick) {

		if (currentTick > totalTicks) {

			totalTicks = currentTick;
			playhead.maxValue = totalTicks;
			playhead.value = playhead.maxValue;

		}

	}
	
	// Update is called once per frame
	public void OnPaused (TimeEntity e) {
		playhead.enabled = e.isPaused;
	}

	public void GoToTick () {

		if (playhead.enabled)
			_timeContext.ReplaceJumpInTime ((int) playhead.value);

	}

}
