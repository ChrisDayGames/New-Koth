using System.Collections.Generic;
using Entitas;
using Determinism;

public class FollowPointSystem : ReactiveSystem <LogicEntity> {

	private readonly long PICKUP_INVINCIBILITY_TIME = FixedMath.Tenth;
	private const int OUTER_RADIUS_FACTOR = 2;


	readonly LogicContext _context;
	public FollowPointSystem (Contexts contexts) : base (contexts.logic) {

		_context = Contexts.sharedInstance.logic;

	}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Velocity);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasFollowPoint && entity.hasPosition && entity.hasVelocity;

	}

	FixedVector2 targetPosition, distance, targetVelocity;
	long angle, maxRotation;

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			LogicEntity owner = _context.GetEntityWithId (e.followPoint.targetID);
			targetPosition = owner.position.value + e.followPoint.offset;
			distance = targetPosition - e.position.value;
			maxRotation = e.followPoint.maxRotation;

			if (e.isAttached && distance.magnitude < e.followPoint.pickUpRadius) {

				if (e.isDangerous)
					e.isDangerous = false;
				
				targetVelocity = distance * e.followPoint.followSpeed / GameController.FPS;

				if (distance.x.Abs () <= targetVelocity.x) {
					targetVelocity.x = 0;
				}

				if (distance.y.Abs () <= targetVelocity.y) {
					targetVelocity.y = 0;
				}

				if (e.position.value.y <= targetPosition.y)
					targetVelocity.y = targetPosition.y - e.position.value.y;

//				if (e.hasCollisionInfo) {
//
//					if (e.collisionInfo.value.CollidesHorizontal ()) {
//
//						targetVelocity.y -= (e.collisionInfo.value.horizontalHit.y - e.position.value.y).Sign () * FixedMath.ONE;
//						UnityEngine.Debug.Log ("h: " + -(e.collisionInfo.value.horizontalHit.y - e.position.value.y).Sign () * FixedMath.ONE);
//
//					}
//
//					if (e.collisionInfo.value.CollidesVertical ()) {
//
//						targetVelocity.x -= (e.collisionInfo.value.verticalHit.x - e.position.value.x).Sign () * FixedMath.ONE;
//						UnityEngine.Debug.Log ("v: " + -(e.collisionInfo.value.verticalHit.x - e.position.value.x).Sign () * FixedMath.ONE);
//
//					}
//
//				}
					
				e.ReplaceLastVelocity (e.velocity.value);
				e.ReplaceVelocity (targetVelocity * 50);

				angle = FixedMath.Lerp01 (

					0,
					distance.x.Sign () * maxRotation,
					distance.x.Abs ()

				);

				e.ReplaceLastRotation (e.rotation.value);
				e.ReplaceRotation (angle);

			} 

			else if (!owner.isStunned && !e.hasThrowTimer && !e.isDangerous && distance.magnitude < e.followPoint.pickUpRadius * OUTER_RADIUS_FACTOR) {

				bool bIsPressed = false;


				if (owner.hasPlayerID) {

					foreach (InputEntity controller in Contexts.sharedInstance.input.GetEntitiesWithControllerID (owner.playerID.id)) {

						if (controller.controllerInput.snapshot.GetButton (CommandInput.Buttons.B).pressed)
							bIsPressed = true;

					}

				}

				if (!bIsPressed) {

					targetVelocity = distance * (FixedMath.ONE -distance.magnitude.Div(e.followPoint.pickUpRadius * OUTER_RADIUS_FACTOR)) * e.followPoint.followSpeed / (GameController.FPS * 2);

					e.isAttached = true;
					e.ReplaceLastVelocity (e.velocity.value);
					e.ReplaceVelocity (targetVelocity * 50);

				}
				
			} 

			else if (!owner.isStunned && !e.hasThrowTimer && !e.isAttached && distance.magnitude < e.followPoint.pickUpRadius) {
				
				bool bIsPressed = false;


				if (owner.hasPlayerID) {
					
					foreach (InputEntity controller in Contexts.sharedInstance.input.GetEntitiesWithControllerID (owner.playerID.id)) {

						if (controller.controllerInput.snapshot.GetButton (CommandInput.Buttons.B).pressed)
							bIsPressed = true;

					}

				}

				if (!bIsPressed) {

					e.isAttached = true;
					e.isInvincible = true;
					e.ReplaceInvincibilityTimer (PICKUP_INVINCIBILITY_TIME);

				}

			} else if (e.isAttached) {

				e.isAttached = false;

			}

		}

	}

}