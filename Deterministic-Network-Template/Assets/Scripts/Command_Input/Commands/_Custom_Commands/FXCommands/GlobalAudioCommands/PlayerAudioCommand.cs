using System;
using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public static class PlayerSounds {

		public const string JUMP_SOUND = "Jump";
        public const string WALLJUMP_SOUND = "Jump";
        public const string LAND_SOUND = "Land";
        public const string THROW_SOUND = "Throw";
        public const string DEATH_SOUND = "Death";
        public const string WIN_SOUND = "Win";
        public const string FOOTSTEPS_SOUND = "Footsteps";
        public const string DASH_SOUND = "Dash";
        public const string FOOTSTOOL_SOUND = "Footstool";
        public const string FASTFALL_SOUND = "Fastfall";

    }


    public class PlayerAudioCommand : AudioFXCommand {

		private OnBeginFXModule jumpFX;
        private OnBeginFXModule walljumpFX;
        private OnBeginFXModule landFX;
        private OnBeginFXModule throwFX;
        private OnBeginFXModule deathFX;
        private OnBeginFXModule winFX;
        private OnBeginFXModule dashFX;
        private OnBeginFXModule footstoolFX;
        private OnBeginFXModule fastfallFX;
        private OnBeginFXModule wallrideFX;

        private OnRepeatFXModule footstepsFX;


        public PlayerAudioCommand (int _e, string _name, GameObject _go) 
			: base(_e, _name, _go) {

			jumpFX = new OnBeginFXModule (OnBeginJump);
            walljumpFX = new OnBeginFXModule(OnBeginWallJump);
            landFX = new OnBeginFXModule(OnBeginLand);
            throwFX = new OnBeginFXModule(OnBeginThrow);
            deathFX = new OnBeginFXModule(OnBeginDeath);
            winFX = new OnBeginFXModule(OnBeginWin);
            dashFX = new OnBeginFXModule(OnBeginDash);
            fastfallFX = new OnBeginFXModule(OnBeginFastfall);
            footstoolFX = new OnBeginFXModule(OnBeginFootstool);
            wallrideFX = new OnBeginFXModule(OnBeginWallride);



            footstepsFX = new OnRepeatFXModule(OnRepeatFootsteps, 10);


        }


        long lastJumpsCompleted = 0;

		public override void Execute () {
			
			//get a reference to the entity
			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

            //jump logic
			jumpFX.Update (e.jumpsCompleted.value != 0 && (int) e.jumpsCompleted.value != lastJumpsCompleted, e);
            lastJumpsCompleted = e.jumpsCompleted.value;

            //walljump logic
            walljumpFX.Update(e.isWallJumping, e);

            //footsteps logic
            footstepsFX.Update(e.isGrounded && e.velocity.value.x.Abs() >= FixedMath.Tenth, e);

            //dash
            dashFX.Update(e.isDashing && e.hasLastVelocity
                && (e.lastVelocity.value.x.Abs() < FixedMath.Thousandth || e.lastVelocity.value.x.Sign() != e.velocity.value.x.Sign()), e);

            //wallride
            wallrideFX.Update(e.isWallRiding, e);

            //land 
            landFX.Update(!e.isGrounded, e);

            /* need to get the while wall ride is happening code */



            //landFX.Update (e.isGrounded, e);





        }

        private void OnBeginWallride(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.JUMP_SOUND, go);
        }

        private void OnRepeatFootsteps(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        public void OnBeginJump (LogicEntity e) {

            //Wise play sound (JUMP_SOUND + name)
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.JUMP_SOUND, go);   

		}

        public void OnBeginWallJump(LogicEntity obj) {

            //Wise play sound (JUMP_SOUND + name)
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.WALLJUMP_SOUND, go);

        }

        public void OnBeginLand (LogicEntity e) {

            //Wise play sound (JUMP_SOUND + name)
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        private void OnBeginFootstool(LogicEntity obj) {

            //Wise play sound (JUMP_SOUND + name)
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        private void OnBeginFastfall(LogicEntity obj) {

            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        private void OnBeginDash(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        private void OnBeginWin(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        private void OnBeginDeath(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }

        private void OnBeginThrow(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + PlayerSounds.LAND_SOUND, go);

        }






    }

}