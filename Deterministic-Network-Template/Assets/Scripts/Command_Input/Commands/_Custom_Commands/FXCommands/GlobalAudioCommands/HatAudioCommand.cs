using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class HatAudioCommand : AudioFXCommand {

		public HatAudioCommand (int _e, string _name, GameObject _go) 
			: base(_e, _name, _go) {

			priority = (int) Priority.VERY_SLOW;

		}

		public override void Execute () {

			//get a reference to the entity
			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.isDangerous) {

				//Entity is Dangerous

			}

			if (e.isAttached) {

				//Entity is Attached

			}

			if (e.collisionInfo.value.CollidesWithHorizontal (Tag.PLAYER)) {

				//Collides with Player on X axis

			}

			if (e.collisionInfo.value.CollidesWithVertical (Tag.PLAYER)) {

				//Collides with Player on Y axis

			}

		}

	}

}