using Determinism;
using Entitas;
using CommandInput;

public class OnTriggerEnterCommand : Command{

	public OnTriggerEnterCommand (int _e) 
		: base(_e) {
	}

	public void OnCollisionEnter (int otherEntityID)  {

		LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);
		LogicEntity other = Contexts.sharedInstance.logic.GetEntityWithId(otherEntityID);

		if (other.collider.value.tag == Tag.HAT 
			&& e.collider.value.mask == other.collider.value.mask
			&& !other.isAttached
			&& !other.hasThrowTimer
			&& !e.isStunned
			&& !e.hasFreeze) {

			bool bIsPressed = false;

			if (e.hasPlayerID) {

				foreach (InputEntity controller in Contexts.sharedInstance.input.GetEntitiesWithControllerID (e.playerID.id)) {

					if (controller.controllerInput.snapshot.GetButton (CommandInput.Buttons.B).pressed)
						bIsPressed = true;

				}

			}

			if (!bIsPressed) {

				other.isAttached = true;
				other.isDangerous = false;
				other.isFalling = false;

			}

		}
		
	}

}
