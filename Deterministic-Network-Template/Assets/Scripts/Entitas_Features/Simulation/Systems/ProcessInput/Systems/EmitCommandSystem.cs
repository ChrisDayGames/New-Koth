using System.Collections.Generic;
using Entitas;
using UnityEngine;
using CommandInput;
using Determinism;

public class EmitCommandSystem : ReactiveSystem <InputEntity>, IInitializeSystem {

	readonly InputContext _context;


	public EmitCommandSystem (Contexts contexts) : base (contexts.input) {
		_context = contexts.input;
	}


	protected override ICollector<InputEntity> GetTrigger (IContext<InputEntity> context) {

		return context.CreateCollector (InputMatcher.ControllerInput);

	}

	protected override bool Filter (InputEntity entity){

		return entity.hasControllerID && entity.hasControllerInput;

	}

	public void Initialize () {

		_context.ReplaceCommandList (new List<Command> ());

	}

	protected override void Execute (List<InputEntity> entities) {

		int numCommands = _context.commandList.commands.Count;

		foreach (InputEntity e in entities) {

			foreach (LogicEntity player in Contexts.sharedInstance.logic.GetEntitiesWithPlayerID(e.controllerID.id)) {

				if (!player.isFastFalling && player.hasFastFallFactor && !player.isGrounded
					&& e.controllerInput.snapshot.GetAxis (Axes.MoveVertical) < -FixedMath.Create (4, 10)
					&& player.velocity.value.y < player.minJumpVelocity.value
					&& !e.controllerInput.snapshot.GetButton (Buttons.A).pressed) {

					_context.commandList.commands.Add (new FastFallCommand (

						player.id.value,
						true

					));

				} else if (player.isFastFalling && (player.isGrounded
					|| e.controllerInput.snapshot.GetAxis (Axes.MoveVertical) >= -FixedMath.Create (4, 10)
					|| player.velocity.value.y >= player.minJumpVelocity.value
					|| e.controllerInput.snapshot.GetButton (Buttons.A).pressed
				)) {

					_context.commandList.commands.Add (new FastFallCommand (

						player.id.value,
						false

					));

				}

				if (e.controllerInput.snapshot.GetButton(Buttons.A).down) {

					_context.commandList.commands.Add (new JumpCommand (
						player.id.value,
						player.maxJumpVelocity.value,
						e.controllerInput.snapshot.GetAxis (Axes.MoveHorizontal)

					));

				}

				if (e.controllerInput.snapshot.GetButton(Buttons.A).up) {

					_context.commandList.commands.Add (new ReleaseJumpCommand (
						player.id.value,
						player.minJumpVelocity.value,
						player.maxJumpVelocity.value

					));

				}

				if (e.controllerInput.snapshot.GetButton(Buttons.B).down) {

					_context.commandList.commands.Add (new ThrowCommand (
						player.id.value,
						new FixedVector2 (
							e.controllerInput.snapshot.GetAxis (Axes.MoveHorizontal),
							e.controllerInput.snapshot.GetAxis (Axes.MoveVertical)
						)

					));

				}

				if (e.controllerInput.snapshot.GetButton(Buttons.B).up) {

//					_context.commandList.commands.Add (new ChangeColorCommand (
//						player.id.value,
//						Color.white
//
//					));

				}

				_context.commandList.commands.Add (new RunCommand (
					player.id.value,
					new FixedVector2 (
						e.controllerInput.snapshot.GetAxis (Axes.MoveHorizontal),
						e.controllerInput.snapshot.GetAxis (Axes.MoveVertical)
					)
				));
					

				if (_context.commandList.commands.Count > numCommands)
					e.isValidInput = true;
				

			}
				
		}

	}

}