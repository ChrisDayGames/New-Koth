using Determinism;
using CommandInput;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Logic]
public class ColliderComponent : IComponent {

	public Collider value;

}

[Logic]
public class RayCastCollisionComponent : IComponent {

	public RaycastCollider value;

}

[Logic, Event (false)]
public class CollisionInfoComponent : IComponent {

	public RaycastCollisionInfo value;

}

[Logic]
public class OnRayCastCollisionComponent : IComponent {

	public RayCastCollisionCommand function;

}

[Logic]
public class OnTriggerEnterComponent : IComponent {

	public OnTriggerEnterCommand function;

}

