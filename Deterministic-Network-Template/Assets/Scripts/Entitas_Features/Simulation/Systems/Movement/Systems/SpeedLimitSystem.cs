using System.Collections.Generic;
using Entitas;
using Determinism;

public class SpeedLimitSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _speedLimiters;
	public SpeedLimitSystem (Contexts contexts) {

		_speedLimiters = contexts.logic.GetGroup (LogicMatcher.AllOf (LogicMatcher.Velocity, LogicMatcher.ThrowMovement));

	}

	public void Execute () {

		foreach (LogicEntity e in _speedLimiters.GetEntities ()) {

			if (e.velocity.value.x.Abs() > GameConstants.MAX_HAT_SPEED) {

				e.ReplaceVelocity (e.velocity.value.SetX (
					GameConstants.MAX_HAT_SPEED * e.velocity.value.x.Sign ()
				));

			}


			if (e.velocity.value.y.Abs() >  GameConstants.MAX_HAT_SPEED) {

				e.ReplaceVelocity (e.velocity.value.SetY (
					GameConstants.MAX_HAT_SPEED * e.velocity.value.y.Sign ()
				));

			}

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _speedLimiters.GetEntities ()) {

			if (e.isDangerous 
				&& e.velocity.value.x.Abs() < GameConstants.THROW_SPEED_CUTOFF_X 
				&& e.velocity.value.y.Abs() < GameConstants.THROW_SPEED_CUTOFF_Y)
				e.isDangerous = false;
			
		}

	}

}
