using Determinism;
using Entitas;
using UnityEngine;
using TypeReferences;

[System.Serializable]
public class CharacterUIBlueprint {

    public bool isUnlocked = true;
    public bool isHidden = false;
    
    public Sprite smallSprite;
    public Sprite bigSprite;
    public Sprite logo;

	public HatAndBodySkins[] skins = new HatAndBodySkins[0];
}

[CreateAssetMenu]
public class CharacterBlueprint : ScriptableObject {

	[Header ( "UI Data" )]
	public CharacterUIBlueprint uiInfo;

	[Header ( "Prefabs" )]
	public GameObject player;
	public GameObject hat;

	[Header ( "Collision" )]
	public Mask mask;
	public Tag tag;
	[EnumFlagAttribute]
	public Mask checkAgainst;
	public static long scale = FixedMath.Create (3, 2);

	[Header( "Ground Movement" )]
	public long groundSpeed = FixedMath.Create (15);
	public long groundAcceleration = FixedMath.Create (12, 1000);
	public long weightFactor = FixedMath.Create (1);
	
	[Header( "Air Movement" )]
	public long airSpeed = FixedMath.Create (15);
	public long airAcceleration = FixedMath.Create (1, 10);

	[Header( "Move Bonuses" )]
	public long hatlessMoveBonus = 0;
	public long hatlessJumpBonus = 0;
	public long hoverMoveBonusX = 0;
	public long hoverMoveBonusY = 0;
	
	[Header( "Dash Movement" )]
	public long dashSpeed = FixedMath.Create (30);
	public long dashAcceleration = FixedMath.Create (1, 100);
	public long dashLength = FixedMath.Create (7, 100);

	[Header( "Jump Parameters" )]
	public long timeToJumpApex = FixedMath.Create (48, 100);
	public long minJump = FixedMath.Create (15, 10);
	public long maxJump = FixedMath.Create (9);
	public long bounceHeight = FixedMath.Create (6);
	public int jumpsAllowed = 2;

	[Header( "Fall Parameters" )]
	public int fastFallFactor = 10;
	public long terminalVelocity = FixedMath.Create (30);
	public long terminalVelocityFastFall = FixedMath.Create (50);

	[Header( "Wall Parameters" )]
	public FixedVector2 innerJump = new FixedVector2 ((int) 20, (int) 30);
	public FixedVector2 neutralJump = new FixedVector2 ((int) 30, (int) 25);
	public FixedVector2 outerJump = new FixedVector2 ((int) 30, (int) 25);
	public long maxSlideSpeed = FixedMath.Create (8);
	public int fastSlideFactor = 3;
	public FixedVector2 reflectionDampPlayer = new FixedVector2 (FixedMath.Create (7, 10), FixedMath.Create (7, 10));


	[Header( "Hat Physics" )]
	public long gravity = FixedMath.Create (15, 100);
	public long normalFriction = FixedMath.Create (9, 10);
	public long dangerousFriction = FixedMath.Create (9, 10);
	public long normalDrag = FixedMath.Create (2, 10);
	public long dangerousDrag = FixedMath.Create (2, 10);
	public FixedVector2 reflectionDampHat = new FixedVector2 (FixedMath.Create (7, 10), FixedMath.Create (7, 10));

	[Header( "Hat Power" )]
	public long throwPower = FixedMath.Create (8);
	public long blowBack = FixedMath.Create (50);
	public long stunTime = FixedMath.Create (33, 100);

	[Header( "Hat Attached Parameters" )]
	public long followSpeed = FixedMath.Create (20);
	public long maxRotation = FixedMath.Create (80);


	[HideInInspector]
	public FixedVector2 playerColOffsetPosition, playerColScale, followPoint;

	[HideInInspector]
	public long pickUpRadius;

	public void GetPlayerCollider () {

		LowLevelPlayerView playerView = player.GetComponent <LowLevelPlayerView> ();
		if (playerView != null) {

			Debug.Log (player.name + " collider updated.");

			//collider data
			playerColOffsetPosition = playerView.offset;
			playerColScale = playerView.size;

			//hat pickup data
			followPoint = playerView.followPoint;
			pickUpRadius = playerView.pickUpRadius;

		} else {

			Debug.LogError (hat.name + " could not be found.");

		}
			
	}
		
	[HideInInspector]
	public FixedVector2 hatColOffsetPosition, hatColScale;

	public void GetHatCollider () {

		LowLevelHatView hatView = hat.GetComponent <LowLevelHatView> ();

		if (hatView != null) {

			Debug.Log (hat.name + " follow data updated.");

			hatColOffsetPosition = hatView.offset;
			hatColScale = hatView.size;

		} else {

			Debug.LogError (hat.name + " could not be found.");

		}

	}

	public void OnValidate () {

		if (Application.isPlaying) return;

		GetPlayerCollider ();
		GetHatCollider ();

	}

//	public void Reset () {
//
//		GetPlayerCollider ();
//		GetHatCollider ();
//
//	}

}
