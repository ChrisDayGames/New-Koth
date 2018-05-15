using System.Collections.Generic;
using Entitas;

public class InvincibleOnDangerousSystem : ReactiveSystem <LogicEntity> {

	public InvincibleOnDangerousSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Dangerous.AddedOrRemoved ());

	}

	protected override bool Filter (LogicEntity entity){

		return true;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			e.isInvincible = e.isDangerous;
		
		}

	}

}