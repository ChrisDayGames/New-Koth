using System.Collections.Generic;
using Determinism;
using Entitas;
using UnityEngine;
using Persistence;

public class CreatePlayerSystem : ReactiveSystem <InputEntity>, IInitializeSystem {

	public CreatePlayerSystem (Contexts contexts) : base (contexts.input) {}

	protected override ICollector<InputEntity> GetTrigger (IContext<InputEntity> context) {
		
		return context.CreateCollector (InputMatcher.ControllerInput);

	}

	protected override bool Filter (InputEntity entity) {
		
		return entity.hasControllerID && entity.hasControllerInput && !entity.isAssignedToPlayer;

	}

	protected override void Execute (List <InputEntity> entities) {

		foreach (InputEntity e in entities) {

			if (!e.controllerInput.snapshot.HasRecordedInput ()) continue;

			if (!(Contexts.sharedInstance.logic.GetEntitiesWithPlayerID(e.controllerID.id).Count > 0)) {

				CreatePlayer (e.controllerID.id, Assets.Get (Characters.BIRTHDAY));

                e.isAssignedToPlayer = true;
				e.isValidInput = true;

			}
				
		}

	}

	public void Initialize () {

		//Debug.Log ( System.IO.Directory.GetFiles(Application.dataPath + LevelData.DIRECTORY)[0]);

		CreateLevel (SaveLoad.LoadAll <LevelData> (SaveLoad.EDITOR_ROOT + LevelData.DIRECTORY, LevelData.EXTENSION, false)[0]);

	}

	private void CreateRandomPlayer (int id) {

		CreatePlayer (id, Assets.Get (Enum<Characters>.RandomValue()));

	}

	private void CreatePlayerByName (int id, string name) {

		CreatePlayer (id, Resources.Load <CharacterBlueprint> ("Characters/" + name));

	}
		
	private void CreatePlayer (int id, CharacterBlueprint c) {

		LogicEntity e = Contexts.sharedInstance.logic.CreateEntity ();

		//Add the logic
		e.AddPlayerID (id);

		e.AddPosition (
			new FixedVector2 (0, 0)
		);

		e.AddRotation (0);
		e.AddScale (
			new FixedVector2 (
				CharacterBlueprint.scale, 
				CharacterBlueprint.scale)
		);

		e.AddVelocity (
			new FixedVector2 (0, 0)
		);

		e.AddAcceleration (
			new FixedVector2 (0, 0)
		);

		e.isMovable = true;
		e.AddLastPosition (e.position.value);
		e.AddLastVelocity (e.velocity.value);

		//Movement
		e.AddCurrentMovementX(0, 0, 0);
		e.AddGroundMovement (c.groundSpeed, c.groundAcceleration);
		e.AddAirMovement (c.airSpeed, c.airAcceleration);
		e.AddDashMovement (c.dashSpeed, c.dashAcceleration, c.dashLength);
		e.AddStunMovement (0, FixedMath.Create (2));
		e.AddWallRideMovement (
			c.maxSlideSpeed, 
			c.fastSlideFactor,
			c.innerJump,
			c.neutralJump,
			c.outerJump,
			//acceleration
			FixedMath.Create (2, 10),
			//walljump length
			FixedMath.Create (25, 100),
			//stick time
			FixedMath.Create (5, 100)

		);

		e.AddDirection (1);

		//Jump
		e.AddTimeToApex (c.timeToJumpApex);
		e.AddMinJump (c.minJump); // 1.5
		e.AddMaxJump (c.maxJump);

		e.AddJumpsAllowed (c.jumpsAllowed);
		e.AddJumpsCompleted (0);
		e.AddFastFallFactor (c.fastFallFactor);
		e.AddTerminalVelocity (c.terminalVelocity);
		e.AddFastFallTerminalVelocity (c.terminalVelocityFastFall);
		e.AddBounceHeight (c.bounceHeight);
		e.AddStunTime (FixedMath.Hundredth * 2);

		e.AddReflectionDampening (c.reflectionDampPlayer.x, c.reflectionDampPlayer.y);
		
		e.AddCollider (new Determinism.BoxCollider (e.position.value, c.playerColOffsetPosition * CharacterBlueprint.scale, c.playerColScale * CharacterBlueprint.scale));
		e.AddOnRayCastCollision (new CommandInput.PlayerRayCastCollisionCommand(e.id.value));
		e.AddOnTriggerEnter (new OnTriggerEnterCommand (e.id.value));

		e.collider.value.tag = Tag.PLAYER;
		e.collider.value.mask = (Mask) (1 << (id + 1));

		e.collider.value.check = 
			e.collider.value.check.AddFlags (Mask.DEFAULT, Mask.P1, Mask.P2, Mask.P3, Mask.P4, Mask.P5, Mask.P6, Mask.P7, Mask.P8);

		e.collider.value.check = 
			e.collider.value.check.RemoveFlag (e.collider.value.mask);

		e.AddPusher (new List<Passenger> ());
		e.isHitable = true;

		e.AddWeight (c.weightFactor);

		e.AddHat(CreateHat (id, e.id.value, c));
		e.isRespawn = true;


		var view = Object.Instantiate(c.player).GetComponent<IView>();
		view.Link(e, Contexts.sharedInstance.logic);

	}


