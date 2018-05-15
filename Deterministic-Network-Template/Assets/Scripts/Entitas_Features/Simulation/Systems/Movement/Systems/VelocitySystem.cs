using System.Collections.Generic;
using Entitas;
using Determinism;

public class VelocitySystem : ReactiveSystem <LogicEntity> {

	public VelocitySystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Velocity);

	}

	protected override bool Filter (LogicEntity entity){
		
		return entity.hasVelocity;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			if (e.velocity.value.x != 0 && !e.isWallRiding)
				e.ReplaceDirection (e.velocity.value.x.Sign ());
			else if (e.isWallRiding)
				e.ReplaceDirection ((e.collisionInfo.value.left == Tag.DEFAULT) ? -1 : 1);

			if (!e.hasFreeze)
				e.ReplaceMove (e.velocity.value / GameController.FPS);

		}

	}

}