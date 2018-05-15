using Entitas;

public class ProcessInputSystems : Feature {

	public ProcessInputSystems (Contexts contexts) : base ("ProcessInputSystems") {

        Add (new PlayerDataSystem (contexts));    

		Add (new EmitCommandSystem (contexts));
		//Add (new EmitStateCommandSystem (contexts));
		Add (new CreatePlayerSystem (contexts));

		Add (new RunCommandSystem (contexts));

	}

}