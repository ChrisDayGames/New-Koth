using Determinism;
using Entitas;

public class VictoryScreen : ReadyGate {

	#region implemented abstract members of ReadyGate

	public override void OnReady (bool isReady) {

		if (isReady)
			link.Click ();

	}

	#endregion

}
