using System.Collections.Generic;
using Determinism;
using CommandInput;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
public sealed class ControllerIDComponent : IComponent {

	[EntityIndex]
	public int id;

}

[Input, Event (false)]
public sealed class ControllerInputComponent : IComponent {

	public InputSnapshot snapshot;

}

[Input]
public sealed class AssignedToPlayer : IComponent {}


[Input, Unique]
public sealed class InputHistory : IComponent {

	public List<StoredInput> snapshots = new List <StoredInput> ();

}

[Input]
public sealed class ValidInput : IComponent {}

public struct StoredInput {

	public int tick;
	public int playerID;
	public InputSnapshot snapshot;

	public StoredInput (int _tick, int _playerID, InputSnapshot _snapshot) {

		this.tick = _tick;
		this.playerID = _playerID;
		this.snapshot = new InputSnapshot ();
		this.snapshot.Copy (_snapshot);

	}

}

