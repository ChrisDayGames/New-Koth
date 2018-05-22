using UnityEngine;

namespace ModularUI {

	/// <summary>
	/// Click command class, drag and drop onto a mono behaviour for modular functionality
	/// </summary>
	public abstract class ClickBehaviour : MonoBehaviour, IClickable {

		#region IClickable implementation

		/// <summary>
		/// Abstract click function
		/// </summary>
		public abstract void Click ();

		#endregion


	}

}