using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

//public interface IDestroyEntity : IEntity, IDestroyed {}
//
//public partial class LogicEntity : IDestroyEntity { }
//public partial class InputEntity : IDestroyEntity { }

public class MultiDestroySystem : MultiReactiveSystem<LogicEntity, Contexts> {

	public MultiDestroySystem(Contexts contexts) : base(contexts) {}

	protected override ICollector[] GetTrigger (Contexts contexts)
	{
		return new ICollector[] {
			contexts.logic.CreateCollector(LogicMatcher.Destroyed),
			contexts.logic.CreateCollector(LogicMatcher.Reset),
			contexts.input.CreateCollector(InputMatcher.Destroyed),
		};
	}

	protected override bool Filter (LogicEntity entity)
	{
		return entity.isDestroyed;
	}

	protected override void Execute (List<LogicEntity> entities)
	{
		foreach (var e in entities) {

//			if (e.hasView) {
//
//				GameObject go = e.view.gameObject;
//
//				if (go.GetEntityLink ())
//					go.Unlink ();
//				
//				Object.Destroy (go);
//				e.RemoveView ();
//
//			}
//
			if (e.hasPlayerID) {

				var connectedControllers = Contexts.sharedInstance.input.GetEntitiesWithControllerID(e.playerID.id);

				foreach (InputEntity controller in connectedControllers) {
					controller.isAssignedToPlayer = false;
				}

			}

			e.Destroy ();

		}

	}

}