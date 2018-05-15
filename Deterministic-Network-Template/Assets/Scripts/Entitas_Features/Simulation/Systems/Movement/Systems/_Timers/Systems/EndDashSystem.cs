using System.Collections.Generic;
using Entitas;

public class EndDashSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _dashTimers;
	public EndDashSystem (Contexts contexts) {

		_dashTimers = contexts.logic.GetGroup (LogicMatcher.DashTimer);

	}

	public void Execute () {

		foreach (LogicEntity e in _dashTimers.GetEntities ()) {
			
			e.ReplaceDashTimer (e.dashTimer.timeLeft - GameController.DELTA_TIME);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _dashTimers.GetEntities ()) {

			if (!e.isDashing) {
				
				e.RemoveDashTimer ();

			} else if (e.dashTimer.timeLeft <= 0) {

				e.RemoveDashTimer ();
				e.isDashing = false;

			} 

		}

	}

}