using System.Collections.Generic;
using Entitas;

public class EndThrowSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _wallJumpTimers;
	public EndThrowSystem (Contexts contexts) {

		_wallJumpTimers = contexts.logic.GetGroup (LogicMatcher.ThrowTimer);

	}
		
	public void Execute () {
		
		foreach (LogicEntity e in _wallJumpTimers.GetEntities ()) {

			e.ReplaceThrowTimer (e.throwTimer.timeLeft - GameController.DELTA_TIME);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _wallJumpTimers.GetEntities ()) {

			if (e.throwTimer.timeLeft <= 0)
				e.RemoveThrowTimer ();

		}

	}

}