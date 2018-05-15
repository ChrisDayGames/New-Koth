using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class ReleaseJumpCommand : Command {

		long minJump;
		long maxJump;

		public ReleaseJumpCommand (int _e, long _minJump, long _maxJump) 
			: base(_e) {

			this.minJump = _minJump;
			this.maxJump = _maxJump;
			priority = (int) Priority.FAST;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.velocity.value.y > minJump && !e.isGrounded)
				e.ReplaceVelocity(e.velocity.value.SetY (FixedMath.Max(e.velocity.value.y - (maxJump - minJump), minJump)));
			

		}

		public override void Undo () {

		}

	}

}