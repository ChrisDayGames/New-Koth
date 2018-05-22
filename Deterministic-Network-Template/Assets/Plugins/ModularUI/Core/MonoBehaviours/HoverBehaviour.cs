using UnityEngine;

namespace ModularUI {

	public abstract class HoverBehaviour : MonoBehaviour, IHoverable {

		#region IHoverable implementation

		public abstract void OnHoverBegin ();
		public abstract void OnHoverOver ();
		public abstract void OnHoverEnd ();

		#endregion


	}

}