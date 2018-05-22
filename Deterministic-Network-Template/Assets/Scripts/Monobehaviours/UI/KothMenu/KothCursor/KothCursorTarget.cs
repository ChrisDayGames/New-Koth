using ModularUI.Cursors;
using ModularUI;

public class KothCursorTarget : ModularUI.Cursors.CursorTarget {

	private IClickable[] clickCommands;
	private IHoverable[] hoverCommands;

	public override void Init () {

		base.Init ();

		clickCommands = GetComponents <IClickable> ();
		hoverCommands = GetComponentsInChildren <IHoverable> (true);

	}

	#region overriden members of CursorTarget

	public override void OnCursorEnter (ICursor cursor) {

		foreach (IHoverable h in hoverCommands)
			h.OnHoverBegin ();

	}

	public override void OnCursorOver (ICursor cursor) {

		foreach (IHoverable h in hoverCommands)
			h.OnHoverOver ();

	}

	public override void OnCursorExit (ICursor cursor) {

		foreach (IHoverable h in hoverCommands)
			h.OnHoverEnd ();

	}

	public override void Click (ICursor cursor) {
		base.Click (cursor);

		foreach (IClickable c in clickCommands)
			c.Click ();

	}

	#endregion


}
