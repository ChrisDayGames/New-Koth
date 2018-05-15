using Entitas;

public class SimulationSystems : Feature {

	public SimulationSystems (Contexts contexts) : base ("SimulationSystems") {

		//timers
		Add (new TimerSystems (contexts));

		Add (new EntityManagementSystems (contexts));

		Add (new ProcessInputSystems (contexts));

		Add (new MovementSystems (contexts));
		Add (new CollisionSystems (contexts));
		Add (new SpawnSystems (contexts));

	}

}