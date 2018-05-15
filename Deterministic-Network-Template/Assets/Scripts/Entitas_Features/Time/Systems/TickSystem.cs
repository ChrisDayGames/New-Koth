using Entitas;

public class TickSystem : IInitializeSystem, IExecuteSystem {

	readonly TimeContext _context;

	public TickSystem (Contexts contexts) {

		_context = contexts.time;

	}

	public void Initialize () {

		_context.SetTick (0);

	}
	
	public void Execute () {

		if (!_context.isPaused)
			_context.ReplaceTick (_context.tick.value + 1);

	}

}
