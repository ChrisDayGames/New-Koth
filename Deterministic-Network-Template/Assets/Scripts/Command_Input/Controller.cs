using UnityEngine;
using Rewired;
using System;

namespace CommandInput {

	public enum Axes {

		MoveHorizontal,
		MoveVertical

	}

	public enum Buttons {

		A,
		B,
		X,
		Y,
		L,
		R,
		START

	}

	public abstract class Controller {

		public InputSnapshot snapshot;

		public int playerId, controllerId;
		public bool used;

		private string eventName;

		private Axes[] axes = (Axes[]) Enum.GetValues(typeof (Axes));
		private Buttons[] buttons = (Buttons[]) Enum.GetValues(typeof (Buttons));
		protected string[] axisNames;
		protected string[] buttonNames;

		public Controller (int _playerId) {

			playerId  =  _playerId;
			controllerId = playerId;
			eventName = "Controller " + playerId;

			snapshot.Init ();
			BuildNameTables ();

		}

		public Controller (int _playerId, int _controllerId) {

			playerId  =  _playerId;
			controllerId = _controllerId;
			eventName = "Controller " + playerId;

			snapshot.Init ();
			BuildNameTables ();

		}

		private void BuildNameTables () {

			axisNames = new string[axes.Length];

			for (int i = 0; i < axes.Length; i++)
				axisNames[i] = Enum.GetName (typeof (Axes), axes[i]);

			buttonNames = new string[buttons.Length];

			for (int i = 0; i < buttons.Length; i++)
				buttonNames[i] = Enum.GetName (typeof (Buttons), buttons[i]);

		}

		protected virtual void ReadAxis (Axes axis) {}
		protected virtual void ReadButton (Buttons button) {}

		public virtual void ReadInput () {
			
			foreach (Axes axis in axes) {

				ReadAxis (axis);

			}
				
			foreach (Buttons button in buttons) {

				ReadButton (button);

			}

		}
			
	}

	public class RewiredJoystick : Controller {

		private Player player;

		public RewiredJoystick (int _playerId) : base(_playerId) {

			player = ReInput.players.GetPlayer(playerId);

		}

		public RewiredJoystick (int _playerId, int _controllerId) : base(_playerId, _controllerId) {

			player = ReInput.players.GetPlayer(playerId);

		}

		protected override void ReadAxis (Axes axis) {

			snapshot.SetAxis (axis, player.GetAxisRaw (axisNames[(int) axis]));

		}

		protected override void ReadButton (Buttons button) {

			if (player.GetButtonDown (buttonNames[(int) button])) {

				snapshot.PressButton (button);
				used = true;

			}

			if (player.GetButtonUp (buttonNames[(int) button])) {

				snapshot.ReleaseButton (button);

			}

		}

	}

	public class Phone : Controller {

		public Phone (int _playerId, int _controllerId) : base(_playerId, _controllerId) {}

		public override void ReadInput () {

		}

	}

}