	private int CreateHat (int id, int ownerID, CharacterBlueprint c) {

		LogicEntity e = Contexts.sharedInstance.logic.CreateEntity ();

		e.AddPosition (
			new FixedVector2 (0, 0)
		);

		e.AddRotation (0);
		e.AddLastRotation (0);
		e.AddScale (new FixedVector2 (
			CharacterBlueprint.scale, 
			CharacterBlueprint.scale)
		);

		e.AddVelocity (
			new FixedVector2 (0, 0)
		);

		e.AddAcceleration (
			new FixedVector2 (0, 0)
		);

		e.isMovable = true;
		e.AddLastPosition (e.position.value);
		e.AddLastVelocity (e.velocity.value);

		//Movement
		e.AddDirection (1);

		//Follow
		e.isAttached = true;
		e.AddFollowPoint (ownerID, c.followSpeed, c.followPoint, c.pickUpRadius, c.maxRotation);

		//gravity is 50 * greater than in game
		e.AddGravity (c.gravity * 50);

		e.AddFriction (c.normalFriction, c.dangerousFriction);
		e.AddDrag (c.normalDrag, c.dangerousDrag);


		//* 50 / 10 from original amt
		e.AddThrowMovement (c.throwPower * 5, 0);

		//Throw
		e.AddKnockBack (c.blowBack);
		e.AddStunTime (c.stunTime);

		e.AddReflectionDampening (c.reflectionDampHat.x, c.reflectionDampHat.y);

		//collision
		e.AddCollider (new Determinism.BoxCollider (e.position.value, c.hatColOffsetPosition * CharacterBlueprint.scale, c.hatColScale * CharacterBlueprint.scale));
		e.AddOnRayCastCollision (new CommandInput.HatRayCastCollisionCommand(e.id.value));

		e.collider.value.tag = Tag.HAT;
		e.collider.value.mask = (Mask) (1 << (id + 1));

		e.collider.value.isTrigger = true;

		e.collider.value.check =  
			e.collider.value.check.AddFlags (Mask.DEFAULT, Mask.P1, Mask.P2, Mask.P3, Mask.P4, Mask.P5, Mask.P6, Mask.P7, Mask.P8);

		e.collider.value.check = 
			e.collider.value.check.RemoveFlag (e.collider.value.mask);

		e.isHitable = true;
		e.isPusheable = true;
		e.AddWeight (c.weightFactor);

		var view = Object.Instantiate(c.hat).GetComponent<IView>();
		view.Link(e, Contexts.sharedInstance.logic);

		return e.id.value;

	}
		
	private void CreateBlock (FixedVector2 position, FixedVector2 scale) {

//		LogicEntity block = Contexts.sharedInstance.logic.CreateEntity ();
//		block.AddPosition (position);
//		block.AddScale (scale);
//		block.AddCollider (new Determinism.BoxCollider (block.position.value, block.scale.value));
//		block.AddSprite ("Sprites/Crate");
//
//		block.AddLastPosition (FixedVector2.ZERO);
//		block.AddLastVelocity (FixedVector2.ZERO);
//		block.AddVelocity (FixedVector2.ZERO);
//		block.AddAcceleration (FixedVector2.ZERO);
//		block.AddDirection (1);
//
//		block.isMovable = true;
//		block.isPusheable = true;
//		block.isFalling = true;
//		block.isHitable = true;
//		block.AddGravity (FixedMath.Create (30));
//
//		block.AddCurrentMovementX (0, 0, 0);
//		block.AddGroundMovement (0, 0);
//		block.AddStunMovement (0, FixedMath.Create (33, 100));
//
//		block.AddOnRayCastCollision (new CommandInput.RayCastCollisionCommand(block.id.value));
//
//		block.collider.value.mask = Mask.DEFAULT;
//		block.collider.value.tag = Tag.DEFAULT;
//
//		block.collider.value.check = 
//			block.collider.value.check.AddFlags (Mask.DEFAULT, Mask.P1, Mask.P2, Mask.P3, Mask.P4, Mask.P5, Mask.P6, Mask.P7, Mask.P8);

	}

	private void CreateWall (FixedVector2 position, FixedVector2 scale) {

//		LogicEntity wall = Contexts.sharedInstance.logic.CreateEntity ();
//		wall.AddPosition (position);
//		wall.AddScale (scale);
//		wall.AddCollider (new Determinism.BoxCollider (wall.position.value, wall.scale.value));
//		wall.AddSprite ("Sprites/1");
//
//		wall.collider.value.mask = Mask.DEFAULT;
//		wall.collider.value.tag = Tag.DEFAULT;

	}


	//Hatlandian Hills
	private void CreateLevel (LevelData saveData) {

		Contexts.sharedInstance.logic.ReplaceLevel (saveData);

	}

}