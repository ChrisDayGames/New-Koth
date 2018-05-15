using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class ThrowCommand : AxisCommand2D {

		public ThrowCommand (int _e, FixedVector2 _axes) 
			: base(_e, _axes) {

			priority = (int) Priority.VERY_FAST;

		}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);
			LogicEntity hat = Contexts.sharedInstance.logic.GetEntityWithId(e.hat.entityID);
			if (hat == null) return;

			if (hat.isAttached) {

				hat.isAttached = false;
				hat.isDangerous = true;
				hat.isFalling = true;

				FixedVector2 throwdirection = axes.normalized;
				throwdirection = throwdirection.ContrainTo16Angles ();
				throwdirection = throwdirection.normalized;
				
				if (axes == FixedVector2.ZERO) {

					if (e.isWallRiding)
						throwdirection = FixedVector2.LEFT * e.direction.value;
					else 
						throwdirection = FixedVector2.RIGHT * e.direction.value;

				}

				long extraPower = 0;
				if(e.velocity.value.y < 0 && axes.y < 0) {
					extraPower = e.velocity.value.y / 200;
				}

				hat.ReplaceVelocity (new FixedVector2 (

					(hat.throwMovement.power + extraPower).Mul(throwdirection.x),
					(hat.throwMovement.power + extraPower).Mul(throwdirection.y)

				));

				hat.throwMovement.throwPositionY = e.position.value.y;
				hat.ReplaceThrowTimer (FixedMath.Create (3, 10));

				hat.ReplaceLastRotation (0);
				hat.ReplaceRotation (0);

			}

		}

		public override void Undo () {

		}

	}

}