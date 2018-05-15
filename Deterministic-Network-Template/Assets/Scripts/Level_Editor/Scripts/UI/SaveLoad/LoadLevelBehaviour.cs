using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class LoadLevelBehaviour : ButtonFunctionBehaviour {

		public override void ClickFunction () {

			MapEditor.instance.LoadLevel ();

		}

	}

}
