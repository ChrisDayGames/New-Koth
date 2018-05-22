using ModularUI.Cursors;
using UnityEngine;
using UnityEngine.UI;

public class KothPlayerBox : KothDisplayBox, ISelectionListener {

	private const float PREVIEW_ALPHA = 0.5f;
	private const float SELECTION_ALPHA = 1f;

	private float alpha = 0.5f;

	#region ISelectionListener implementation
	public void OnCharacter (Characters character) {

		ChooseCharacter (character);

	}

	public void OnColor (int skinIndex, int colorIndex) {
		
		Debug.Log ("Color Change");

	}

	public void OnConfirm (bool isConfirmed) {

		if (isConfirmed)
			ToggleAlpha (SELECTION_ALPHA);
		else
			ToggleAlpha (PREVIEW_ALPHA);

	}

	public void OnJoin (bool isJoined) {

		ToggleVisibility (isJoined); 

	}

	#endregion

	public void ChangeAlpha (Graphic g, float newAlpha) {

		Color c = g.color;
		c.a = newAlpha;

		g.color = c;

	}

	public void ToggleAlpha (float value) {

		ChangeAlpha (name, value);
		ChangeAlpha (preview, value);

	}

	public void ToggleVisibility (bool isActive) {

		Color c = Color.black;

		if (isActive)
			c = Color.white;

		GetComponent <Image> ().color = c;

		gameObject.ToggleChildren(isActive);
		ToggleAlpha (PREVIEW_ALPHA);

	}

	public void TryChangeSelection () {

		KothMenuManager.instance.SelectCharacter (referenceId, (cursor.currentTarget as KothCharacterBox).character);

	}

	public void TryForceSelection () {

		TryChangeSelection ();
		selection.IsConfirmed = true;

	}

	#region KothCursorLink implementation

	public override void OnAButtonDown () {
		base.OnAButtonDown ();
			
		if (!selection.IsPlaying) {

			selection.IsPlaying = true;

			if (cursor.currentTarget is KothCharacterBox)
				TryForceSelection ();
			
		} else if (!selection.IsConfirmed)
			selection.IsConfirmed = true;
		
	}

	public override void OnBButtonDown () {
		base.OnBButtonDown ();

		if (selection.IsConfirmed) {

			selection.IsConfirmed = false;

			if (cursor.currentTarget is KothCharacterBox)
				TryChangeSelection ();
			
		} else if (selection.IsPlaying)
			selection.IsPlaying = false;
		else if (!selection.IsPlaying)
			Contexts.sharedInstance.meta.ReplaceMenuState (MenuState.INTRO);

	}

	public override void Activate () {
		base.Activate ();

		selection.AddListener (this);

	}

	public override void DeActivate () {
		base.DeActivate ();

		selection.RemoveListener (this);

	}
	
	#endregion

}
