using Determinism;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Logic, Input, Time]
public sealed class IdComponent : IComponent {
	
	[PrimaryEntityIndex]
	public int value;

}