using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class RunCommand : AxisCommand2D {

		public RunCommand (int _e, FixedVector2 _axes) 
			: base(_e, _axes) {

			priority = (int) Priority.MEDIUM;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.isStunned || e.hasFreeze)
				return;

			if (e.isGrounded) e.isWallRiding = false;
			if (axes.x.Abs () <= FixedMath.Create (1, 10)) axes.x = 0;
			if (axes.x.Abs () >= FixedMath.Create (6, 10)) axes.x = axes.x.Sign () * FixedMath.ONE;
			if (axes.x.Abs () <= FixedMath.Tenth) e.isDashing = false;

			if (e.isWallRiding) {

				int walldirection = (e.collisionInfo.value.left != Tag.NONE) ? 1 : -1;

				if (axes.x != 0 && axes.x.Sign() == walldirection && axes.x.Abs () > FixedMath.Create (3, 10) && !e.hasWallStickTimer) {

					e.AddWallStickTimer (e.wallRideMovement.stickTime);

				} else if (e.hasWallStickTimer && axes.x.Abs () <= FixedMath.Create (3, 10)) {

					e.RemoveWallStickTimer ();

				}

				e.ReplaceCurrentMovementX (
					0, 
					0, 
					e.currentMovementX.refSpeed
				);

				e.ReplaceDirection (-walldirection);
				e.ReplaceVelocity (e.velocity.value.SetX(0));

			} else if (e.isDashing) {

				e.ReplaceCurrentMovementX (
					
					e.currentMovementX.targetSpeed, 
					e.dashMovement.accelerationTime, 
					e.currentMovementX.refSpeed

				);

			} else if (e.isGrounded) {
			
				if (axes.x.Abs () == FixedMath.ONE
					&& (
						e.velocity.value.x.Abs () < FixedMath.Thousandth
						|| e.direction.value != axes.x.Sign ()
						|| (e.direction.value == axes.x.Sign () && e.velocity.value.x.Abs () < e.groundMovement.targetSpeed.Mul (FixedMath.Tenth * 9))

					)) {
						
					//e.ReplaceVelocity (e.velocity.value.SetX(0));
					e.ReplaceDashTimer (e.dashMovement.length);
					e.isDashing = true;

					e.ReplaceCurrentMovementX (
						
						e.dashMovement.targetSpeed * axes.x.Sign(), 
						e.dashMovement.accelerationTime,
						e.currentMovementX.refSpeed

					);

				} else if (e.isDashing) {

					if (e.collisionInfo.value.CollidesHorizontal ())
						e.isDashing = false;

				} else {
					
					e.ReplaceCurrentMovementX (
						
						e.groundMovement.targetSpeed.Mul (axes.x), 
						e.groundMovement.accelerationTime, 
						e.currentMovementX.refSpeed

					);

				}

			} 

			else if (!e.isGrounded){

				long accelerationtime = e.airMovement.accelerationTime;


				if (e.isWallJumping)
					accelerationtime = e.wallRideMovement.accelerationTime;	


				e.ReplaceCurrentMovementX (
					
					e.airMovement.targetSpeed.Mul (axes.x), 
					accelerationtime, 
					e.currentMovementX.refSpeed

				);

			}

		}

		public override void Undo () {

		}

	}

}