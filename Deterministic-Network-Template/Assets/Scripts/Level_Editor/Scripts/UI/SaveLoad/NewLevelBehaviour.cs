using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class NewLevelBehaviour : ButtonFunctionBehaviour {

		public override void ClickFunction () {

			MapEditor.instance.CreateNewLevel ();

		}

	}

}
