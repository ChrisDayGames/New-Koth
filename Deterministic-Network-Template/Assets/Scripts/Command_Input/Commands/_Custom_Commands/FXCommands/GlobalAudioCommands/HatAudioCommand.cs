using System;
using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

    public static class HatSounds {

        public const string THROW_SOUND = "Throw";
        public const string HIT_SOUND = "Hit";
        public const string DEATH_SOUND = "Death";
        public const string PICKUP_SOUND = "Pickup";
        public const string EXTINGUISH_SOUND = "Extinguish";



    }

    public class HatAudioCommand : AudioFXCommand {

        private OnBeginFXModule attachFX;
        private OnBeginFXModule hitFX;
        private OnBeginFXModule throwFX;
        private OnBeginFXModule hatDeathFX;

        private OnRepeatFXModule groundFX;
        private OnRepeatFXModule airFX;

        private OnEndFXModule endDangerousFX;

        public HatAudioCommand(int _e, string _name, GameObject _go)
            : base(_e, _name, _go) {

            attachFX = new OnBeginFXModule(PickUpFX);
            hitFX = new OnBeginFXModule(HitFX);
            throwFX = new OnBeginFXModule(ThrowFX);
            hatDeathFX = new OnBeginFXModule(HatDeathFX);


            endDangerousFX = new OnEndFXModule(DangerOverFX);

            priority = (int)Priority.VERY_SLOW;

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

        private void HitFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.HIT_SOUND, go);

        }

        private void PickUpFX(LogicEntity obj) {
            AudioManager.singleton.PostWwiseEvent(HatSounds.PICKUP_SOUND, go);
        }

        public override void Execute() {

            LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

            hatDeathFX.Update(e.isDead, e);

            if (e.isDead || e.hasFreeze) return;

            attachFX.Update(e.isAttached, e);
            hitFX.Update(!e.collisionInfo.value.DoesntCollide(), e);
            throwFX.Update(e.isDangerous && !e.isAttached, e);


            endDangerousFX.Update(e.isDangerous, e);

        }

    }

}