using System.Collections.Generic;
using Entitas;

public class SetDirtySystem : ReactiveSystem <LogicEntity>, ICleanupSystem {

	readonly IGroup <LogicEntity> dirtyEntities;
	public SetDirtySystem (Contexts contexts) : base (contexts.logic) {

		dirtyEntities = contexts.logic.GetGroup (LogicMatcher.Dirty);

	}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.AllOf (LogicMatcher.Position).AnyOf(
			LogicMatcher.Grounded,
			LogicMatcher.Dashing,
			LogicMatcher.FastFalling,
			LogicMatcher.WallRiding,
			LogicMatcher.WallJumping,
			LogicMatcher.Velocity,
			LogicMatcher.Dead,
			LogicMatcher.DeathTimer,
			LogicMatcher.Attached
		).NoneOf (LogicMatcher.Dirty));

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasPosition;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {
			e.isDirty = true;
		}

	}

	public void Cleanup () {

		foreach (LogicEntity e  in dirtyEntities.GetEntities ()) {
		}

	}

}