using System.Collections.Generic;
using Entitas;
using Determinism;

public class CalculateJumpHeightSystem : ReactiveSystem <LogicEntity> {

	public CalculateJumpHeightSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.TimeToApex);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasTimeToApex && entity.hasMaxJump && !entity.hasGravity;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			e.ReplaceGravity ((2 * e.maxJump.value).Div(e.timeToApex.value.Squared ()));
			e.isFalling = true;

			e.ReplaceMaxJumpVelocity (e.gravity.value.Mul(e.timeToApex.value)); 

			if (e.hasMinJump)
				e.ReplaceMinJumpVelocity (FixedMath.Sqrt(2 * (e.gravity.value.Abs ()).Mul(e.minJump.value)));

			if (e.hasBounceHeight)
				e.ReplaceBounceVelocity (FixedMath.Sqrt(2 * (e.gravity.value.Abs ()).Mul(e.bounceHeight.value)));

		}

	}

}