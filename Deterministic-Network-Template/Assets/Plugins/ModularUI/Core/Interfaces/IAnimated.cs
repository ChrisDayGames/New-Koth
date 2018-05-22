using UnityEngine;

namespace ModularUI {

	public interface IAnimated {

		Animator anim { get; }
		string[] animations { get; }
		void PlayAnimation (string animation);

	}

}	