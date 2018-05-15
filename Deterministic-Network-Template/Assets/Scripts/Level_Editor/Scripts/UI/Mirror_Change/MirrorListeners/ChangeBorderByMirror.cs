using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LevelEditor {

	public class ChangeBorderByMirror : MirrorListenerBehaviour {

		public Sprite icon;
		public Image iconImage;
		public Image border;

		private bool currentMirrorX = false;
		private bool currentMirrorY = false;

		public override void OnMirrorChanged (bool mirrorX, bool mirrorY) {

			currentMirrorX = mirrorX;
			currentMirrorY = mirrorY;

			if ((isListeningX == mirrorX || isListeningY == mirrorY ) && (mirrorX || mirrorY)) {

				border.color = Color.blue;

			} else {

				border.color = Color.white;

			}

		}
			
	}
		
}