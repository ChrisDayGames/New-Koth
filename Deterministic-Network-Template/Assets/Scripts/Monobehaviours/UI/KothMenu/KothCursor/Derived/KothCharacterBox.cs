using ModularUI;
using UnityEngine.UI;

public class KothCharacterBox : KothCursorTarget, IGeneratable {

	public Characters character;
	public Image preview;

	public override void Init () {
		base.Init ();

		name = Assets.Get (character).name;
		preview.sprite = Assets.Get (character).uiInfo.smallSprite;

	}

	public override void OnCursorEnter (ModularUI.Cursors.ICursor cursor) {
		base.OnCursorEnter (cursor);

		KothMenuManager.instance.SelectCharacter (cursor.id, character); 

	}
		
	#region IGeneratable implementation

	public void Generate (int i)
	{
		character = (Characters) i;
		name = Assets.Get (character).name;
		preview.sprite = Assets.Get (character).uiInfo.smallSprite;
	}

	public int GetMaxObjects ()
	{
		return Enum<Characters>.Count;
	}

	public UnityEngine.MonoBehaviour GetScript ()
	{
		return this;
	}

	public UnityEngine.Transform GetTransform ()
	{
		return transform;
	}

	#endregion

}
