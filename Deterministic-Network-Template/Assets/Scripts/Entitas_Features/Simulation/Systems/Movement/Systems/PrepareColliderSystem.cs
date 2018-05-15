using Entitas;

public class PrepareColliderSystem : IExecuteSystem {

	readonly IGroup <LogicEntity> _colliders;
	public PrepareColliderSystem (Contexts contexts) {
		_colliders = contexts.logic.GetGroup (LogicMatcher.AllOf (LogicMatcher.Position, LogicMatcher.Collider));
	}
	
	public void Execute () {

		foreach (LogicEntity e in _colliders.GetEntities ()) {

			if (e.position.value != e.collider.value.position)
				e.collider.value.MoveCollider (e.position.value);
			
		}
		
	}

}
