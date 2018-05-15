using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class FastFallCommand : Command {

		bool value;

		public FastFallCommand (int _e, bool _value) 
			: base(_e) {
		
			value = _value;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);
			e.isFastFalling = value;

		}

		public override void Undo () {

		}

	}

}