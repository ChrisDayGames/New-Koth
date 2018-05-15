using Entitas;
using Determinism;

public class GravitySystem : IExecuteSystem {

	public readonly long STUNNED_GRAVITY_MODIFIER = FixedMath.Create (85, 100);

	readonly IGroup <LogicEntity> _fallers;

	public GravitySystem (Contexts contexts) {
		_fallers = contexts.logic.GetGroup (LogicMatcher.Falling);
	}

	long gravity = 0;
	public void Execute () {

		foreach (LogicEntity e in _fallers.GetEntities ()) {

			//regular falling gravity for generic entities
			gravity = 1;

			//entities with a specified gravity fall at their own pace
			if (e.hasGravity)
				gravity = e.gravity.value;

			//fast falling entities fall with a multiplier
			if (e.isFastFalling && e.hasFastFallFactor) {
				gravity *= e.fastFallFactor.value;	
			}

			if (e.hasThrowMovement) {

				gravity += 
					gravity.Mul((e.position.value.y - e.throwMovement.throwPositionY).Abs ()) / 2;
				
			}

			if (e.isStunned)
				gravity = gravity.Mul (STUNNED_GRAVITY_MODIFIER);

			//update the acceleration value
			e.ReplaceAcceleration (e.acceleration.value + (FixedVector2.DOWN * gravity));

		}

	}

}
