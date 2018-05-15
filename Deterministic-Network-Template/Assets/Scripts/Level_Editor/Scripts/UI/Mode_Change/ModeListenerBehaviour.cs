using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class ModeListenerBehaviour : MonoBehaviour, IModeChangeListener {

		public void Start () {
		
			MapEditor.instance.modeListeners.Add (this);

			Initialize ();

		}

		public virtual void Initialize () {
			
		}

		public virtual void OnModeChanged (bool isEditMode) {}

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
