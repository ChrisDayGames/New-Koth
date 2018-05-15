using System.Collections.Generic;
using Entitas;
using Determinism;

public class AddRayCastCollisionSystem : ReactiveSystem <LogicEntity> {

	public AddRayCastCollisionSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Collider);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasCollider && entity.isMovable && !entity.hasRayCastCollision;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			if (e.collider.value.GetType () == typeof (BoxCollider))
				e.AddRayCastCollision (new RaycastCollider ((BoxCollider) e.collider.value));
			else 
				e.isMovable = false;

		}

	}
	 
}