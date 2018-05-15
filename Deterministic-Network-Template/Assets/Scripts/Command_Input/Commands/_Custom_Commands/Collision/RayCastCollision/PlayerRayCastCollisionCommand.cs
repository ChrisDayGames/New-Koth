using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class PlayerRayCastCollisionCommand : RayCastCollisionCommand  {

		public const int DEATH_FREEZE_FRAMES = 60; // 60 frames ~ 1.1 seconds (50 frames per second)
		public const long DEATH_TIME = FixedMath.ONE * 4; //4 seconds


		public PlayerRayCastCollisionCommand (int _e) 
			: base(_e) {

			priority = (int) Priority.FAST;
		}

		public override void OnCollisionEnter (RaycastCollisionInfo info) {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.isStunned) {
				
				if (!info.CollidesWithHorizontal (Tag.HAT)
				&& info.CollidesHorizontal ()) {
			
					e.ReplaceVelocity (new FixedVector2 (

						-e.velocity.value.x.Mul (e.reflectionDampening.xDampening),
						e.velocity.value.y + (e.velocity.value.y.Sign () * e.velocity.value.x.Abs ()).Mul (e.reflectionDampening.yDampening))

					);

				}

				if (!info.CollidesWithVertical (Tag.HAT)
					&& info.up != Tag.NONE) {

					e.ReplaceVelocity (e.velocity.value.SetY (0));

				}

				return;

			}

			LogicEntity leftEntity = Contexts.sharedInstance.logic.GetEntityWithId(info.leftID);
			LogicEntity rightEntity = Contexts.sharedInstance.logic.GetEntityWithId(info.rightID);
			LogicEntity upEntity = Contexts.sharedInstance.logic.GetEntityWithId(info.upID);
			LogicEntity downEntity = Contexts.sharedInstance.logic.GetEntityWithId(info.downID);

			if (!info.CollidesWithVertical (Tag.HAT) && info.CollidesVertical ())
				e.ReplaceVelocity (e.velocity.value.SetY (0));

			if ((info.left != Tag.NONE || info.right != Tag.NONE)) {

				if (info.horizontalHit.y > e.position.value.y) {

					e.isWallRiding = true;
					e.isDashing = false;

				}
					
			}

			if ((info.down == Tag.HAT && info.downID != e.hat.entityID) && !downEntity.isDangerous && !downEntity.isInvincible) {
				
				LogicEntity hat = Contexts.sharedInstance.logic.GetEntityWithId(info.downID);
				hat.isDead = true;
				hat.ReplaceDeathTimer (DEATH_TIME);

				LogicEntity player =  Contexts.sharedInstance.logic.GetEntityWithId(hat.followPoint.targetID);
				player.isDead = true;
				player.ReplaceFreeze (DEATH_FREEZE_FRAMES);
				player.ReplaceDeathTimer (DEATH_TIME);

			}

			if (info.down == Tag.DEFAULT) {
				
				e.ReplaceJumpsCompleted (0);
				e.isGrounded = true;
				e.isWallRiding = false;

			} else if ((info.down == Tag.HAT || info.down == Tag.PLAYER) && !downEntity.isDangerous) {

				//Bounce
				if (info.down == Tag.PLAYER && !e.isStunned) {
					
					downEntity.isStunned = true;
					downEntity.ReplaceStunTimer (e.stunTime.value);	
					downEntity.ReplaceCurrentMovementX (0, 0, 0);
					downEntity.ReplaceVelocity (downEntity.velocity.value.SetX (0));

				}

				e.ReplaceVelocity (e.velocity.value.SetY(e.bounceVelocity.value));
				e.jumpsCompleted.value = 0;

				e.isGrounded = false;
				e.isDashing = false;

			} else {

				e.isGrounded = false;
				e.isDashing = false;

			}

			if (e.hasPusher) {

				if (info.left != Tag.NONE && leftEntity.isPusheable && !leftEntity.isStunned && !leftEntity.isDangerous)
					e.pusher.passengers.Add (new Passenger(info.leftID, true, false));
				
				if (info.right != Tag.NONE && rightEntity.isPusheable && !rightEntity.isStunned  && !rightEntity.isDangerous)
					e.pusher.passengers.Add (new Passenger(info.rightID, true, false));
				
				if (info.up != Tag.NONE && upEntity.isPusheable && !upEntity.isStunned && !upEntity.isDangerous)
					e.pusher.passengers.Add (new Passenger(info.upID, false, true));
				
				if (info.down != Tag.NONE && downEntity.isPusheable && !downEntity.isStunned && !downEntity.isDangerous)
					e.pusher.passengers.Add (new Passenger(info.downID, false, true));

				e.ReplacePusher (e.pusher.passengers);

			}

		}

	}

}