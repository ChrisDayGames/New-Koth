using Determinism;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Logic]
public sealed class RespawnComponent : IComponent {}


[Logic, Unique]
public class SpawnPointsComponent : IComponent {

	public FixedVector2[] list;

}

[Logic, Unique, Event(false)]
public class LevelComponent : IComponent {

	public LevelData data;

}
