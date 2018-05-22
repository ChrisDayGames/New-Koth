using System.Collections.Generic;

namespace ModularUI.Cursors {

	public class Cursor : UICollider, ICursor {
		
		public static List<ICursorTarget> cursorTargets = new List<ICursorTarget> ();

		#region ICursor implementation

		public int id { get; private set;}

		public ICursorTarget currentTarget { get; private set;}

		#endregion

		protected override void OnUpdate () {
			base.OnUpdate ();

			CheckCollisions ();

			if (UnityEngine.Input.GetMouseButtonDown (0))
				Click ();

		}
			
		public void CheckCollisions () {

			//find the current collision for this cursor
			foreach(ICursorTarget target in cursorTargets) {

				//AABB collision using UIColliders
				if (this.IsCollidingWith (target.uiCollider)) {

					if (currentTarget != target && currentTarget != null)
						currentTarget.EndCollisionWith (this);

					currentTarget = target;
					currentTarget.CollideWith (this);

					//return once a collision is found
					return;

				}

			}

			if (currentTarget != null) {
				currentTarget.EndCollisionWith (this);
				currentTarget = null;

			}

		}

		public void Click () {
			
			if (currentTarget != null)
				currentTarget.Click (this);

		}

			
	}
		
}