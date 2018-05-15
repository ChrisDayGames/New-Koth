using System.Collections.Generic;
using Entitas;
using System.Linq;

public class OnRaycastCollisionSystem : ReactiveSystem <LogicEntity> {

	public OnRaycastCollisionSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.CollisionInfo);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasCollisionInfo && entity.hasOnRayCastCollision;

	}

	protected override void Execute (List<LogicEntity> entities) {

		entities = entities.OrderBy (o => (int) o.onRayCastCollision.function.priority).ToList ();
		
		foreach (LogicEntity e in entities) {

			e.onRayCastCollision.function.OnCollisionEnter (e.collisionInfo.value);

		}

	}

}