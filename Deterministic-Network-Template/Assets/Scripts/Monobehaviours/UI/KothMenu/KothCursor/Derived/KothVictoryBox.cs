using UnityEngine;

public class KothVictoryBox : KothDisplayBox, IGeneratable {

	public override void Activate () {
		base.Activate ();

		gameObject.ToggleChildren (selection.IsPlaying);
		ChooseCharacter (selection.Character);
		AssignBadge ();

	}

	private void AssignBadge() {}

	#region IGeneratable implementation

	public void Generate(int i) {
		referenceId = i;
	}

	public int GetMaxObjects() {
		return GameConstants.MAX_PLAYERS;
	}

	public MonoBehaviour GetScript() {
		return this;
	}

	public Transform GetTransform() {
		return transform;
	}

	#endregion


}
