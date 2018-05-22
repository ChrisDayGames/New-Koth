using ModularUI;
using ModularUI.Cursors;
using UnityEngine;

public class ReadyOnClick : ClickBehaviour {

	public KothCursorLink cursorLink;

	#region IClickable implementation

	public override void Click () {
		
		KothMenuManager.instance.playerSelections[cursorLink.referenceId].IsConfirmed = true;

	}

	#endregion

	void Reset () {

		cursorLink = GetComponentInParent <KothCursorLink> ();

	}

}
