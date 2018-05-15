using System.Collections.Generic;
using Entitas;
using Determinism;

public class MoveSystem : ReactiveSystem <LogicEntity> {

	readonly IGroup <LogicEntity> _allColliders;

	public MoveSystem (Contexts contexts) : base (contexts.logic) {
		
		_allColliders = contexts.logic.GetGroup (LogicMatcher.Collider);

	}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Move);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasMove && !entity.hasRayCastCollision;

	}

	protected override void Execute (List<LogicEntity> entities) {

		foreach (LogicEntity e in entities) {

			FixedVector2 newPosition = e.position.value + e.move.target;

			e.ReplaceLastPosition (e.position.value);
			e.ReplacePosition (newPosition);

			e.RemoveMove ();
			
		}

	}

}