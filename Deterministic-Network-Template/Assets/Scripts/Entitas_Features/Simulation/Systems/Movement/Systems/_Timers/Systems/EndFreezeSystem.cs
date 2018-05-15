using System.Collections.Generic;
using Entitas;

public class EndFreezeSystem : IExecuteSystem, ICleanupSystem {

	readonly IGroup <LogicEntity> _freezeTimers;
	public EndFreezeSystem (Contexts contexts) {

		_freezeTimers = contexts.logic.GetGroup (LogicMatcher.Freeze);

	}

	public void Execute () {

		foreach (LogicEntity e in _freezeTimers.GetEntities ()) {

			e.ReplaceFreeze (e.freeze.frames - 1);

		}

	}

	public void Cleanup () {

		foreach (LogicEntity e in _freezeTimers.GetEntities ()) {

			if (e.freeze.frames <= 0)
				e.RemoveFreeze ();

		}

	}

}