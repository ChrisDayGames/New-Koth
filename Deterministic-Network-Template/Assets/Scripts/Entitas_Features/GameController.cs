using Determinism;
using Entitas;
using UnityEngine;
using System.Linq;
using System.Runtime;

public class GameController : MonoBehaviour{

	public readonly static int FPS = 50;
	public readonly static long DELTA_TIME = FixedMath.Create (1, FPS);

	private static int consecutiveFixedUpdates = 0;

	private Contexts _contexts;
	private Systems _readInputSystems;
	private Systems _logicSystems;

	public void Start () {

//		Time.timeScale = 0.93f;

		//Creating the context
		var _contexts = Contexts.sharedInstance;
		Services.singleton.Initialize (_contexts);

		foreach (var context in _contexts.allContexts) {
			if (context.contextInfo.componentTypes.Contains(typeof(IdComponent)))
				context.OnEntityCreated += AddId;
		}

		//caching simulation
		_contexts.time.ReplaceLogicSystem (new SimulationSystems (_contexts));

		//Creating Systems
		_readInputSystems = CreateReadInputSystems (_contexts);
		_logicSystems = CreateLogicSystems (_contexts);

		//initialize
		_readInputSystems.Initialize ();

		_logicSystems.Initialize ();
		_contexts.time.logicSystem.system.Initialize ();

		//_contexts.time.isPaused = true;

	}

	public void Update () {

		consecutiveFixedUpdates = 0;

        //Clean Up Input
        _readInputSystems.Cleanup();

        //Read Input
        _readInputSystems.Execute();


		if (Input.GetKeyDown (KeyCode.Backspace))
			DestroyAllGameEntities ();

	}
	

	public void FixedUpdate () {

		consecutiveFixedUpdates++;

		if (consecutiveFixedUpdates > 1) {
			
			foreach (InputEntity e in Contexts.sharedInstance.input.GetGroup (InputMatcher.ControllerInput)) {
				if (e.controllerInput.snapshot.HasRecordedInput ())
					e.ReplaceControllerInput (e.controllerInput.snapshot);
			}

		}
			
		//Run Logic
		_logicSystems.Execute ();
		_logicSystems.Cleanup ();

	}

	private static Systems CreateReadInputSystems (Contexts contexts) {

		return new Feature ("Read Input Systems")
			.Add (new InputSystems (contexts));

	}

	private static Systems CreateLogicSystems (Contexts contexts) {

		return new Feature ("Logic Systems")
			.Add (new TimeSystems (contexts))
			.Add (new MoveSimulationSystem (contexts))
			.Add (new InputHistorySystem (contexts))
			.Add (new TimeEventSystems (contexts))
			.Add (new MetaEventSystems (contexts))
			.Add (new LogicEventSystems (contexts));
		
	}

	// add an id to every entity as it's created
	private void AddId(IContext context, IEntity entity) {
		(entity as IIdEntity).AddId (entity.creationIndex);
	}

	public static void DestroyAllGameEntities () {

		foreach (LogicEntity e in Contexts.sharedInstance.logic.GetEntities ()) {
			e.isDestroyed = true;
		}

	}

}
