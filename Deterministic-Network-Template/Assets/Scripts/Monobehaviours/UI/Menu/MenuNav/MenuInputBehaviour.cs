using CommandInput;
using Determinism;
using System.Collections;
using UnityEngine;

public abstract class MenuInputBehaviour : ManagedBehaviour, IControllerInputListener {

    //private reference to the time context
	protected InputContext _context;
    public int playerId;

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

    }

    protected virtual void Init() {}

	public virtual void Execute () {}

	public virtual void OnControllerInput (InputEntity e, InputSnapshot s) {

        if (!this.gameObject.activeInHierarchy) return;
		if (e.id.value != playerId) return;

        //handle axis movement from the player
        Move(
            s.GetAxis(Axes.MoveHorizontal).ToFloat(),
            s.GetAxis(Axes.MoveVertical).ToFloat());

        //handle a button
        AButton(s.GetButton(Buttons.A));

        //handle b button
        BButton(s.GetButton(Buttons.B));

		//handle a button
		RButton(s.GetButton(Buttons.R));

		//handle b button
		LButton(s.GetButton(Buttons.L));

		//handle a button
		XButton(s.GetButton(Buttons.X));

		//handle b button
		YButton(s.GetButton(Buttons.Y));

		//handle a button
		StartButton(s.GetButton(Buttons.START));

    }

    protected virtual void Move(float x, float y) {}

    protected virtual void AButton(ButtonSnapshot aButton) {}

    protected virtual void BButton(ButtonSnapshot bButton) {}

	protected virtual void LButton(ButtonSnapshot lButton) {}

	protected virtual void RButton(ButtonSnapshot rButton) {}

	protected virtual void XButton(ButtonSnapshot xButton) {}

	protected virtual void YButton(ButtonSnapshot yButton) {}

	protected virtual void StartButton(ButtonSnapshot startButton) {}
	
}
