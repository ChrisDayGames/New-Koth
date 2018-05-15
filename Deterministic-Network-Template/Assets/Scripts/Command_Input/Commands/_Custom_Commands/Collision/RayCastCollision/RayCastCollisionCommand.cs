using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class RayCastCollisionCommand : Command {

		public RayCastCollisionCommand (int _e) 
			: base(_e) {

			priority = (int) Priority.MEDIUM;

		}

		public virtual void OnCollisionEnter (RaycastCollisionInfo info) {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.isStunned && (info.left != Tag.NONE || info.right != Tag.NONE) && !info.CollidesWithHorizontal (Tag.HAT)) {

				e.ReplaceVelocity (new FixedVector2 (

					-e.velocity.value.x.Mul (FixedMath.Create (7, 10)),
					e.velocity.value.y + (e.velocity.value.y.Sign () * (e.velocity.value.x.Abs () / 2)))

				);

				return;

			}

			else if ((info.up != Tag.NONE || info.down != Tag.NONE)  && !info.CollidesWithVertical (Tag.HAT))
				e.ReplaceVelocity (e.velocity.value.SetY (0));

		}

	}

}