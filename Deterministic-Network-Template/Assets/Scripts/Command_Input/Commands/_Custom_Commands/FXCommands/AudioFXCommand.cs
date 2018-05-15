using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public abstract class AudioFXCommand : FXCommand {

		protected string name;
        protected GameObject go;

		public AudioFXCommand (int _e, string _name, GameObject _go) 
			: base(_e) {

			this.name = _name;
            this.go = _go;
			priority = (int) Priority.VERY_SLOW;

		}

	}
		
}