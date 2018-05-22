namespace ModularUI.Cursors {

	public interface ICursor : IUICollider {

		int id { get; }
		ICursorTarget currentTarget { get; }

		void Click ();
		void CheckCollisions ();

	}
		
}