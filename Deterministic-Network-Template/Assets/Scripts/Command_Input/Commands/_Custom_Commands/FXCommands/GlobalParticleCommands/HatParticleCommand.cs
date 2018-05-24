using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class HatParticleCommand : ParticleFXCommand {

		private OnBeginFXModule attachFX;
		private OnBeginFXModule hitLevelFX;
        private OnBeginFXModule hitPlayerFX;
        private OnBeginFXModule hitHatFX;
        private OnBeginFXModule throwFX;
		private OnBeginFXModule hatDeathFX;

        private OnRepeatFXModule groundFX;
		private OnRepeatFXModule airFX;

		private OnEndFXModule endDangerousFX;


		public HatParticleCommand (int _e) 
			: base(_e) {

			attachFX = new OnBeginFXModule (PickUpFX);
			hitLevelFX = new OnBeginFXModule (HitLevelFX);
            hitPlayerFX = new OnBeginFXModule(HitPlayerFX);
            hitHatFX = new OnBeginFXModule(HitHatFX);
            throwFX = new OnBeginFXModule(ThrowFX);
			hatDeathFX = new OnBeginFXModule(HatDeathFX);

            groundFX = new OnRepeatFXModule (FrictionFX, 2);
			airFX = new OnRepeatFXModule (EmptyFunction);

			endDangerousFX = new OnEndFXModule (DangerOverFX);

            priority = (int) Priority.VERY_SLOW;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			hatDeathFX.Update(e.isDead, e);
            
            hitLevelFX.Update(!e.collisionInfo.value.DoesntCollide(), e);
            hitHatFX.Update(e.collisionInfo.value.CollidesWith(Tag.HAT), e);
            hitPlayerFX.Update(e.collisionInfo.value.CollidesWith(Tag.PLAYER), e);

            if (e.isDead || e.hasFreeze) return;

			attachFX.Update (e.isAttached, e);
            throwFX.Update(e.isDangerous && !e.isAttached, e);

			groundFX.Update (e.isGrounded && Mathf.Abs(e.velocity.value.x) > 2, e);
			airFX.Update (e.isGrounded, e);

			endDangerousFX.Update (e.isDangerous, e);
			
		}

		public override void Undo () {

		}

		public void EmptyFunction (LogicEntity e) { 

			//get required data from the entity

		}

        public void ThrowFX(LogicEntity e) {

            GlobalVFX.PlayThrowFXParticles(

                        e.position.value.ToVector3(),
                        e.direction.value,
                        e.collider.value.halfExtents.ToVector3(),
                        e.velocity.value.ToVector2()                     

                    );

        }

        public void FrictionFX(LogicEntity e) {

            GlobalVFX.PlayHatGroundFrictionParticles(

                e.position.value.ToVector3(),
                e.direction.value,
                e.collider.value.halfExtents.ToVector3(),
                e.velocity.value.ToVector2(),
                e.isDangerous

            );

        }

        public void HitLevelFX(LogicEntity e) {

            if (e.isAttached) return;

            if (e.collisionInfo.value.CollidesWith(Tag.DEFAULT)) {

                if (e.collisionInfo.value.CollidesHorizontal()) {

                    GlobalVFX.PlayHatHitWallHorizontal(

                        e.position.value.ToVector3(),
                        e.direction.value,
                        e.collider.value.halfExtents.ToVector3(),
                        e.velocity.value.ToVector2(),
                        e.isDangerous

                    );

                } else if (e.collisionInfo.value.up == Tag.DEFAULT) {

                    GlobalVFX.PlayHatHitWallVertical(

                        e.position.value.ToVector3(),
                        e.direction.value,
                        e.collider.value.halfExtents.ToVector3(),
                        e.velocity.value.ToVector2(),
                        e.isDangerous

                    );
                }

            }

        }

        public void HitPlayerFX(LogicEntity e) {

            if(e.isDangerous) {

                if (e.collisionInfo.value.CollidesWithHorizontal(Tag.PLAYER)) {

                    LogicEntity otherPlayer = Contexts.sharedInstance.logic.GetEntityWithId(e.collisionInfo.value.GetHorizontalEntity(Tag.PLAYER));

                    GlobalVFX.PlayDangerousHatHitPlayerParticles(

                        otherPlayer.position.value.ToVector3(),
                        e.direction.value,
                        e.collider.value.halfExtents.ToVector3(),
                        otherPlayer.velocity.value.ToVector2()


                    );

                } 
                
                if(e.collisionInfo.value.CollidesWithVertical(Tag.PLAYER)) {

                    LogicEntity otherPlayer = Contexts.sharedInstance.logic.GetEntityWithId(e.collisionInfo.value.GetVerticalEntity(Tag.PLAYER));

                    GlobalVFX.PlayDangerousHatHitPlayerParticles(

                        otherPlayer.position.value.ToVector3(),
                        e.direction.value,
                        e.collider.value.halfExtents.ToVector3(),
                        otherPlayer.velocity.value.ToVector2()


                    );

                }

            }

        }

        public void HitHatFX(LogicEntity e) {

            if(e.isDangerous) {

                if(e.collisionInfo.value.CollidesWithHorizontal(Tag.HAT)) {

                    LogicEntity otherHat = Contexts.sharedInstance.logic.GetEntityWithId(e.collisionInfo.value.GetHorizontalEntity(Tag.HAT));

                    if (otherHat.isAttached) {

                        LogicEntity otherPlayer = Contexts.sharedInstance.logic.GetEntityWithId(otherHat.followPoint.targetID);

                        GlobalVFX.PlayDangerousHatHitPlayerParticles(

                            otherPlayer.position.value.ToVector3(),
                            e.direction.value,
                            e.collider.value.halfExtents.ToVector3(),
                            otherPlayer.velocity.value.ToVector2()

                        );

                    } else {

                        

                    }

                } 
                
                if(e.collisionInfo.value.CollidesWithVertical(Tag.HAT)) {

                    LogicEntity otherHat = Contexts.sharedInstance.logic.GetEntityWithId(e.collisionInfo.value.GetVerticalEntity(Tag.HAT));

                    if (otherHat.isAttached) {

                        LogicEntity otherPlayer = Contexts.sharedInstance.logic.GetEntityWithId(otherHat.followPoint.targetID);

                        GlobalVFX.PlayDangerousHatHitPlayerParticles(

                            otherPlayer.position.value.ToVector3(),
                            e.direction.value,
                            e.collider.value.halfExtents.ToVector3(),
                            otherPlayer.velocity.value.ToVector2()

                        );

                    } else {

                        

                    }
                
                }

            } else {
                
            }

            

        }

        public void HatDeathFX (LogicEntity e) {

			GlobalVFX.PlayHatDeathParticles(

				e.position.value.ToVector3(),
				e.direction.value,
				e.collider.value.halfExtents.ToVector3(),
				e.velocity.value.ToVector2()

			);

		}

        public void DangerOverFX(LogicEntity e) {

            if (e.isAttached) return;

            GlobalVFX.PlayDangerFinishParticles(

                e.position.value.ToVector3(),
                e.direction.value,
                e.collider.value.halfExtents.ToVector3(),
                e.velocity.value.ToVector2()

            );

        }

        public void PickUpFX(LogicEntity e) {

            LogicEntity owner = Contexts.sharedInstance.logic.GetEntityWithId(e.followPoint.targetID);

            GlobalVFX.PlayPickUpParticles(

                e.position.value.ToVector3(),
                e.direction.value,
                e.collider.value.halfExtents.ToVector3(),
                e.velocity.value.ToVector2(),
                e

            );

        }

    }

}