using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {

	public class MouseMovementListener : MonoBehaviour, IMouseMoveListener {

		public void Start () {

			MapEditor.instance.mouseMoveListeners.Add (this);

			Initialize ();

		}

		public virtual void Initialize () {

		}

		public virtual void OnMouseMoved (Vector2 mouseTilePosition) {}

	}

}