using System.Collections.Generic;
using Entitas;
using Determinism;

public class AccelerationSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _accelerators;
	public AccelerationSystem (Contexts contexts) {

		_accelerators = contexts.logic.GetGroup (LogicMatcher.Acceleration);

	}

	public void Execute () {

		foreach (LogicEntity e in _accelerators.GetEntities ()) {

			e.ReplaceLastVelocity (e.velocity.value);
			e.ReplaceVelocity (e.velocity.value + (e.acceleration.value / GameController.FPS));
			e.ReplaceAcceleration (FixedVector2.ZERO);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _accelerators.GetEntities ()) {

		}

	}

}