using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public abstract class FXCommand : Command {

		//An event type for storing FX delegate
		internal delegate void PlayFXEvent (LogicEntity e);

		//Base class for FX Modules
		internal abstract class FXModule {

			protected bool lastCheck;
			public virtual void Update (bool condition, LogicEntity e) {}

		}

		//FX module for calling FX that will play on the first frame of a bool
		internal class OnBeginFXModule : FXModule {

			public PlayFXEvent OnBeginFX;
			public OnBeginFXModule (System.Action<LogicEntity> callback) {

				OnBeginFX = delegate(LogicEntity e) { callback (e); };

			}

			public override void Update (bool condition, LogicEntity e) {

				if (lastCheck != condition) {

					if (condition)
						OnBeginFX (e);

					lastCheck = condition;

				} 

			}

		}


		//FX class that will play FX that will play while a bool is set
		internal class OnRepeatFXModule : FXModule {

			public PlayFXEvent OnRepeatFX;
			public int waitFrames;
			public OnRepeatFXModule (System.Action<LogicEntity> callback, int _waitFrames = 1) {

				OnRepeatFX = delegate(LogicEntity e) { callback (e); };
				waitFrames = _waitFrames;

			}

			public override void Update (bool condition, LogicEntity e) {

				if (condition 
					&& lastCheck == condition 
					&& Contexts.sharedInstance.time.tick.value % waitFrames == 0)
					OnRepeatFX (e);

				lastCheck = condition;

			}

		}


		//FX class that will play FX the first frame the bool turns to false
		internal class OnEndFXModule : FXModule {

			public PlayFXEvent OnEndFX;
			public OnEndFXModule (System.Action<LogicEntity> callback) {

				OnEndFX = delegate(LogicEntity e) { callback (e); };

			}

			public override void Update (bool condition, LogicEntity e) {

				if (lastCheck != condition) {

					if (!condition)
						OnEndFX (e);

					lastCheck = condition;

				} 

			}

		}


		//FX class that combines all others together, useful for complicated FX
		internal class PlayFxModule {

			public bool lastCheck;
			private int repeatDuration;

			public PlayFxModule (System.Action<LogicEntity> startCallback, System.Action<LogicEntity> repeatCallback, int _repeatDuration, System.Action<LogicEntity> endCallback) {


				OnBeginFX = delegate(LogicEntity e) {  startCallback (e); };
				OnRepeatFX = delegate(LogicEntity e) { repeatCallback (e); };
				repeatDuration = _repeatDuration;
				OnEndFX = delegate(LogicEntity e) { endCallback (e); };

			}

			public void Update (bool condition, LogicEntity e) {

				if (lastCheck == condition) {

					if (condition && Contexts.sharedInstance.time.tick.value % repeatDuration == 0)
						OnRepeatFX (e);

				} else {

					lastCheck = condition;

					if (condition)
						OnBeginFX (e);
					else 
						OnEndFX (e);

				}

			}

			public PlayFXEvent OnBeginFX;
			public PlayFXEvent OnRepeatFX;
			public PlayFXEvent OnEndFX;

		}

        public FXCommand (int _e) 
			: base(_e) {

			priority = (int) Priority.VERY_SLOW;

		}

	}

}