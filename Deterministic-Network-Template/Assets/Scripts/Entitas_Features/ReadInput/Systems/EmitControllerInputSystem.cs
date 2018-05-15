using Determinism;
using CommandInput;
using Entitas;
using UnityEngine;

public class EmitControllerInputSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem {

	readonly InputContext _context;
	private InputEntity[] _controllerInputs;

	public EmitControllerInputSystem (Contexts contexts) {

		_context = contexts.input;

	}

	public void Initialize () {

		_controllerInputs = new InputEntity[GameConstants.MAX_PLAYERS];

		for (int i = 0; i < _controllerInputs.Length; i++) {

			InputSnapshot s = new InputSnapshot ();
			s.Init ();

			_controllerInputs[i] = _context.CreateEntity ();
			_controllerInputs[i].AddControllerInput (s);
			_controllerInputs[i].AddControllerID (i);

		}

	}

	InputSnapshot s;
	public void Execute () {

		for (int i = 0, l = ControllerBus.connectedControllers.Count; i < l; i++) {

			ControllerBus.connectedControllers[i].ReadInput ();

			if (ControllerBus.connectedControllers[i].snapshot.HasRecordedInput()) {

				s = new InputSnapshot (ControllerBus.connectedControllers[i].snapshot);
				_controllerInputs[ControllerBus.connectedControllers[i].playerId].ReplaceControllerInput (s);

			}
				
		}
			
	}

	public void Cleanup () {

		foreach (InputEntity e in _controllerInputs) {

			if (e.isValidInput) {

				if (e.controllerInput.snapshot.HasRecordedInput ()) {

					e.controllerInput.snapshot.Reset ();
					e.ReplaceControllerInput(e.controllerInput.snapshot);

				}

				e.isValidInput = false;	

			}

		}

	}

}
