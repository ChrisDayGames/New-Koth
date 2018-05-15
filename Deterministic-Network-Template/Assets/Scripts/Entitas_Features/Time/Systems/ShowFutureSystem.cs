using System.Collections.Generic;
using Entitas;
using UnityEngine;
using Determinism;

public class ShowFutureSystem : ReactiveSystem <TimeEntity> {

	public const int FUTURE_FRAMES = 25;

	LogicContext _LogicContext;
	TimeContext _timeContext;
	InputContext _inputContext;

	readonly IGroup <LogicEntity> _movers;
	readonly IGroup <InputEntity> _inputSources;

	private List <LineRenderer> futureMovements = new List<LineRenderer> ();

	public ShowFutureSystem (Contexts contexts) : base (contexts.time) {

		_LogicContext = contexts.logic;
		_timeContext = contexts.time;
		_inputContext = contexts.input;

		_movers = _LogicContext.GetGroup (LogicMatcher.Movable);
		_inputSources = _inputContext.GetGroup (InputMatcher.ControllerInput);

	}

	protected override ICollector<TimeEntity> GetTrigger (IContext<TimeEntity> context) {

		return context.CreateCollector (TimeMatcher.Tick);

	}

	protected override bool Filter (TimeEntity entity){

		return entity.hasTick;

	}

	protected override void Execute (List<TimeEntity> entities) {
		return;
		if (_movers.count == 0) return;

		while (futureMovements.Count > _movers.count) {

			GameObject go = futureMovements[0].gameObject;
			futureMovements.RemoveAt (0);
			GameObject.Destroy (go);

		}

		while (futureMovements.Count < _movers.count) {
			LineRenderer lr = new GameObject ("future move").AddComponent <LineRenderer> ();
			lr.positionCount = FUTURE_FRAMES;
			lr.startWidth = 0.1f;
			lr.endWidth = 0.1f;
			futureMovements.Add (lr);
		}

//		_timeContext.logicSystem.system.Initialize ();

		LogicContext gc = new LogicContext ();
		foreach (LogicEntity e in _LogicContext.GetEntities ()) {
			gc.CloneEntity (e, true, e.GetComponentIndices ());
		}

		for (int i = 0; i < FUTURE_FRAMES; i++) {

			_timeContext.ReplaceTick (_timeContext.tick.value++);

			foreach (InputEntity e in _inputSources) {
				e.ReplaceControllerInput(e.controllerInput.snapshot);
			}


			_timeContext.logicSystem.system.Execute ();
			_timeContext.logicSystem.system.Cleanup ();

			int j = 0;
			foreach (LogicEntity mover in _movers) {
				futureMovements[j].SetPosition (i, new Vector3 (

					mover.position.value.x.ToFloat (),
					mover.position.value.y.ToFloat (),
					0

				)); 
				j++;
			}

		}

		foreach (LogicEntity e in _LogicContext.GetEntities ()) {
			e.isDestroyed = true;
		}

		_timeContext.logicSystem.system.Execute ();
		_timeContext.logicSystem.system.Cleanup ();
			
		foreach (LogicEntity ge in gc.GetEntities ()) {
			_LogicContext.CloneEntity (ge, true, ge.GetComponentIndices ());
		}

		foreach (InputEntity e in _inputSources) {
			e.ReplaceControllerInput(e.controllerInput.snapshot);
		}
			
	}

//	public int[] GetNonIdComponents (LogicEntity e) {
//
//		int[] indices = e.GetComponentIndices ();
//		int[] newIndices = new int [e.GetComponentIndices ().Length - 1];
//		var name = "";
//
//		int j = 0;
//		foreach (var index in indices) {
//			
//			name = LogicComponentsLookup.componentNames [index];
//
//			if (name != "Id") {
//
//				newIndices[j] = index;
//				j++;
//
//			}
//
//		}
//
//		return newIndices;
//
//	}

//	public LogicEntity CopyEntityToContext  (LogicEntity e) {
//
//		foreach (var index in e.GetComponentIndices ()) {
//
//			var component = e.GetComponent (index);
//			var name = LogicComponentsLookup.componentNames [index];
//
//			if (name != "Id") {
//				int gIndex = Array.IndexOf(GameComponentsLookup.componentNames, tName);
//				var gComponent = g.CreateComponent(gIndex, tComponent.GetType());
//				gComponent = (IComponent) tComponent.PublicMemberClone();
//
//				g.ReplaceComponent(gIndex, gComponent);
//
//			}
//
//		}
//
//
//	}

//
//	public LogicEntity CreateTemplateEntity (int tag) {        
//		var t = _templateContext.GetEntityWithTemplateTag(tag);        
//		var g = _LogicContext.CreateEntity();
//
//		foreach(var tIndex in t.GetComponentIndices()) {
//			var tComponent = t.GetComponent(tIndex);
//			var tName = TemplateComponentsLookup.componentNames[tIndex];
//
//			if (tName != "Id" && tName != "TemplateTag") {
//				if (GameComponentsLookup.componentNames.Contains(tName)) {
//					int gIndex = Array.IndexOf(GameComponentsLookup.componentNames, tName);
//					var gComponent = g.CreateComponent(gIndex, tComponent.GetType());
//					gComponent = (IComponent) tComponent.PublicMemberClone();
//
//					g.ReplaceComponent(gIndex, gComponent);
//				}   
//			}
//		}
//
//		return g;
//	}

}