using Determinism;
using Entitas;

public class TimeSystems : Feature {

	public TimeSystems (Contexts contexts) : base ("Time Systems") {

		Add (new ReplaySystem (contexts));
		Add (new TickSystem (contexts));
		Add (new ShowFutureSystem (contexts));

	}

}
