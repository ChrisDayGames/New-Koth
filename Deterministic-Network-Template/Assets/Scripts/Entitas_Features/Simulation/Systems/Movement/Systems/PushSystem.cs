using System.Collections.Generic;
using Entitas;
using Determinism;

public class PushSystem : ReactiveSystem <LogicEntity> {

	public PushSystem (Contexts contexts) : base (contexts.logic) {}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Pusher);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasPusher && entity.hasVelocity;

	}

	protected override void Execute (List<LogicEntity> entities) {

		foreach (LogicEntity e in entities) {

			foreach (Passenger p  in e.pusher.passengers) {

				LogicEntity passenger = Contexts.sharedInstance.logic.GetEntityWithId(p.id);

				if (passenger.isMovable && !passenger.isStunned && (!passenger.hasWeight || !e.hasWeight || passenger.weight.value >= e.weight.value)) {

					FixedVector2 currentMovement = FixedVector2.ZERO;
					if (passenger.hasMove) currentMovement = passenger.move.target;

					if (p.h) {
						passenger.ReplaceMove (currentMovement + new FixedVector2 (e.velocity.value.x / 50, 0));
						passenger.ReplaceDirection (passenger.move.target.x.Sign ());
					}

					if (p.v)
						passenger.ReplaceMove (currentMovement + new FixedVector2 (0, e.velocity.value.y / 50));
					
				}

			}

			e.pusher.passengers.Clear ();

		}

	}

}