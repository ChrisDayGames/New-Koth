using System.Collections.Generic;
using Determinism;

namespace CommandInput {
	
	public struct InputSnapshot {

		public long[] axes;
		public ButtonSnapshot[] buttons;

		public InputSnapshot (InputSnapshot snapshot) {

			this.axes = snapshot.axes;
			this.buttons = snapshot.buttons;

		}

		public void Init (int numAxes, int numButtons) {

			axes =  new long[numAxes];
			buttons =  new ButtonSnapshot[numButtons];

		}

		public void Init () {

			Init (

				System.Enum.GetValues(typeof (Axes)).Length,
				System.Enum.GetValues(typeof (Buttons)).Length

			);

		}

		public long GetAxis (Axes i) {

			return axes [(int) i];

		}

		public void SetAxis (Axes i, float value) {

			axes [(int) i] =  FixedMath.Create (value);

		}

		public ButtonSnapshot GetButton (Buttons i) {

			return buttons [(int) i];

		}

		public void PressButton (Buttons i) {

			buttons [(int) i].Press ();

		}

		public void ReleaseButton (Buttons i) {
			
			buttons [(int) i].Release ();

		}

		public void ResetButton (Buttons i) {

			buttons [(int) i].Reset ();

		}

		public void Copy (InputSnapshot snapshot) {

			this.axes = new long[snapshot.axes.Length];
			this.buttons = new ButtonSnapshot[snapshot.buttons.Length];

			for (int i = 0; i < this.axes.Length; i++) {
				this.axes[i] = snapshot.axes[i];
			}

			for (int i = 0; i < this.buttons.Length; i++) {
				this.buttons[i].down = snapshot.buttons[i].down;
				this.buttons[i].pressed = snapshot.buttons[i].pressed;
				this.buttons[i].up = snapshot.buttons[i].up;
			}

		}

		public bool HasRecordedInput () {

			foreach (long axis in axes)
				if  (axis != 0) return true;

			foreach (ButtonSnapshot button in buttons)
				if (button.down || button.up) return true;

			return false;

		}

		public void ResetAxes () {

			for (int i = 0; i < axes.Length; i++) {
				axes [i] = 0;

			}

		}

		public void ResetButtons () {

			for (int i = 0; i < buttons.Length; i++) {
				buttons [i].Reset ();

			}

		}

		public void Reset () {
			
			for (int i = 0; i < axes.Length; i++) {
				axes [i] = 0;

			}

			for (int i = 0; i < buttons.Length; i++) {
				buttons [i].Reset ();

			}

		}


	}

	public struct ButtonSnapshot {
		
		public bool down;
		public bool pressed;
		public bool up;

		public ButtonSnapshot (ButtonSnapshot copy) {

			this.down = copy.down;
			this.pressed = copy.pressed;
			this.up = copy.up;

		}

		public void Press () {

			this.down = true;
			this.pressed =  true;

		}

		public void Release () {

			this.pressed =  false;
			this.up = true;

		}

		public void Reset () {
			
			this.down = false;
			this.up = false;

		}

	}
}
