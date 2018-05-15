using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class LinkBehaviour : ButtonFunctionBehaviour {

	public MenuState nextScreen;

	public override void ClickFunction () {
		Contexts.sharedInstance.meta.ReplaceMenuState (nextScreen);
	}

}
