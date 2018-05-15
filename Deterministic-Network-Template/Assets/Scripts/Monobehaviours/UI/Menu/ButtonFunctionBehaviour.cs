using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(Button))]
public class ButtonFunctionBehaviour : MonoBehaviour {

	private Button b;

	public virtual void ClickFunction () {}

	#if UNITY_EDITOR
	public void OnValidate () {

		b = GetComponent<Button>();

		if (b != null) {

			UnityEventTools.RemovePersistentListener(b.onClick, ClickFunction);
			UnityEventTools.AddPersistentListener(b.onClick, ClickFunction);

		}

	}
		
	public void OnDestroy () {

		b = GetComponent<Button>();

		if (b != null)
			UnityEventTools.RemovePersistentListener(b.onClick, ClickFunction);
		b = null;

	}
	#endif

}
