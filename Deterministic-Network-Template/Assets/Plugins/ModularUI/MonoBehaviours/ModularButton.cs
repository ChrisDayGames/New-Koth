using ModularUI;
using UnityEngine;

public class ModularButton : UIBehaviour {

	private IClickable[] clickCommands;

	public void TestClick () {


		foreach (IClickable c in clickCommands)
			c.Click ();

	}

	// Use this for initialization
	void Start () {

		clickCommands = GetComponents <IClickable> ();

	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			TestClick ();
		}

	}

}
