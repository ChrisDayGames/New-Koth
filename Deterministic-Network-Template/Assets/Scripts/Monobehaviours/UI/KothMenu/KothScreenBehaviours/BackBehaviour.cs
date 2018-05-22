using UnityEngine;

public class BackBehaviour : KothScreenBehaviour {

	public MenuState backLink;

	protected override void BButton (CommandInput.ButtonSnapshot bButton) {
		base.BButton (bButton);

		if (bButton.down)
			Contexts.sharedInstance.meta.ReplaceMenuState (backLink);
		
	}

}
