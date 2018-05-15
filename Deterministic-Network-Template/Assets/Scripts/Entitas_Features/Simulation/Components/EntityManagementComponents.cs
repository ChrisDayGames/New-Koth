using Entitas;
using Entitas.CodeGeneration.Attributes;

[Logic, Input]
public class DestroyedComponent : IComponent {}

[Logic, Event (true)]
public sealed class DirtyComponent : IComponent {}

[Logic, Input]
public class ResetComponent : IComponent {}


