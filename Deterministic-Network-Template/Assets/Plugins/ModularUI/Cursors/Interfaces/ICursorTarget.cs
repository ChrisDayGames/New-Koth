using ModularUI;
using System.Collections.Generic;

namespace ModularUI.Cursors {

	public interface ICursorTarget : IUICollider {

		void Click (ICursor cursor);
		void CollideWith (ICursor cursor);
		void EndCollisionWith (ICursor cursor);

//		void OnCursorEnter (ICursor cursor);
//		void OnCursorOver (ICursor cursor);
//		void OnCursorExit (ICursor cursor);

	}
		
}