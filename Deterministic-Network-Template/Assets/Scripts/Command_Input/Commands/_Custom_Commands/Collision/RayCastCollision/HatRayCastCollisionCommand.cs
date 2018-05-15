using Determinism;
using Entitas;
using UnityEngine;
using EZCameraShake;

namespace CommandInput {

	public class HatRayCastCollisionCommand : RayCastCollisionCommand {

		public HatRayCastCollisionCommand (int _e) 
			: base(_e) {

			priority = (int) Priority.VERY_FAST;
		}

		public override void OnCollisionEnter (RaycastCollisionInfo info) {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (!e.isAttached && info.CollidesHorizontal () && e.velocity.value.x != 0) {

				e.ReplaceVelocity (e.velocity.value.SetX (-e.velocity.value.x.Mul(e.reflectionDampening.xDampening)));
				if (e.hasThrowTimer)
					e.RemoveThrowTimer ();

			}

			if (!e.isAttached && info.CollidesVertical () && e.velocity.value.y != 0)  {


//				if (e.hasThrowTimer && e.throwTimer.timeLeft - FixedMath.Tenth > 0)
//					e.ReplaceVelocity (e.velocity.value.FlipY ());
//				else {
//					e.ReplaceVelocity (e.velocity.value.SetY (-e.velocity.value.y.Mul(e.reflectionDampening.yDampening / 2)));
//				}

				if (info.CollidesWithVertical (Tag.HAT)) {
					
					e.ReplaceVelocity (e.velocity.value.SetY (-e.velocity.value.y.Mul(e.reflectionDampening.yDampening / 2)));

				} else {
					
					e.ReplaceVelocity (e.velocity.value.SetY (0));

				}

			}

			if (!e.isAttached && info.down != Tag.NONE)  {

				e.isGrounded = true;

			} else {

				e.isGrounded = false;

			}

			if (e.isDangerous) {

				Hit (entityID, info.leftID, info.horizontalHit);
				Hit (entityID, info.rightID, info.horizontalHit);
				Hit (entityID, info.upID, info.verticalHit);
				Hit (entityID, info.downID, info.verticalHit);

			}

		}

		public void Hit (int eID, int otherID, FixedVector2 hitPoint) {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(eID);
			LogicEntity other = Contexts.sharedInstance.logic.GetEntityWithId(otherID);

			if (other == null) return;

			if (other.hasFollowPoint && other.isAttached)
				other =  Contexts.sharedInstance.logic.GetEntityWithId(other.followPoint.targetID);

			if (!other.isHitable) return;


//			FixedVector2 offset = new FixedVector2 (
//				(e.collider.value.halfExtents.x / 2) * e.velocity.value.x.Sign (),
//				//- (e.collider.value.halfExtents.y / 2)
//				0
//			);
			FixedVector2 offset = new FixedVector2 (
				//(e.collider.value.halfExtents.x / 2) * e.velocity.value.x.Sign (),
				//- (e.collider.value.halfExtents.y / 2)
				0,
				-FixedMath.Create (1, 10)
			);

			long knockback = e.knockBack.value.Mul (e.weight.value);

			FixedVector2 direction = (hitPoint - (e.position.value + offset)).normalized;

			if (other.hasHat) {

				other.ReplaceVelocity (direction * knockback);
				e.ReplaceFreeze (5);
				other.ReplaceFreeze (5);

				LogicEntity otherHat = Contexts.sharedInstance.logic.GetEntityWithId(other.hat.entityID);

				if (otherHat.isAttached) {
					
					otherHat.ReplaceVelocity ((other.velocity.value.FlipX ().FlipY ()));
					otherHat.ReplaceFreeze (5);
					otherHat.throwMovement.throwPositionY = other.position.value.y;
					otherHat.isAttached = false;
					otherHat.isFalling = true;

				}

				other.isStunned = true;
				other.ReplaceStunTimer (e.stunTime.value);
				other.ReplaceCurrentMovementX (
					other.velocity.value.x, 
					other.stunMovement.accelerationTime, 
					0);

			} else if (other.isDangerous) {

				e.ReplaceFreeze (5);
				other.ReplaceFreeze (5);

			} else {


				direction = e.position.value - other.position.value;
				other.ReplaceVelocity (direction * knockback/2);

			}

			if (e.hasThrowTimer)
				e.RemoveThrowTimer ();

		}	

	}
		
}