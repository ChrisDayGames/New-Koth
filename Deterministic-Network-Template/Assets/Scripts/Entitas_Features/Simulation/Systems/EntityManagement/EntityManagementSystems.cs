using Entitas;

public class EntityManagementSystems : Feature {

	public EntityManagementSystems (Contexts contexts) : base ("EntityManagementSystem") {

		Add (new MultiDestroySystem (contexts));
		Add (new SetDirtySystem (contexts));

	}

}