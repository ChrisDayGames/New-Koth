using Entitas;
using Entitas.CodeGeneration.Attributes;


[Time, Unique, Event(false)]
public class TickComponent : IComponent {
	
	public int value;

}

[Time, Unique]
public class AlphaComponent : IComponent {

	public float value;

}

[Time, Unique, Event(false)]
public class PausedComponent : IComponent {}

[Time, Unique]
public class JumpInTimeComponent : IComponent {
	public int targetTick;
}

[Time, Unique]
public class LogicSystemComponent : IComponent {

	public Systems system;

}

[Time, Unique]
public class FutureLogicSystemComponent : IComponent {

	public Systems system;

}

