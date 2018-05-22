using ModularUI.Cursors;
using ModularUI;
using CommandInput;

public class KothCursorLink : UIBehaviour, ICursorLink {

	#region ICursorLink implementation

	public int referenceId { get; set; }

	#endregion

	protected KothCursor cursor;

	void OnEnable () {

		Activate ();

	}

	void OnDisable () {

		DeActivate ();

	}

	public virtual void Activate () {

		cursor = KothMenuManager.instance.cursors[referenceId];

		cursor.buttonEvents[(int) Buttons.A] += OnAButtonDown;
		cursor.buttonEvents[(int) Buttons.B] += OnBButtonDown;

	}

	public virtual void DeActivate () {

		cursor.buttonEvents[(int) Buttons.A] -= OnAButtonDown;
		cursor.buttonEvents[(int) Buttons.B] -= OnBButtonDown;

	}

	public virtual void OnAButtonDown () {}
	public virtual void OnBButtonDown () {}
	public virtual void OnXButtonDown () {}
	public virtual void OnYButtonDown () {}
	public virtual void OnRButtonDown () {}
	public virtual void OnLButtonDown () {}
	public virtual void OnStartButtonDown () {}

}
