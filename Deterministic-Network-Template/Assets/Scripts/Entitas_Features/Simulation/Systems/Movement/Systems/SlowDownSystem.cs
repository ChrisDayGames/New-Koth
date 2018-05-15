using System.Collections.Generic;
using Entitas;
using Determinism;

public class SlowDownSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _slowDowns;
	public SlowDownSystem (Contexts contexts) {

		_slowDowns = contexts.logic.GetGroup (LogicMatcher.AllOf (LogicMatcher.Velocity).AnyOf(
			LogicMatcher.CurrentMovementX, 
			LogicMatcher.CurrentMovementY,
			LogicMatcher.Drag,
			LogicMatcher.Friction));

	}

	long xa = 0, ya = 0;
	public void Execute () {

		foreach (LogicEntity e in _slowDowns.GetEntities ()) {

			xa = ya = 0;

			if (e.hasCurrentMovementX && e.velocity.value.x != e.currentMovementX.targetSpeed) {

				xa = e.velocity.value.x.SmoothDamp (
					e.currentMovementX.targetSpeed, 
					ref e.currentMovementX.refSpeed,
					e.currentMovementX.accelerationTime);

			}

			if (e.hasCurrentMovementY && e.velocity.value.y != e.currentMovementY.targetSpeed) {

				ya = e.velocity.value.y.SmoothDamp (
					e.currentMovementY.targetSpeed, 
					ref e.currentMovementY.refSpeed,
					e.currentMovementY.accelerationTime);

			}

			if (e.hasFriction && e.isGrounded) {

				long friction = (e.isDangerous) ? e.friction.dangerousFriction : e.friction.friction;

				if (e.velocity.value.x.Abs () > friction){

					e.velocity.value.x -= (friction * e.velocity.value.x.Sign ());
					e.ReplaceVelocity (e.velocity.value);

				} else {

					e.velocity.value.x = 0;

				}

			}

			if (e.hasDrag && !e.isGrounded) {

				long drag = (e.isDangerous) ? e.drag.dangerousDrag : e.drag.drag;

				if (e.velocity.value.x.Abs () > drag) {

					e.velocity.value.x -= (drag * e.velocity.value.x.Sign ());
					e.ReplaceVelocity (e.velocity.value);

				} else {

					e.velocity.value.x = 0;

				}

			}

			e.ReplaceAcceleration (new FixedVector2 (xa, ya) + e.acceleration.value);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _slowDowns.GetEntities ()) {

		}

	}

}