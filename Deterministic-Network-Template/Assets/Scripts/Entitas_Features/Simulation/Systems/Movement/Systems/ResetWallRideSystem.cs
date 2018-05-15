using System.Collections.Generic;
using Entitas;

public class ResetWallRideSystem : ReactiveSystem <LogicEntity> {

	public ResetWallRideSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.WallRiding);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.isWallRiding;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			e.isWallRiding = false;
			
		}

	}

}