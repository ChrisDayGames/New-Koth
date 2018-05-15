using System.Collections.Generic;
using Entitas;
using Determinism;

public class TryMoveSystem : ReactiveSystem <LogicEntity> {

	public const long minDistance = FixedMath.Hundredth;

	public TryMoveSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Move);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasMove && entity.hasRayCastCollision && !entity.hasFreeze;

	}

	int left, right, up, down;
	FixedLine2 l = new FixedLine2 ();

	FixedVector2 horizontalHit;
	FixedVector2 verticallHit;

	RaycastCollider r;
	Collider c;

	protected override void Execute (List<LogicEntity> entities) {

		foreach (LogicEntity e in entities) {

			horizontalHit = FixedVector2.NAN;
			verticallHit = FixedVector2.NAN;

			r = e.rayCastCollision.value;
			c = e.collider.value;

			left = right = up = down = -1;

			int direction = e.direction.value;
			long rayLength = e.move.target.x.Abs () + r.skinWidth;

			if (e.move.target.x.Abs () < r.skinWidth)
				rayLength = r.skinWidth;
			
			for (int i = 0; i < r.horizontalRayCount; i++) {

				FixedVector2 origin = (direction == -1) ? c.broadBounds.bottomLeft : c.broadBounds.bottomRight;
				origin += FixedVector2.UP * (r.horizontalRaySpacing * i);

				FixedVector2 hit;
				FixedVector2 end = FixedVector2.RIGHT * (rayLength * direction);
				l = new FixedLine2 (origin, origin + end);

				int otherId = -1;
				if (RayCastSystem.Check (origin, end, out hit, e.id.value, c.check, out otherId)) {

					if (direction < 0)
						left = otherId;

					else
						right = otherId;

					long distance = (hit - origin).magnitude;
					if (distance > minDistance) 
						distance -= minDistance;
					else distance = 0;
					
					e.move.target.x = distance * direction;

					horizontalHit = hit;

				}

			}

			direction = e.move.target.y.Sign ();
			rayLength = e.move.target.y.Abs () + r.skinWidth;

			for (int i = 0; i < r.verticalRayCount; i++) {

				FixedVector2 origin = (direction == -1) ? c.broadBounds.bottomLeft : c.broadBounds.topLeft;
				origin.x += e.move.target.x;
				origin += FixedVector2.RIGHT * (r.verticalRaySpacing * i);

				FixedVector2 hit;
				FixedVector2 end = FixedVector2.UP * (rayLength * direction);
				l = new FixedLine2 (origin, origin + end);

				int otherId = -1;
				if (RayCastSystem.Check (origin, end, out hit, e.id.value, c.check, out otherId)) {

					if (direction < 0)
						down = otherId;

					else
						up = otherId;

					long distance = (hit - origin).magnitude;
					if (distance > minDistance) 
						distance -= minDistance;
					else distance = 0;

					e.move.target.y = distance * direction;

					verticallHit = hit;

				}

			}

			r = null;
			c = null;

			e.ReplaceLastPosition (e.position.value);
			e.ReplacePosition (e.position.value + e.move.target);

			e.collider.value.MoveCollider (e.position.value);
			e.RemoveMove ();

			RaycastCollisionInfo info = new RaycastCollisionInfo (left, right, up, down, horizontalHit, verticallHit);

			if (e.hasCollisionInfo)
				info.RecordLastPreviousCollision (
					e.collisionInfo.value.leftID,
					e.collisionInfo.value.rightID,
					e.collisionInfo.value.upID,
					e.collisionInfo.value.downID
				);
				
			e.ReplaceCollisionInfo (info);

		}

	}

}