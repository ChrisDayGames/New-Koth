using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class SetVelocityCommand : Command {

		FixedVector2 newVelocity;

		public SetVelocityCommand (int _e, FixedVector2 _newVelocity) 
			: base(_e) {

			this.newVelocity = _newVelocity;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);
			e.ReplaceVelocity (newVelocity);

		}

		public override void Undo () {

		}

	}

}