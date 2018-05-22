using UnityEngine;

namespace ModularUI {

	[RequireComponent(typeof(RectTransform))]
	public abstract class UIBehaviour : BaseBehaviour {

		[HideInInspector]
		public RectTransform rectTransform;

		void Reset () {

			GetReferences ();

		}

		public virtual void GetReferences () {

			GetRect ();

		}

		private void GetRect () {

			if (this.rectTransform == null)
				this.rectTransform = GetComponent <RectTransform> ();


		}

	}

}