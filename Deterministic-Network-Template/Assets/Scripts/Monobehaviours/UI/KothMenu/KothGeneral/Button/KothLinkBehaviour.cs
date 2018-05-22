using ModularUI;

public class KothLinkBehaviour : ClickBehaviour {

	public MenuState link;

	#region implemented abstract members of ClickBehaviour

	public override void Click () {
		
		Contexts.sharedInstance.meta.ReplaceMenuState (link);

	}

	#endregion




}
