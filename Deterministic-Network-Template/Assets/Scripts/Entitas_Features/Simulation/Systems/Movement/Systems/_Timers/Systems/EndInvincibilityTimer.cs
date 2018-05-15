using System.Collections.Generic;
using Entitas;

public class EndInvincibilityTimer : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _invincibilityTimers;
	public EndInvincibilityTimer (Contexts contexts) {

		_invincibilityTimers = contexts.logic.GetGroup (LogicMatcher.InvincibilityTimer);

	}

	public void Execute () {

		foreach (LogicEntity e in _invincibilityTimers.GetEntities ()) {

			e.ReplaceInvincibilityTimer (e.invincibilityTimer.timeLeft - GameController.DELTA_TIME);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _invincibilityTimers.GetEntities ()) {

			if (!e.isInvincible) {
				e.RemoveInvincibilityTimer ();

			} else if (e.invincibilityTimer.timeLeft <= 0) {

				e.RemoveInvincibilityTimer ();
				e.isInvincible = false;

			}

		}

	}

}