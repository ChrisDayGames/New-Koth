using System.Collections.Generic;
using Entitas;
using UnityEngine;
using CommandInput;
using Entitas.Unity;

public class ReplaySystem : ReactiveSystem <TimeEntity> {

	readonly LogicContext _LogicContext;
	readonly TimeContext _timeContext;
	readonly InputContext _inputContext;
	readonly IGroup <InputEntity> _inputSources;

	public ReplaySystem (Contexts contexts) : base (contexts.time) {

		_LogicContext = contexts.logic;
		_timeContext = contexts.time;
		_inputContext = contexts.input;
		_inputSources = _inputContext.GetGroup (InputMatcher.ControllerInput);

	}

	protected override ICollector<TimeEntity> GetTrigger (IContext<TimeEntity> context) {

		return context.CreateCollector (TimeMatcher.JumpInTime);

	}

	protected override bool Filter (TimeEntity entity){

		return entity.hasJumpInTime;

	}

	protected override void Execute (List<TimeEntity> entities) {

		foreach (LogicEntity e in _LogicContext.GetEntities ()) {
			e.isDestroyed = true;
		}

		Contexts.sharedInstance.input.commandList.commands.Clear ();
		_timeContext.logicSystem.system.Initialize ();


		int currentInputFrame = 0;
		for (int i = 0; i <= _timeContext.jumpInTime.targetTick; i++) {

			_timeContext.ReplaceTick (i);

			foreach (InputEntity e in _inputSources) {

				e.controllerInput.snapshot.Reset ();
				e.ReplaceControllerInput(e.controllerInput.snapshot);
				e.isValidInput = false;

			}

			if (currentInputFrame < Contexts.sharedInstance.input.inputHistory.snapshots.Count) {

				for (int j = currentInputFrame; j < Contexts.sharedInstance.input.inputHistory.snapshots.Count; j++) {

					StoredInput nextInput = _inputContext.inputHistory.snapshots[j];

					if (nextInput.tick == i) {

						foreach (InputEntity e in _inputContext.GetEntitiesWithControllerID (nextInput.playerID)) {

							InputSnapshot s = new InputSnapshot ();
							s.Copy (nextInput.snapshot);

							e.ReplaceControllerInput (s);
							e.isValidInput = true;

						}

					} 

					else if (nextInput.tick > i){
						
						currentInputFrame = j;
						break;

					}

				}

				_timeContext.logicSystem.system.Execute ();
				_timeContext.logicSystem.system.Cleanup ();
					
			}

		}

	}

}