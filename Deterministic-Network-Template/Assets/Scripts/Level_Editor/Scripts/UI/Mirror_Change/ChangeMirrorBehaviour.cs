using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace LevelEditor {

	[RequireComponent(typeof(Button))]
	public class ChangeMirrorBehaviour : MirrorListenerBehaviour {

		public Sprite icon;
		public Image iconImage;
		public Image border;

		Button b;

		private bool currentMirrorX = false;
		private bool currentMirrorY = false;

		public override void Initialize () {
		}

		public void ChangeMirror () {

			if (isListeningX)
				currentMirrorX = !currentMirrorX;

			if (isListeningY)
				currentMirrorY = !currentMirrorY;

			MapEditor.instance.ChangeMirroState (currentMirrorX, currentMirrorY);

		}

		public override void OnMirrorChanged (bool mirrorX, bool mirrorY) {

			currentMirrorX = mirrorX;
			currentMirrorY = mirrorY;

			if ((isListeningX == mirrorX || isListeningY == mirrorY ) && (mirrorX || mirrorY)) {

				border.color = Color.blue;

			} else {

				border.color = Color.white;

			}

		}



		#if UNITY_EDITOR
		void OnValidate() {

			if (Application.isPlaying) return;

			iconImage.sprite = icon;

			b = GetComponent<Button>();

			if (b != null) {

				UnityEventTools.RemovePersistentListener(b.onClick, ChangeMirror);
				UnityEventTools.AddPersistentListener(b.onClick, ChangeMirror);

			}

		}

		#endif

	}

}
