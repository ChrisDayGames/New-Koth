using Determinism;
using Entitas;

public class InputSystems : Feature {

	public InputSystems (Contexts contexts) : base ("Input Systems") {

		Add (new EmitControllerInputSystem (contexts));

	}

}
