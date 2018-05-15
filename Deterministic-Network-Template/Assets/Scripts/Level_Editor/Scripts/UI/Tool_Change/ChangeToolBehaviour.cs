using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace LevelEditor {

	[RequireComponent(typeof(Button))]
	public class ChangeToolBehaviour : ToolListenerBehaviour {

		public Tool tool;
		public Sprite icon;
		public Image iconImage;
		public Image border;

		Button b;

		public override void Initialize () {
		}

		public void ChangeTool () {

			MapEditor.instance.ChangeTool (tool);

		}

		public override void OnToolChanged (Tool _tool) {

			if (this.tool == _tool) {

				border.color = Color.yellow;

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

				UnityEventTools.RemovePersistentListener(b.onClick, ChangeTool);
				UnityEventTools.AddPersistentListener(b.onClick, ChangeTool);

			}

		}

		#endif

	}

}
