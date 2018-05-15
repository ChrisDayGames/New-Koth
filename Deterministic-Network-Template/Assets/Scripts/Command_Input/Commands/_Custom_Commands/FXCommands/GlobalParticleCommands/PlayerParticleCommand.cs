using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class PlayerParticleCommand : ParticleFXCommand {

		private OnBeginFXModule dashFX;
		private OnBeginFXModule jumpFX;
		private OnBeginFXModule wallJumpFX;
		private OnBeginFXModule deathFX;

        private OnRepeatFXModule stunFX;
		private OnRepeatFXModule runFX;
		private OnRepeatFXModule wallrideFX;
		private OnRepeatFXModule sludgeFX;

		private OnEndFXModule fallFX;

		public PlayerParticleCommand (int _e) 
			: base(_e) {

			dashFX = new OnBeginFXModule (DashBeginFX);
			jumpFX = new OnBeginFXModule (JumpBeginFX);
			wallJumpFX = new OnBeginFXModule (JumpBeginFX);
            deathFX = new OnBeginFXModule (DeathFX);
			
            stunFX = new OnRepeatFXModule (StunRepeatFX, 4);
			runFX = new OnRepeatFXModule (RunRepeatFX, 10);
			wallrideFX = new OnRepeatFXModule (WallRideRepeatFX, 6);
			sludgeFX = new OnRepeatFXModule (SludgeFX, 1);
			
			fallFX = new OnEndFXModule (FallEndFX);

		}

		long lastJumpsCompleted = 0;

		public override void Execute () {

			//get a reference to the entity
			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

            //when the soul hits you
            deathFX.Update(!e.hasFreeze && e.hasDeathTimer, e);
			
            //when you start sludging
            sludgeFX.Update (!e.hasFreeze && e.hasDeathTimer, e);

			if (e.isDead || e.hasFreeze) return;

			//jump
			jumpFX.Update (e.jumpsCompleted.value != 0 && (int) e.jumpsCompleted.value != lastJumpsCompleted, e);
			lastJumpsCompleted = e.jumpsCompleted.value;

			//wall jump
			wallJumpFX.Update (e.isWallJumping, e);
            
			//stun
			stunFX.Update (e.isStunned, e);

			//run
			runFX.Update (e.isGrounded && e.velocity.value.x.Abs () >= FixedMath.Tenth, e);

			//dash
			dashFX.Update (e.isDashing && e.hasLastVelocity 
				&& (e.lastVelocity.value.x.Abs () < FixedMath.Thousandth || e.lastVelocity.value.x.Sign () != e.velocity.value.x.Sign ()), e);

			//wallride
			wallrideFX.Update (e.isWallRiding, e);

			//fall + land
			fallFX.Update (!e.isGrounded, e);
			
		}

		private void StunRepeatFX (LogicEntity e) {
			
			GlobalVFX.PlayStunnedParticles(e.position.value.ToVector3 ());

		}

		private void RunRepeatFX (LogicEntity e) {

			GlobalVFX.PlayRunParticles (

				e.position.value.ToVector3 (),
				e.direction.value,
				e.collider.value.halfExtents.ToVector3 ()

			);

		}

		private void DashBeginFX (LogicEntity e) {

			GlobalVFX.PlayDashParticles (

				e.position.value.ToVector3 (),
				e.direction.value,
				e.collider.value.halfExtents.ToVector3 (),
				e.velocity.value.ToVector3 ()

			);

		}

		private void WallRideRepeatFX (LogicEntity e) {
			
			//if (playerInfo.velocity.y <= 0)
			GlobalVFX.PlayWallSlideDust (

				e.position.value.ToVector3 (),
				e.direction.value,
				e.collider.value.halfExtents.ToVector3 (),
				e.velocity.value.ToVector3 ()

			);
			
		}

		private void JumpBeginFX (LogicEntity e) {

			GlobalVFX.PlayJumpParticles (

				e.position.value.ToVector3 (),
				e.direction.value,
				e.collider.value.halfExtents.ToVector3 (),
				e.velocity.value.ToVector3 (),
				e.jumpsCompleted.value.ToInt ()

			);

		}

		private void FallEndFX (LogicEntity e) {

			if (e.isFastFalling) {

				GlobalVFX.PlayLandFastFallDust (

					e.position.value.ToVector3 (),
					e.direction.value,
					e.collider.value.halfExtents.ToVector3 (),
					e.velocity.value.ToVector3 ()

				);

			} else {

				GlobalVFX.PlayLandDust (

					e.position.value.ToVector3 (),
					e.direction.value,
					e.collider.value.halfExtents.ToVector3 (),
					e.velocity.value.ToVector3 ()

				);

			}

		}

        private void DeathFX(LogicEntity e) {

            GlobalVFX.PlayPlayerDeathParticles(

                    e.position.value.ToVector3(),
                    e.direction.value,
                    e.collider.value.halfExtents.ToVector3(),
                    e.velocity.value.ToVector3()

            );

        }

		private void SludgeFX (LogicEntity e) {

            GlobalVFX.PlaySludgeParticles(

                    e.position.value.ToVector3(),
                    e.direction.value,
                    e.collider.value.halfExtents.ToVector3(),
                    e.velocity.value.ToVector3()

            );

        }

	}

}