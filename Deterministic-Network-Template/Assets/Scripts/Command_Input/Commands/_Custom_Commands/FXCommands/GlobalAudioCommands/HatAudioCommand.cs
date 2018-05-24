using System;
using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

    public static class HatSounds {

        public const string THROW_SOUND = "Throw";
        public const string PLAYER_HIT_SOUND = "PlayerHit";
        public const string LEVEL_HIT_SOUND = "LevelHit";
        public const string HAT_HIT_SOUND = "Hit";
        public const string DEATH_SOUND = "Death";
        public const string PICKUP_SOUND = "Pickup";
        public const string EXTINGUISH_SOUND = "Extinguish";



    }

    public class HatAudioCommand : AudioFXCommand {

        private OnBeginFXModule attachFX;
        private OnBeginFXModule hitLevelFX;
        private OnBeginFXModule hitPlayerFX;
        private OnBeginFXModule hitHatFX;
        private OnBeginFXModule throwFX;
        private OnBeginFXModule hatDeathFX;

        private OnRepeatFXModule groundFX;
        private OnRepeatFXModule airFX;

        private OnEndFXModule endDangerousFX;

        public HatAudioCommand(int _e, string _name, GameObject _go)
            : base(_e, _name, _go) {

            attachFX = new OnBeginFXModule(PickUpFX);
            hitLevelFX = new OnBeginFXModule(HitLevelFX);
            hitPlayerFX = new OnBeginFXModule(HitPlayerFX);
            hitHatFX = new OnBeginFXModule(HitHatFX);
            throwFX = new OnBeginFXModule(ThrowFX);
            hatDeathFX = new OnBeginFXModule(HatDeathFX);

            groundFX = new OnRepeatFXModule(FrictionFX, 2);
            airFX = new OnRepeatFXModule(EmptyFunction);

            endDangerousFX = new OnEndFXModule(DangerOverFX);

            priority = (int)Priority.VERY_SLOW;

        }

        private void HitLevelFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.LEVEL_HIT_SOUND, go);
        }

        private void HitPlayerFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.PLAYER_HIT_SOUND, go);
            Debug.Log("Hit");

        }

        private void HitHatFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.HAT_HIT_SOUND, go);

        }

        private void EmptyFunction(LogicEntity obj) {

        }

        private void FrictionFX(LogicEntity obj) {
        }

        private void DangerOverFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.EXTINGUISH_SOUND, go);

        }



        private void HatDeathFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.DEATH_SOUND, go);

        }

        private void ThrowFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(name + HatSounds.THROW_SOUND, go);
        }

        private void PickUpFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.PICKUP_SOUND, go);
        }

        public override void Execute() {

            LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

            hatDeathFX.Update(e.isDead, e);

            hitLevelFX.Update(!e.collisionInfo.value.DoesntCollide(), e);
            hitHatFX.Update(e.collisionInfo.value.CollidesWith(Tag.HAT), e);
            hitPlayerFX.Update(e.collisionInfo.value.CollidesWith(Tag.PLAYER), e);

            if (e.isDead || e.hasFreeze) return;

            attachFX.Update(e.isAttached, e);
            throwFX.Update(e.isDangerous && !e.isAttached, e);

            groundFX.Update(e.isGrounded && Mathf.Abs(e.velocity.value.x) > 2, e);
            airFX.Update(e.isGrounded, e);

            endDangerousFX.Update(e.isDangerous, e);

        }

    }

}