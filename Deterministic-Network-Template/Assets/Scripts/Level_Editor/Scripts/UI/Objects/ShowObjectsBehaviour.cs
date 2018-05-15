using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor {

	public class ShowObjectsBehaviour : ButtonFunctionBehaviour {

		public Sprite onIcon;
		public Sprite offIcon;
		public Image iconImage;
		public Image border;

		bool isShowingObjects = false;

		public override void ClickFunction () {

			isShowingObjects = !isShowingObjects;
			MapEditor.instance.ToggleObjects (isShowingObjects);

			if (isShowingObjects) TurnOn ();
			else TurnOff ();

		}

		public void TurnOn () {

			iconImage.sprite = onIcon;
			border.color = Color.red;
			
		}

		public void TurnOff () {

			iconImage.sprite = offIcon;
			border.color = Color.white;

		}

	}

}
