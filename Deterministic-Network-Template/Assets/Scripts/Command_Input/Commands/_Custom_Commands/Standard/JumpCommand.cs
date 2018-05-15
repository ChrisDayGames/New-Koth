using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class JumpCommand : Command {

		long vy;
		long xInput;

		public JumpCommand (int _e, long _vy, long _axis = 0) 
			: base(_e) {

			this.vy = _vy;
			this.xInput = _axis;

			priority = (int) Priority.FAST;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.isStunned)
				return;

			if (e.isWallRiding && !e.isGrounded) {

				int walldirection = (e.collisionInfo.value.left == Tag.DEFAULT) ? 1 : -1;

				if (xInput != 0 && xInput.Sign() != walldirection)
					e.ReplaceVelocity (e.wallRideMovement.innerJumpVelocity);

				else if (xInput == 0)
					e.ReplaceVelocity (e.wallRideMovement.neutralJumpVelocity);
					
				else
					e.ReplaceVelocity (e.wallRideMovement.outerJumpVelocity);
					
				
				e.velocity.value.x *= walldirection;
				e.ReplaceCurrentMovementX (e.velocity.value.x, e.wallRideMovement.accelerationTime, 0);
				e.ReplaceWallJumpTimer (e.wallRideMovement.jumpLenth);

				e.isGrounded = false;
				e.isDashing = false;
				e.isFastFalling = false;
				e.isWallRiding = false;
				e.isWallJumping = true;
				
			} else if (e.jumpsCompleted.value < e.jumpsAllowed.value) {

				e.ReplaceVelocity(e.velocity.value.SetY (vy));
				e.ReplaceJumpsCompleted (++e.jumpsCompleted.value);

				e.isGrounded = false;
				e.isFastFalling = false;
				e.isDashing = false;

			}

		}

		public override void Undo () {

		}

	}

}