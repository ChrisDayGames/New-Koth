using UnityEngine;

public class KothMenuManager : BaseBehaviour {

	public static KothMenuManager instance;

	[System.NonSerialized]
	public KothCursor[] cursors = new KothCursor[GameConstants.MAX_PLAYERS];
	[System.NonSerialized]
	public KothPlayerSelection[] playerSelections = new KothPlayerSelection[GameConstants.MAX_PLAYERS];

	void Awake () {

		if (instance == null)
			instance = this;
		else
			Destroy (this);

	}

	public void SelectCharacter (int playerIndex, Characters character) {

		if (!playerSelections[playerIndex].IsPlaying)
			playerSelections[playerIndex].IsPlaying = true;

		int skinIndex = 0;
		int colorIndex = 0;

		playerSelections[playerIndex].Character = character;
		playerSelections[playerIndex].SkinIndex = skinIndex;
		playerSelections[playerIndex].ColorIndex = colorIndex;

	}

	public void ChangeColor (int playerIndex, int direction) {

		print ("skin change: " + Mathf.Sign (direction));

	}

}
