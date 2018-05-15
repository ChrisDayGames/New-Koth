using System.Collections.Generic;
using Entitas;
using UnityEngine;
using CommandInput;

public class InputHistorySystem : IInitializeSystem, ICleanupSystem {

	readonly TimeContext _timeContext;
	readonly InputContext _inputContext;
	readonly IGroup <InputEntity> _validInputs;

	public InputHistorySystem (Contexts contexts) {

		_timeContext = contexts.time;
		_inputContext = contexts.input;
		_validInputs = _inputContext.GetGroup (InputMatcher.ValidInput);

	}

	public void Initialize () {

		_inputContext.ReplaceInputHistory (new List<StoredInput> ());

	}

	public void Cleanup () {

		foreach (InputEntity e in _validInputs) {

//			_inputContext.inputHistory.snapshots
//				.Add (new StoredInput (
//					_timeContext.tick.value, 
//					e.controllerID.id, 
//					e.controllerInput.snapshot));

			e.controllerInput.snapshot.ResetButtons ();

		}
			
	}

}