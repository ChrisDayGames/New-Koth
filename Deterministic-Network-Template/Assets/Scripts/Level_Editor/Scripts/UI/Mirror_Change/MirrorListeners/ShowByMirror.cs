using UnityEngine;


namespace LevelEditor {

	public class ShowByMirror : MirrorListenerBehaviour {


		public override void Initialize () {

			gameObject.SetActive (false);

		}

		public override void OnMirrorChanged (bool mirrorX, bool mirrorY) {

			if (isListeningX) {

				gameObject.SetActive (mirrorX);

			} else {

				//gameObject.SetActive (false);

			}

			if (isListeningY) {

				gameObject.SetActive (mirrorY);

			} else {

				//gameObject.SetActive (false);

			}

		}

	}

}
