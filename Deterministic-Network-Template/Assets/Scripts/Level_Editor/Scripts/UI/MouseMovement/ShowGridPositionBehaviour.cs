using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class ShowGridPositionBehaviour : MouseMovementListener {

		public override void OnMouseMoved (Vector2 mouseTilePosition) {

			transform.position = new Vector3 (mouseTilePosition.x, mouseTilePosition.y, 0);

		}

	}


}