using Determinism;
using Entitas;
using CommandInput;
using UnityEngine;

public static class PlayerAnimations {

	public const string IDLE = "01_Idle";
	public const string RUN = "02_Run";
	public const string DASH = "03_DASH";
	public const string JUMP = "04_Jump";
	public const string FALL = "05_Fall";
	public const string WALL_SLIDE = "06_Wall_Slide";
	public const string WALL_JUMP = "07_Wall_Jump";
	public const string DEATH = "08_Death";
	public const string THEATRICAL_ENTRANCE = "09_Theatrical_Entrance";
	public const string THEATRICAL_VICTORY = "10_Theatrical_Victory";
	public const string HOVER = "11_Hover";
	public const string SUPER_JUMP = "12_Super_Jump";

}

namespace CommandInput {
		
	public class PlayerAnimationCommand : AnimationCommand {

		public PlayerAnimationCommand (int _e, Animator _anim) : base(_e, _anim) {}

		public override void Execute () {

			LogicEntity e = Contexts.sharedInstance.logic.GetEntityWithId(entityID);

			if (e.isStunned) {

				
			} 

			else if (e.isWallRiding) {

				PlayAnimation (PlayerAnimations.WALL_SLIDE);

			} 

			else if (e.isDashing) {

				PlayAnimation  (PlayerAnimations.RUN);

			}

			else if (e.isGrounded) {

				if (e.velocity.value.x.Abs () < FixedMath.Tenth)
					PlayAnimation (PlayerAnimations.IDLE);
				else
					PlayAnimation (PlayerAnimations.RUN);

			}

			else if (!e.isGrounded) {

				if (e.velocity.value.y > 0)
					PlayAnimation (PlayerAnimations.JUMP);
				else
					PlayAnimation (PlayerAnimations.FALL);

			}

		}

	}

}