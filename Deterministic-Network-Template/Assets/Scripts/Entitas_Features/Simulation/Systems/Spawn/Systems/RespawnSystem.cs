using System.Collections.Generic;
using Entitas;
using Determinism;
using UnityEngine;

public class RespawnSystem : ReactiveSystem <LogicEntity> {

	readonly LogicContext _context;

	public RespawnSystem (Contexts contexts) : base (contexts.logic) {

		_context = contexts.logic;
		_context.ReplaceSpawnPoints (new FixedVector2[0]);

	}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Respawn);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasPosition && entity.hasPlayerID;

	}


	FixedVector2 spawnPosition;
	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			if (_context.spawnPoints.list.Length == 0)
				spawnPosition = FixedVector2.ZERO;
			else
				spawnPosition = _context.spawnPoints.list[Random.Range (0, _context.spawnPoints.list.Length)];

			if (!e.isDead) {

				e.ReplacePosition (spawnPosition);
				e.ReplaceLastPosition (spawnPosition);

				if (e.hasHat) {

					LogicEntity hat = Contexts.sharedInstance.logic.GetEntityWithId(e.hat.entityID);

					hat.ReplacePosition (spawnPosition);
					hat.ReplaceLastPosition (spawnPosition);

				}

				e.isRespawn = false;

			}
			
		}

	}

}