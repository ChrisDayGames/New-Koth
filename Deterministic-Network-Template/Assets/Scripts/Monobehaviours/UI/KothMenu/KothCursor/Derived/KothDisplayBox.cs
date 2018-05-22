using ModularUI.Cursors;
using UnityEngine;
using UnityEngine.UI;

public class KothDisplayBox : KothCursorLink, IGeneratable {

	public Text name;
	public Image preview;

	protected KothPlayerSelection selection;

	public override void Activate () {
		base.Activate ();

		selection = KothMenuManager.instance.playerSelections[referenceId];

	}

	protected void ChooseCharacter (Characters character) {

		name.text = Assets.Get (character).name;
		preview.sprite = Assets.Get (character).uiInfo.bigSprite;

	}

	#region IGeneratable implementation

	//IGeneratable Interface
	public void Generate (int i) {
		referenceId = i;
	}

	public int GetMaxObjects () {
		return GameConstants.MAX_PLAYERS;
	}

	public MonoBehaviour GetScript () {
		return this;
	}

	public Transform GetTransform () {
		return transform;
	}

	#endregion

}
