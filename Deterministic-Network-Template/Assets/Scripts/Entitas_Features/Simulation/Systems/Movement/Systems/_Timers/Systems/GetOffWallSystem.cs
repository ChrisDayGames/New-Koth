using System.Collections.Generic;
using Entitas;

public class GetOffWallSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _wallStickTimers;
	public GetOffWallSystem (Contexts contexts) {

		_wallStickTimers = contexts.logic.GetGroup (LogicMatcher.WallStickTimer);

	}

	public void Execute () {

		foreach (LogicEntity e in _wallStickTimers.GetEntities ()) {

			e.ReplaceWallStickTimer (e.wallStickTimer.timeLeft - GameController.DELTA_TIME);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _wallStickTimers.GetEntities ()) {

			if (e.wallStickTimer.timeLeft <= 0) {

				e.RemoveWallStickTimer ();
				e.isWallRiding = false;

			}

		}

	}

}