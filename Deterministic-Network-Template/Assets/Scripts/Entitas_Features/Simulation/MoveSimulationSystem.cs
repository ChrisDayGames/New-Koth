using System.Collections.Generic;
using Entitas;

public class MoveSimulationSystem : ReactiveSystem <TimeEntity> {

	readonly TimeContext _context;

	public MoveSimulationSystem (Contexts contexts) : base (contexts.time) {

		_context = contexts.time;

	}

	protected override ICollector<TimeEntity> GetTrigger (IContext<TimeEntity> context) {

		return context.CreateCollector (TimeMatcher.Tick);

	}

	protected override bool Filter (TimeEntity entity){

		return entity.hasTick;

	}

	protected override void Execute (List<TimeEntity> notNeeded) {

		_context.logicSystem.system.Execute ();
		_context.logicSystem.system.Cleanup ();

	}

}