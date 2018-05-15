using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace LevelEditor {

	[RequireComponent(typeof(Button))]
	public class UndoBehaviour : MonoBehaviour {

		public Image border;

		Button b;

		public void Undo () {

			MapEditor.instance.RequestUndo ();

		}

		public void OnDown () {

			border.color = Color.yellow;

		}

		public void OnUp () {

			border.color = Color.white;

		}

		#if UNITY_EDITOR
		void OnValidate() {

			if (Application.isPlaying) return;

			b = GetComponent<Button>();

			if (b != null) {

				UnityEventTools.RemovePersistentListener(b.onClick, Undo);
				UnityEventTools.AddPersistentListener(b.onClick, Undo);

			}

		}

		#endif

	}

}
