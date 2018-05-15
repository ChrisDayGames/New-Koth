using System.Collections.Generic;
using CommandInput;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Logic]
public sealed class PlayerID : IComponent {

	[EntityIndex]
	public int id;

}

[Logic]
public sealed class ControllableComponent : IComponent {}



[Input, Unique]
public sealed class CommandListComponent : IComponent {

	public List <Command> commands;

}

[Logic]
public sealed class AButtonDownComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class AButtonPressedComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class AButtonReleasedComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class BButtonDownComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class BButtonPressedComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class BButtonReleasedComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class JoystickMoveComponent : IComponent {

	public System.Type command;

}

[Logic]
public sealed class StateCommandComponent : IComponent {

    public Command command;

}

[Input]
public sealed class PlayerDataComponent : IComponent {

    public PlayerData info;

}