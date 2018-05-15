using System.Collections.Generic;
using Entitas;
using Determinism;

public class EndStunSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _stunTimers;
	public EndStunSystem (Contexts contexts) {

		_stunTimers = contexts.logic.GetGroup (LogicMatcher.StunTimer);

	}

	public void Execute () {

		foreach (LogicEntity e in _stunTimers.GetEntities ()) {

			if (e.stunTimer.timeLeft <= FixedMath.Create (3, 10)) {

				long acceleration = FixedMath.Lerp (
					e.stunMovement.accelerationTime, 
					e.groundMovement.accelerationTime, 
					e.stunTimer.timeLeft.Mul(FixedMath.Create (3, 10)));

				e.ReplaceCurrentMovementX (0, acceleration, e.currentMovementX.refSpeed);

			}

			e.ReplaceStunTimer (e.stunTimer.timeLeft - GameController.DELTA_TIME);

		}


	}

	public void Cleanup () {

		foreach (LogicEntity e in _stunTimers.GetEntities ()) {

			if (!e.isStunned) {

				e.RemoveStunTimer ();

			} else if (e.stunTimer.timeLeft <= -FixedMath.Tenth * 3) {
				
				e.ReplaceCurrentMovementX (0, 0, e.currentMovementX.refSpeed);
				e.isStunned = false;
				e.RemoveStunTimer ();

			}

		}

	}

}