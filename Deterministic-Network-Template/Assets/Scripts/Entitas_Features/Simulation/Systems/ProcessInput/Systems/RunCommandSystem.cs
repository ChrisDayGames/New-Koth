using System.Collections.Generic;
using Entitas;
using CommandInput;
using System.Linq;

public class RunCommandSystem : IExecuteSystem {

	readonly InputContext _context;

	public RunCommandSystem (Contexts contexts) {
		_context = contexts.input;
	}

	public void Execute () {

		_context.commandList.commands = _context.commandList.commands.OrderBy (o => (int) o.priority).ToList ();

		foreach (Command c in _context.commandList.commands) {

			c.Execute ();

		}

		_context.ReplaceCommandList (new List<Command>());

	}

}