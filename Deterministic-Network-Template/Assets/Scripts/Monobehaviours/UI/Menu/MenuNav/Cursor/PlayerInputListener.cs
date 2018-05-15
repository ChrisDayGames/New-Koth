using UnityEngine.Events;
using UnityEngine;
using CommandInput;

[System.Serializable]
public class MovementEvent : UnityEvent<Vector2> { }

public class PlayerInputListener : MenuInputBehaviour {

    public MovementEvent CursorMoveEvent;
    public UnityEvent CursorAButtonEvent;
    public UnityEvent CursorBButtonEvent;
	public UnityEvent CursorXButtonEvent;
	public UnityEvent CursorYButtonEvent;
	public UnityEvent CursorRButtonEvent;
	public UnityEvent CursorLButtonEvent;
	public UnityEvent CursorStartButtonEvent;

    protected override void Move(float x, float y) {
        CursorMoveEvent.Invoke(new Vector2(x, y));
    }

    protected override void AButton(ButtonSnapshot aButton) {
        if(aButton.down)
            CursorAButtonEvent.Invoke();
    }

    protected override void BButton(ButtonSnapshot bButton) {
        if(bButton.down)
            CursorBButtonEvent.Invoke();
    }

	protected override void LButton(ButtonSnapshot lButton) {
		if(lButton.down)
			CursorLButtonEvent.Invoke();
	}

	protected override void RButton(ButtonSnapshot rButton) {
		if(rButton.down)
			CursorRButtonEvent.Invoke();
	}

	protected override void XButton(ButtonSnapshot xButton) {
		if(xButton.down)
			CursorXButtonEvent.Invoke();
	}

	protected override void YButton(ButtonSnapshot yButton) {
		if(yButton.down)
			CursorYButtonEvent.Invoke();
	}

	protected override void StartButton(ButtonSnapshot startButton) {
		if(startButton.down)
			CursorStartButtonEvent.Invoke();
	}

}
