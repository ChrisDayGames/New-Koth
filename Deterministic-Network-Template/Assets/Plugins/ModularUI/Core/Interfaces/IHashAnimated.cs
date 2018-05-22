using UnityEngine;

namespace ModularUI {

	public interface IHashAnimated {

		Animator anim { get; }
		int[] animationHashes { get; }
		void PlayAnimation (int animationHash);

	}

}