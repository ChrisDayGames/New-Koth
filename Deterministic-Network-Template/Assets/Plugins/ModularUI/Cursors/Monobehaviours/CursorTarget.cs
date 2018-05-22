using System.Collections.Generic;
using ModularUI;

namespace ModularUI.Cursors {

	public abstract class CursorTarget : UICollider, ICursorTarget {

		public override void Init () {
			base.Init ();

			collidingCursors = new HashSet <int> ();

		}

		#region ICursorTarget implementation

		public HashSet<int> collidingCursors { get; private set;}

		public virtual void CollideWith (ICursor cursor) {

			if (!collidingCursors.Contains (cursor.id)) {

				collidingCursors.Add (cursor.id);
				OnCursorEnter (cursor);

			} else {

				OnCursorOver (cursor);

			}
				

		}

		public virtual void EndCollisionWith (ICursor cursor) {

			collidingCursors.Remove (cursor.id);
			OnCursorExit (cursor);

		}

		public virtual void Click (ICursor cursor) {}

		public virtual void OnCursorEnter (ICursor cursor) {}

		public virtual void OnCursorOver (ICursor cursor) {}

		public virtual void OnCursorExit (ICursor cursor) {}

		#endregion

		void OnEnable () {

			Cursor.cursorTargets.Add (this);

		}

		void OnDisable () {

			Cursor.cursorTargets.Remove (this);

		}


	}

}