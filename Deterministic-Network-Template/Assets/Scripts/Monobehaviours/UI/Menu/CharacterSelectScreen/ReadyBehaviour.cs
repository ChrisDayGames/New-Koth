using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyBehaviour : MenuInputBehaviour {

    private bool isOn = true;

	void Update() {
    
        if (PlayerBox.allPlayersReady && !isOn) {
            ToggleReadyScreen();
        }

        if (!PlayerBox.allPlayersReady && isOn) {
            ToggleReadyScreen();
        }
        
    }

	protected override void StartButton (CommandInput.ButtonSnapshot startButton) {

		if (startButton.down)
			StartGame ();
			

	}

    void ToggleReadyScreen() {
        isOn = !isOn;
        gameObject.ToggleChildren(isOn);
    }

	void StartGame () {

		foreach (PlayerBox pb in PlayerBox.playerBoxes) {

			//Generate the Playrer


		}

	}

}
