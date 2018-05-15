using Entitas;

public class SpawnSystems : Feature {

	public SpawnSystems (Contexts contexts) : base ("SpawnSystems") {

		Add (new CreateLevelSystem (contexts));
		Add (new RespawnSystem (contexts));

	}

}