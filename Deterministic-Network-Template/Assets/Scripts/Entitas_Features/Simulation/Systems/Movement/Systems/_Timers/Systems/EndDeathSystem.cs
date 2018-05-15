using System.Collections.Generic;
using Entitas;

public class EndDeathSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _deathTimers;
	public EndDeathSystem (Contexts contexts) {

		_deathTimers = contexts.logic.GetGroup (LogicMatcher.DeathTimer);

	}

	public void Execute () {

		foreach (LogicEntity e in _deathTimers.GetEntities ()) {

			e.ReplaceDeathTimer (e.deathTimer.timeLeft - GameController.DELTA_TIME);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _deathTimers.GetEntities ()) {

			if (!e.isDead) {
				e.RemoveDeathTimer ();

			} else if (e.deathTimer.timeLeft <= 0) {

				e.RemoveDeathTimer ();
				e.isDead = false;

			}

		}

	}

}