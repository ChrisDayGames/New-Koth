using Determinism;
using Entitas;
using UnityEngine;

namespace CommandInput {

	public class AnimationCommand : Command {

		private Animator anim;

		public AnimationCommand (int _e, Animator _anim) 
			: base(_e) {

			anim = _anim;
			
		}
			

		protected virtual void PlayAnimation (string animation) {

			if (!anim.GetCurrentAnimatorStateInfo(0).IsName(animation))
				anim.Play(animation);

		}

	}

}