using Determinism;
using Entitas;

public class MovementSystems : Feature {

	public MovementSystems (Contexts contexts) : base ("Movement Systems") {

		//Begin
		Add (new CalculateJumpHeightSystem (contexts));
		Add (new InvincibleOnDangerousSystem (contexts));

		//Acceleration
		Add (new GravitySystem (contexts));
		Add (new SlowDownSystem (contexts));
		Add (new FollowPointSystem (contexts));
		Add (new AccelerationSystem (contexts));

		//Velocity
		Add (new SpeedLimitSystem (contexts));
		Add (new TerminalVelocitySystem (contexts));
		Add (new VelocitySystem (contexts));
		Add (new PrepareColliderSystem (contexts));
		Add (new PushSystem (contexts));

		//collision
		Add (new DeathCollisionSystem (contexts));
		Add (new TryMoveSystem (contexts));

		//remove wallride
		Add (new ResetWallRideSystem (contexts));

	}

}
