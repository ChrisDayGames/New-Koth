using System.Collections.Generic;
using Entitas;
using Determinism;

public class DeathCollisionSystem : ReactiveSystem <LogicEntity> {

	public DeathCollisionSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Dead.AddedOrRemoved ());

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasCollider;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			if (e.isDead) {

				e.collider.value.mask = Mask.DEAD;

				e.collider.value.check = 
					e.collider.value.check.RemoveFlags (Mask.P1, Mask.P2, Mask.P3, Mask.P4, Mask.P5, Mask.P6, Mask.P7, Mask.P8);


			} else if (e.hasCollider) {
				
				if (e.hasPlayerID)
					e.collider.value.mask = MaskUtil.GetMaskForPlayerID (e.playerID.id);
				else if (e.hasFollowPoint)
					e.collider.value.mask = MaskUtil.GetMaskForPlayerID (
						Contexts.sharedInstance.logic.GetEntityWithId(e.followPoint.targetID).playerID.id
					);
				else
					e.collider.value.mask = Mask.DEFAULT;

				e.collider.value.check = 
					e.collider.value.check.AddFlags (Mask.DEFAULT, Mask.P1, Mask.P2, Mask.P3, Mask.P4, Mask.P5, Mask.P6, Mask.P7, Mask.P8);

				e.collider.value.check = 
					e.collider.value.check.RemoveFlag (e.collider.value.mask);


			}
			
		}

	}

}