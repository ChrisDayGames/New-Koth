using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (KothLinkBehaviour))]
public abstract class ReadyGate : KothScreenBehaviour {

	public KothLinkBehaviour link;

	private bool isReady;
	protected bool IsReady {

		get {

			return isReady;

		}

		set {

			isReady = value;
		
			OnReady (isReady);

		}

	}

	public abstract void OnReady (bool isReay);

	void OnEnable () {

		foreach (KothPlayerSelection selection in KothMenuManager.instance.playerSelections)
			selection.IsConfirmed = false;

	}

	void Update () {

		int i = 0;
		foreach (KothPlayerSelection selection in KothMenuManager.instance.playerSelections)
			if (!selection.IsConfirmed && selection.IsPlaying) {

				IsReady = false;
				return;

			} else if (selection.IsPlaying)
				i++;

		IsReady = i > 0;

	}

	void Reset () {

		link = GetComponent <KothLinkBehaviour> ();

	}

}