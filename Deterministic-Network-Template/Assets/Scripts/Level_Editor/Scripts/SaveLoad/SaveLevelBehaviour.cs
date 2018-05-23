using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class SaveLevelBehaviour : ButtonFunctionBehaviour {

		public override void ClickFunction () {

			MapEditor.instance.SaveLevel ();

		}

	}

}
