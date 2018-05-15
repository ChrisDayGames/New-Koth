using System.Collections.Generic;
using CommandInput;
using Determinism;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Logic, Event(true)]
public sealed class PositionComponent :IComponent {

	public FixedVector2 value;

}
	
[Logic, Event(true)]
public sealed class RotationComponent :IComponent {

	public long value;

}

[Logic, Event(true)]
public sealed class ScaleComponent :IComponent {

	public FixedVector2 value;

}

[Logic]
public sealed class LastPositionComponent : IComponent {

	public FixedVector2 value;

}

[Logic]
public sealed class LastRotationComponent :IComponent {

	public long value;

}

[Logic]
public sealed class VelocityComponent :IComponent {

	public FixedVector2 value;

}

[Logic]
public sealed class LastVelocityComponent : IComponent {

	public FixedVector2 value;

}

[Logic]
public sealed class AccelerationComponent : IComponent {

	public FixedVector2 value;

}

[Logic, Event(true)]
public sealed class DirectionComponent :IComponent {

	public int value;

}

[Logic]
public sealed class JumpsAllowedComponent : IComponent {

	public long value;

}

[Logic]
public sealed class JumpsCompletedComponent : IComponent {

	public long value;

}


[Logic]
public sealed class TimeToApex : IComponent {

	public long value;

}

[Logic]
public sealed class MinJump : IComponent {

	public long value;

}

[Logic]
public sealed class MinJumpVelocity : IComponent {

	public long value;

}

[Logic]
public sealed class MaxJump : IComponent {

	public long value;

}

[Logic]
public sealed class MaxJumpVelocity : IComponent {

	public long value;

}

[Logic]
public sealed class BounceHeightComponent : IComponent {
	public long value;

}

[Logic]
public sealed class BounceVelocityComponent : IComponent {

	public long value;

}

[Logic]
public sealed class Gravity : IComponent {

	public long value;

}

[Logic]
public sealed class TerminalVelocityComponent : IComponent {

	public long value;

}

[Logic]
public sealed class FastFallTerminalVelocityComponent : IComponent {

	public long value;

}

[Logic]
public sealed class Falling : IComponent {}

[Logic]
public sealed class FastFalling : IComponent {}

[Logic]
public sealed class FastFallFactor : IComponent {

	public long value;

}

[Logic]
public sealed class BouncingComponent : IComponent {}
	
[Logic]
public sealed class MovableComponent : IComponent {}

[Logic]
public sealed class MoveComponent : IComponent {
	public FixedVector2 target;
}

[Logic]
public sealed class MoveCompleteComponent : IComponent {}


[Logic]
public sealed class CurrentMovementXComponent : IComponent {

	public long targetSpeed;
	public long accelerationTime;
	public long refSpeed;

}

[Logic]
public sealed class CurrentMovementYComponent : IComponent {

	public long targetSpeed;
	public long accelerationTime;
	public long refSpeed;

}

[Logic]
public sealed class GroundedComponent : IComponent {}

[Logic]
public sealed class GroundMovementComponent : IComponent {

	public long targetSpeed;
	public long accelerationTime;

}
	
[Logic]
public sealed class AirMovementComponent : IComponent {

	public long targetSpeed;
	public long accelerationTime;

}

[Logic]
public sealed class DashingComponent : IComponent {}

[Logic]
public sealed class DashTimerComponent : IComponent {
	
	public long timeLeft;

}

[Logic]
public sealed class DashMovementComponent : IComponent {

	public long targetSpeed;
	public long accelerationTime;
	public long length;

}

[Logic]
public sealed class WallRidingComponent : IComponent {}

[Logic]
public sealed class WallJumpingComponent : IComponent {}

[Logic]
public sealed class WallJumpTimerComponent : IComponent {

	public long timeLeft;

}

[Logic]
public sealed class WallStickTimerComponent : IComponent {

	public long timeLeft;

}

[Logic]
public sealed class WallRideMovementComponent : IComponent {

	public long fallSpeed;
	public long fastFallFactor;
	public FixedVector2 innerJumpVelocity;
	public FixedVector2 neutralJumpVelocity;
	public FixedVector2 outerJumpVelocity;
	public long accelerationTime;
	public long jumpLenth;
	public long stickTime;

}

[Logic, Event(true), Event(true, EventType.Removed)]
public sealed class StunnedComponent : IComponent {}

[Logic]
public sealed class StunMovementComponent : IComponent {

	public long targetSpeed;
	public long accelerationTime;

}

[Logic]
public sealed class StunTimerComponent : IComponent {

	public long timeLeft;

}

[Logic, Event(true), Event(true, EventType.Removed)]
public sealed class FreezeComponent : IComponent {

	public int frames;

}

[Logic]
public sealed class AttachedComponent : IComponent {}

[Logic]
public sealed class FollowPointComponent : IComponent {

	public int targetID;
	public long followSpeed;
	public FixedVector2 offset;
	public long pickUpRadius;
	public long maxRotation;

}

[Logic]
public sealed class FrictionComponent : IComponent {
	
	public long friction;
	public long dangerousFriction;

}

[Logic]
public sealed class DragComponent : IComponent {

	public long drag;
	public long dangerousDrag;

}

[Logic, Event(true), Event(true, EventType.Removed)]
public sealed class DangerousComponent : IComponent {}

[Logic]
public sealed class HitableComponent : IComponent {}

[Logic]
public sealed class ThrowableComponent : IComponent {}

[Logic]
public sealed class ThrowMovementComponent : IComponent {

	public long power;
	public long throwPositionY;

}

[Logic]
public sealed class ThrowTimerComponent : IComponent {

	public long timeLeft;

}

[Logic]
public sealed class KnockBackComponent : IComponent {

	public long value;

}

[Logic]
public sealed class ReflectionDampeningComponent : IComponent {

	public long xDampening;
	public long yDampening;

}

[Logic, Event(true), Event(true, EventType.Removed)]
public sealed class StunTimeComponent : IComponent {

	public long value;

}

[Logic]
public sealed class HatComponent : IComponent {

	public int entityID;

}

[Logic, Event(true), Event(true, EventType.Removed)]
public sealed class InvincibleComponent : IComponent {}

[Logic]
public sealed class InvincibilityTimerComponent : IComponent {

	public long timeLeft;

}

[Logic, Event(true), Event(true, EventType.Removed)]
public sealed class DeadComponent : IComponent {}

[Logic]
public sealed class DeathTimerComponent : IComponent {

	public long timeLeft;

}

[Logic]
public sealed class WeightComponent : IComponent {

	public long value;

}
	
[Logic]
public sealed class Armoured : IComponent {}

[Logic]
public sealed class PusheableComponent : IComponent {}


[Logic]
public sealed class PusherComponent : IComponent {

	public List<Passenger> passengers;

}

public class Passenger {

	public int id;
	public bool h, v;

	public Passenger (int _id, bool _h, bool _v) {

		this.id = _id;
		this.h = _h;
		this.v = _v;

	}

}
	


