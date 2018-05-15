using Entitas;
using CommandInput;

public class EmitStateCommandSystem : IExecuteSystem {

	readonly InputContext _context;
	readonly IGroup <LogicEntity> _stateChangers;

	public EmitStateCommandSystem (Contexts contexts) {

		_context = contexts.input;
		_stateChangers = contexts.logic.GetGroup (LogicMatcher.StateCommand);


	}
	
	public void Execute () {

		if (_stateChangers.count == 0) return;

		foreach (LogicEntity e in _stateChangers.GetEntities()) {
			_context.commandList.commands.Add (e.stateCommand.command);
		}

	}

}

