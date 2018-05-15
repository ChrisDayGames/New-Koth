using System.Collections.Generic;
using Entitas;

public class EndWallJumpSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _wallJumpTimers;
	public EndWallJumpSystem (Contexts contexts) {

		_wallJumpTimers = contexts.logic.GetGroup (LogicMatcher.WallJumpTimer);

	}

	public void Execute () {

		foreach (LogicEntity e in _wallJumpTimers.GetEntities ()) {

			e.ReplaceWallJumpTimer (e.wallJumpTimer.timeLeft - GameController.DELTA_TIME);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _wallJumpTimers.GetEntities ()) {

			if (!e.isWallJumping) {

				e.RemoveWallJumpTimer ();

			} else if (e.wallJumpTimer.timeLeft <= 0) {

				e.RemoveWallJumpTimer ();
				e.isWallJumping = false;

			}

		}

	}

}