using Entitas;

public class TimerSystems : Feature {

	public TimerSystems (Contexts contexts) : base ("TimerSystem") {

		Add (new EndInvincibilityTimer (contexts));
		Add (new EndThrowSystem (contexts));
		Add (new EndStunSystem (contexts));
		Add (new EndDashSystem (contexts));
		Add (new EndWallJumpSystem (contexts));
		Add (new GetOffWallSystem (contexts));
		Add (new EndDeathSystem (contexts));
		Add (new EndFreezeSystem (contexts));

	}

}