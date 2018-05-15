using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput{

	public abstract class Command {

		protected enum Priority {

			VERY_SLOW = 4,

			SLOW = 3,

			MEDIUM = 2,

			FAST = 1,

			VERY_FAST = 0

		}

		public int priority;
		public int entityID;

		public Command (int _id) {
			entityID = _id;
		}

		public virtual void Execute () {}

		public virtual void Undo () {
		}

	}

}
