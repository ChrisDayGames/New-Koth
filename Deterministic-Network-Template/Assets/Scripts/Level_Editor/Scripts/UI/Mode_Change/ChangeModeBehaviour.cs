using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class ChangeModeBehaviour : ButtonFunctionBehaviour {

		public override void ClickFunction () {

			MapEditor.instance.ToggleEditMode ();

		}

	}

}	