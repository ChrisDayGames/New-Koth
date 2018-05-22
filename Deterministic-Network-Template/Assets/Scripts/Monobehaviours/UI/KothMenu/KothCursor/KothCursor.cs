using ModularUI.Cursors;
using UnityEngine.UI;
using CommandInput;
using Determinism;
using UnityEngine;

public class KothCursor : ModularUI.Cursors.Cursor, IControllerInputListener, IGeneratable {

	public int playerId;
	public float speed = 800f;

	public delegate void ButtonEvent();
	public ButtonEvent[] buttonEvents = new ButtonEvent[Enum<Buttons>.Count];

	//private reference to the time context
	protected InputContext _context;

	// Use this for initialization
	protected virtual void Start() {

		//initialize the menu, find the screen etc
		Init();

		//get the time context
		_context = Contexts.sharedInstance.input;

		//creating an entity to represent this
		InputEntity e = _context.CreateEntity();

		//Adding the appropriate listeners to the entity
		e.AddControllerInputListener(this);

		for (int i = 0; i < buttonEvents.Length; i++)
			buttonEvents[i] = delegate {};

		buttonEvents[(int) Buttons.A] += AButton;

		//Add to the Koth Menu Manager cursors array
		KothMenuManager.instance.cursors[playerId] = this;

	}

	public virtual void OnControllerInput (InputEntity e, InputSnapshot s) {

		if (!this.gameObject.activeInHierarchy) return;
		if (e.controllerID.id != playerId && playerId >= 0) return;

		//handle axis movement from the player
		Move(
			s.GetAxis(Axes.MoveHorizontal).ToFloat(),
			s.GetAxis(Axes.MoveVertical).ToFloat());


		int i = 0;
		foreach (ButtonSnapshot b in s.buttons) {

			if (b.down)
				buttonEvents[i].Invoke ();

			i++;

		}

	}

	protected void Move(float x, float y) {

		float factor = speed * Time.unscaledDeltaTime;
		rectTransform.Translate (new Vector3(x, y, 0).normalized * factor);

	}

	protected virtual void AButton() {
		
		if(currentTarget != null)
			currentTarget.Click (this);

	}

	#region IGeneratable implementation

	public void Generate(int i) {

		this.playerId = i;
		GetComponentInChildren<Text>().text = "P" + (i + 1);

	}

	public int GetMaxObjects() {
		return GameConstants.MAX_PLAYERS;
	}

	public MonoBehaviour GetScript() {
		return this;
	}

	public Transform GetTransform() {
		return transform;
	}

	#endregion


}
