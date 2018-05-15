using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CommandInput;
using Determinism;

/*
 * Last Position Removes
 * 
 * 
 * Input Validation and Solidifications
 * Squash and Stretch
 * 2 Controllers input
 * Smooth motion
 * Rollback (Forward and Backward Motion)
 * 
 * 
 * 
 */

public class SyncInputBehaviour : NetworkBehaviour {

	private InputContext _inputContext;
	private TimeContext _timeContext;

	public class SyncInputHistory : SyncListStruct <StoredInput> {}
	private SyncInputHistory syncInputHistory = new SyncInputHistory ();

	[SyncVar]
	private int lockstepTick;

	[SyncVar]
	private int inputHistoryPosition;

	public void Start () {

		_inputContext = Contexts.sharedInstance.input;
		_timeContext = Contexts.sharedInstance.time;
	}

	public override void OnStartLocalPlayer () {

		lockstepTick = 0;
		inputHistoryPosition = 0;
	
	}

	void LateUpdate () {

		if (isServer)
			CmdSendClientInput (_inputContext.inputHistory.snapshots.ToArray());

		if (isLocalPlayer)
			SyncInput ();


	}

	[Command]
	void CmdSendClientInput (StoredInput[] clientInputHistory) {

		if (!isServer) return;
		if (inputHistoryPosition >= clientInputHistory.Length) return;

		for (int i = inputHistoryPosition; i < clientInputHistory.Length; i++) {

			InputSnapshot snapshot = new InputSnapshot ();
			snapshot.Copy (clientInputHistory[i].snapshot);

			syncInputHistory.Add(new StoredInput (
				clientInputHistory[i].tick,
				clientInputHistory[i].playerID, 
				snapshot));	

			if (lockstepTick < clientInputHistory[i].tick)
				lockstepTick = clientInputHistory[i].tick;

		}

		inputHistoryPosition = syncInputHistory.Count;

	}

	void SyncInput () {

		List<StoredInput> testList = new List<StoredInput> ();

		for (int i = 0; i < syncInputHistory.Count; i++) {

			InputSnapshot snapshot = new InputSnapshot ();
			snapshot.Copy (syncInputHistory[i].snapshot);

			testList.Add(new StoredInput (
				syncInputHistory[i].tick,
				syncInputHistory[i].playerID, 
				snapshot));

		}

		_inputContext.ReplaceInputHistory (testList);

		if (_timeContext.tick.value < lockstepTick)
			_timeContext.ReplaceJumpInTime (lockstepTick);

	}

}
