using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionScreen : ReadyGate {

	#region implemented abstract members of ReadyGate
	public override void OnReady (bool isReay) {
		
		readyScreen.SetActive (IsReady);
	
	}
	#endregion

	public GameObject readyScreen;

	protected override void AButton (CommandInput.ButtonSnapshot aButton) {
		base.AButton (aButton);

		if (IsReady && aButton.down)
			link.Click ();
		
	}

	protected override void StartButton (CommandInput.ButtonSnapshot startButton) {
		base.StartButton (startButton);

		if (IsReady && startButton.down)
			link.Click ();

	}

}
