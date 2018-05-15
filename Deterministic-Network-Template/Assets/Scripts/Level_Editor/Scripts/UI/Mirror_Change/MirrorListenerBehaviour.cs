using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class MirrorListenerBehaviour : MonoBehaviour, IMirrorChangeListener {

		public bool isListeningX, isListeningY;

		public void Start () {
		
			MapEditor.instance.mirrorListeners.Add (this);

			Initialize ();

		}

		public virtual void Initialize () {
			
		}

		public virtual void OnMirrorChanged (bool mirrorX, bool mirrorY) {}

//		public void OnEnable () {
//
//			MapEditor.instance.toolListeners.Add (this);
//
//		}
//
//		public void OnDisable () {
//
//			MapEditor.instance.toolListeners.Remove (this);
//
//		}
//
//		public void OnDestroy () {
//
//			MapEditor.instance.toolListeners.Remove (this);
//
//		}

	}

}
