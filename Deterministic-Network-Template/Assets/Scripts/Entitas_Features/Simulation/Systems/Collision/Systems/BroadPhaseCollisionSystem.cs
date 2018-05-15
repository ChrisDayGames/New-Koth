using System.Collections.Generic;
using Entitas;
using Determinism;
using CommandInput;

public class BroadPhaseCollisionSystem : ReactiveSystem <LogicEntity> {

	readonly IGroup <LogicEntity> _allColliders;

	public BroadPhaseCollisionSystem (Contexts contexts) : base (contexts.logic) {

		_allColliders = contexts.logic.GetGroup (LogicMatcher.Collider);

	}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Position);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasPosition && entity.hasCollider;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			foreach (LogicEntity other in _allColliders) {

				if (e == other) {
					continue;

				}

				if (Collider.CheckOverlapBroad (e.collider.value, other.collider.value)) {

					if (e.hasOnTriggerEnter)
						e.onTriggerEnter.function.OnCollisionEnter (other.id.value);

				}
				
			}
			
		}

	}

}