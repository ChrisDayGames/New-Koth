using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class ToolListenerBehaviour : MonoBehaviour, IToolChangeListener {

		public void Start () {
		
			MapEditor.instance.toolListeners.Add (this);

			Initialize ();

		}

		public virtual void Initialize () {
			
		}

		public virtual void OnToolChanged (Tool t) {}

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
