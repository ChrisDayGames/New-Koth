using System.Collections.Generic;
using Entitas;
using Determinism;

public class CreateLevelSystem : ReactiveSystem <LogicEntity> {

	readonly LogicContext _context;

	public CreateLevelSystem (Contexts contexts) : base (contexts.logic) {

		_context = contexts.logic;

	}

	protected override ICollector<LogicEntity> GetTrigger (IContext<LogicEntity> context) {

		return context.CreateCollector (LogicMatcher.Level);

	}

	protected override bool Filter (LogicEntity entity){

		return entity.hasLevel;

	}

	protected override void Execute (List<LogicEntity> entities) {
		
		foreach (LogicEntity e in entities) {

			TileGrid map = new TileGrid (e.level.data);

			LogicEntity level = Contexts.sharedInstance.logic.CreateEntity ();
			level.AddCollider (new Determinism.RectilinearCollider (LevelBuilder.GenerateDeterministicCollider (map)));
			level.collider.value.mask = Mask.DEFAULT;
			level.collider.value.tag = Tag.DEFAULT;

			Contexts.sharedInstance.logic.ReplaceSpawnPoints(e.level.data.spawnPositions);

		}

	}

}