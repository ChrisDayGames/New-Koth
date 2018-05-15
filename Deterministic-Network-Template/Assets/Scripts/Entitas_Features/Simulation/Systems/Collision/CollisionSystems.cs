using Entitas;

public class CollisionSystems : Feature {

	public CollisionSystems (Contexts contexts) : base ("CollisionSystems") {

		Add (new RayCastSystem (contexts));
		Add (new AddRayCastCollisionSystem (contexts));
		Add (new BroadPhaseCollisionSystem (contexts));
		Add (new OnRaycastCollisionSystem (contexts));

	}

}