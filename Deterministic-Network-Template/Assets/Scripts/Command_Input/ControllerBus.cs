using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

namespace CommandInput{
	
	public class ControllerBus : MonoBehaviour {

		public static List<Controller> connectedControllers = new List<Controller> ();
		private static int currentPlayers = 1;


		void Awake() {
			// Subscribe to events
			ReInput.ControllerConnectedEvent += OnControllerConnected;
			ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
			ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;

//			// Assign each Joystick to a Player initially
//			foreach(Joystick j in ReInput.controllers.Joysticks) {
//				if(ReInput.controllers.IsJoystickAssigned(j)) continue; // Joystick is already assigned
//
//				// Assign Joystick to first Player that doesn't have any assigned
//				//AssignJoystickToNextOpenPlayer(j);
//
//			}

			for (int playerID = 0; playerID < GameConstants.MAX_PLAYERS; playerID++) {
				connectedControllers.Add (new RewiredJoystick (playerID));
			}

		}

//		void Update () {
//
//			for (int i = 0, l = connectedControllers.Count; i < l; i++) {
//				//connectedControllers[i].ReadInput ();
//			}
//
//		}

		private void AddController (int playerId, int controllerId) {

//			connectedControllers.Add (
//				new RewiredJoystick (playerId, controllerId)
//			);

			currentPlayers++;

		}

		private void RemoveController (int controllerId) {

//			for (int i = connectedControllers.Count - 1; i >= 0; i--) {
//
//				if (connectedControllers[i].controllerId == controllerId) {
//					connectedControllers.RemoveAt (i);
//				}
//
//			}

			currentPlayers--;

		}

		// This function will be called when a controller is connected
		// You can get information about the controller that was connected via the args parameter
		void OnControllerConnected(ControllerStatusChangedEventArgs args) {

			AddController (currentPlayers, args.controllerId);
			Debug.Log("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);

			//AssignJoystickToNextOpenPlayer(ReInput.controllers.GetJoystick(args.controllerId));

		}

		// This function will be called when a controller is fully disconnected
		// You can get information about the controller that was disconnected via the args parameter
		void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {

			RemoveController (args.controllerId);
			Debug.Log("A controller was disconnected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);

		}

		// This function will be called when a controller is about to be disconnected
		// You can get information about the controller that is being disconnected via the args parameter
		// You can use this event to save the controller's maps before it's disconnected
		void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args) {
			//Debug.Log("A controller is being disconnected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		}

		void AssignJoystickToNextOpenPlayer(Joystick j) {
			
			foreach(Rewired.Player p in ReInput.players.Players) {
				if(p.controllers.joystickCount > 0 || p.id == 0) continue; // player already has a joystick
				p.controllers.AddController(j, true); // assign joystick to player
				return;

			}

		}

		void OnDestroy() {
			// Unsubscribe from events
			ReInput.ControllerConnectedEvent -= OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= OnControllerDisconnected;
			ReInput.ControllerPreDisconnectEvent -= OnControllerPreDisconnect;
		}

	}

}