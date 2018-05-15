using System.Collections.Generic;
using Entitas;
using Determinism;

public class TerminalVelocitySystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _fallers;
	public TerminalVelocitySystem (Contexts contexts) {

		_fallers = contexts.logic.GetGroup (LogicMatcher.AllOf (LogicMatcher.Velocity, LogicMatcher.TerminalVelocity));

	}

	public void Execute () {

		foreach (LogicEntity e in _fallers.GetEntities ()) {

			long terminalVelocity = e.terminalVelocity.value;

			if (e.hasFastFallTerminalVelocity && e.isFastFalling)
				terminalVelocity = e.fastFallTerminalVelocity.value;

			//wallriding entities will slide at their wallride fallspeed
			if (e.isWallRiding && e.hasWallRideMovement) {

				terminalVelocity = e.wallRideMovement.fallSpeed;

				//wallriding entities can also fastfall
				if (e.isFastFalling)
					terminalVelocity *= e.wallRideMovement.fastFallFactor;

			}

			//stop entities from falling faster than their terminal velocity
			if (e.velocity.value.y < -terminalVelocity)
				e.ReplaceVelocity (e.velocity.value.SetY(-terminalVelocity));

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _fallers.GetEntities ()) {

		}

	}

}