using UnityEngine;

public class GlobalVFX : MonoBehaviour {

    private static GlobalVFX instance;

    [Header("Player Run")]
    public ParticleTemplate runDustTemplate;
    [Space(5)]

    [Header("Player Dash")]
    public ParticleTemplate dashDustTemplate;
    public ParticleTemplate dashDustSpikeTemplate;
    [Space(5)]

    [Header("Player Jump")]
    public ParticleTemplate jumpDustTemplate;
    public ParticleTemplate doubleJumpDustTemplate;
    [Space(5)]

    [Header("Player Land")]
    public ParticleTemplate landDustTemplate;
    public ParticleTemplate landDustSpikeTemplate;
    [Space(5)]

    [Header("Player Land Fastfall")]
    public ParticleTemplate landFastFallDustTemplate;
    public ParticleTemplate landFastFallDustSpikeTemplate;
    [Space(5)]

    [Header("Player Wall Slide")]
    public ParticleTemplate wallSlideDustTemplate;
    public ParticleTemplate wallSlideFastDustTemplate;
    [Space(5)]

    [Header("Player Wall Jump")]
    public ParticleTemplate wallJumpDustTemplate;
    [Space(5)]

    [Header("Player Stunned")]
    public ParticleTemplate stunnedDustTemplate;
    [Space(5)]

    [Header("Hat Ground Friction")]
    public ParticleTemplate groundFrictionTemplate;
    [Space(5)]

    [Header("Hat Hit Wall")]
    public ParticleTemplate hatHitWallTemplate;
    [Space(5)]

    [Header("Hat Hit Player")]
    public ParticleTemplate hatHitPlayerTemplate;
    [Space(5)]

    [Header("Dangerous Hat Hit Player - Slash")]
    public ParticleTemplate slashTemplate;
    [Space(5)]

    [Header("Dangerous Hat Hit Player - Blowback")]
    public ParticleTemplate blowBackFrontTemplate;
    public ParticleTemplate blowBackBackTemplate;
    [Space(5)]

    [Header("Dangerous Hat Hit Player - Screen Crack")]
    public ParticleTemplate screenCrackTemplate;
    [Space(5)]

    [Header("Hat Hit Hat")]
    public ParticleTemplate hatHitHatTemplate;
    [Space(5)]

    [Header("Hat Throw FX")]
    public ParticleTemplate throwFXTemplate;
    [Space(5)]

    [Header("Hat Danger Finish")]
    public ParticleTemplate dangerFinishTemplate;
    [Space(5)]

    [Header("Hat Pick Up")]
    public ParticleTemplate pickUpTemplate;
    [Space(5)]

    [Header("Hat Death - Ray Center")]
    public ParticleTemplate rayCenterTemplate;
    [Space(5)]

    [Header("Hat Death - Rays")]
    public ParticleTemplate raysTemplate;
    [Space(5)]

    [Header("Hat Death - Fireworks")]
    public ParticleTemplate fireworksTemplate;
    [Space(5)]

    [Header("Hat Death - Sparkles")]
    public ParticleTemplate sparklesTemplate;
    [Space(5)]

    [Header("Player Death - Spirit Explosion")]
    public ParticleTemplate spiritExplosionTemplate;
    [Space(5)]

    [Header("Player Death - Insignia")]
    public ParticleTemplate deathInsigniaTemplate;
    //[Space(5)]

    [Header("Player Death - Sludge")]
    public ParticleTemplate sludgeTemplate;
    //[Space(5)]

    void Awake() {

        if (instance == null) 
            instance = this;  
              
    }

    //PLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYER
    //PLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYER
    //PLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYERPLAYER

    public static void PlayRunParticles (Vector3 position, int dir, Vector3 colliderExtents) {

        instance.runDustTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero
                                                       + instance.runDustTemplate.emitParams.rotation3DOffset;
        instance.runDustTemplate.emitParams.position = position
                                                     + Vector3.down * colliderExtents.y
                                                     + Vector3.right * colliderExtents.x * -dir * 1.2f
                                                     + instance.runDustTemplate.emitParams.positionOffset;

        VFXManager.instance.EmitAtPosition(instance.runDustTemplate);
	}

	public static void PlayDashParticles (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //round dust
        instance.dashDustTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero;
        instance.dashDustTemplate.emitParams.position = position
                                                        + Vector3.down * colliderExtents.y
                                                        + Vector3.right * colliderExtents.x *  -dir
                                                        + instance.dashDustTemplate.emitParams.positionOffset;

        VFXManager.instance.EmitAtPosition(instance.dashDustTemplate);

        //sharp dust
        instance.dashDustSpikeTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero;
        instance.dashDustSpikeTemplate.emitParams.position = position
                                                        + Vector3.down * colliderExtents.y
                                                        + Vector3.right * 1.5f * colliderExtents.x * -dir
                                                        + instance.dashDustSpikeTemplate.emitParams.positionOffset;

        VFXManager.instance.EmitAtPosition(instance.dashDustSpikeTemplate);


    }

    public static void PlayJumpParticles (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity, int jumpsCompleted) {

        if (jumpsCompleted > 0) {

            instance.jumpDustTemplate.emitParams.rotation3D = ((dir > 0) ? Vector3.zero : Vector3.up * 180)
                                                            + Vector3.forward * velocity.x * dir;
            instance.jumpDustTemplate.emitParams.position = position
                                                          + Vector3.down * colliderExtents.y
                                                          + instance.doubleJumpDustTemplate.emitParams.positionOffset;

            VFXManager.instance.EmitAtPosition(instance.jumpDustTemplate);

        } else {

            instance.jumpDustTemplate.emitParams.rotation3D = ((dir > 0) ? Vector3.zero : Vector3.up * 180)
                                                            + Vector3.forward * velocity.x * dir;
            instance.jumpDustTemplate.emitParams.position = position
                                                          + Vector3.down * colliderExtents.y
                                                          + instance.jumpDustTemplate.emitParams.positionOffset;

            VFXManager.instance.EmitAtPosition(instance.jumpDustTemplate);
            
        }

    }

	public static void PlayLandDust (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //round dust
        instance.landDustTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero;
        instance.landDustTemplate.emitParams.position = position
                                                        + Vector3.down * colliderExtents.y
                                                        + Vector3.left * dir * colliderExtents.x * 1.5f
                                                        + instance.landDustTemplate.emitParams.positionOffset;


        VFXManager.instance.EmitAtPosition(instance.landDustTemplate);


        //sharp dust
        instance.landDustSpikeTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero;
        instance.landDustSpikeTemplate.emitParams.position = position
                                                        + Vector3.down * colliderExtents.y
                                                        + Vector3.left * dir * colliderExtents.x * 1.5f
                                                        + instance.landDustSpikeTemplate.emitParams.positionOffset;


        VFXManager.instance.EmitAtPosition(instance.landDustSpikeTemplate);

    }

    public static void PlayLandFastFallDust (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //round dust
        instance.landFastFallDustTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero;
        instance.landFastFallDustTemplate.emitParams.position = position
                                                        + Vector3.down * colliderExtents.y
                                                        + Vector3.left * dir * colliderExtents.x * 3
                                                        + instance.landFastFallDustTemplate.emitParams.positionOffset;


        VFXManager.instance.EmitAtPosition(instance.landFastFallDustTemplate);


        //sharp dust
        instance.landFastFallDustSpikeTemplate.emitParams.rotation3D = (dir > 0) ? Vector3.up * 180 : Vector3.zero;                                                                   
        instance.landFastFallDustSpikeTemplate.emitParams.position = position
                                                        + Vector3.down * colliderExtents.y
                                                        + Vector3.left * dir * colliderExtents.x * 3
                                                        + instance.landFastFallDustSpikeTemplate.emitParams.positionOffset;


        VFXManager.instance.EmitAtPosition(instance.landFastFallDustSpikeTemplate);

    }

    public static void PlayWallSlideDust (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //normal
        instance.wallSlideDustTemplate.emitParams.rotation3D = ((dir > 0) ? Vector3.up * 180 : Vector3.zero);
        instance.wallSlideDustTemplate.emitParams.position = position
                                                           + Vector3.right * dir * colliderExtents.x * 1.2f
                                                           + Vector3.down * colliderExtents.y
                                                           + instance.wallSlideDustTemplate.emitParams.positionOffset;

                                                        
        VFXManager.instance.EmitAtPosition(instance.wallSlideDustTemplate);

        //fast
        if (velocity.y > -12) return;
        instance.wallSlideFastDustTemplate.emitParams.rotation3D = ((dir > 0) ? Vector3.up * 180 : Vector3.zero);
        instance.wallSlideFastDustTemplate.emitParams.position = position
                                                           + Vector3.right * dir * colliderExtents.x * 1.2f
                                                           + Vector3.down * colliderExtents.y
                                                           + instance.wallSlideFastDustTemplate.emitParams.positionOffset;


        VFXManager.instance.EmitAtPosition(instance.wallSlideFastDustTemplate);

    }

    public static void PlayWallSlideFastDust(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        instance.wallSlideFastDustTemplate.emitParams.rotation3D = ((dir > 0) ? Vector3.up * 180 : Vector3.zero);
        instance.wallSlideFastDustTemplate.emitParams.position = position
                                                           + Vector3.right * dir * colliderExtents.x * 1.2f
                                                           + Vector3.down * colliderExtents.y
                                                           + instance.wallSlideFastDustTemplate.emitParams.positionOffset;


        VFXManager.instance.EmitAtPosition(instance.wallSlideFastDustTemplate);

    }

    public static void PlayWallJumpParticles (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        instance.wallJumpDustTemplate.emitParams.rotation3D = Vector3.forward * (90 * dir - velocity.y * dir);
        instance.wallJumpDustTemplate.emitParams.position = position
                                                          + Vector3.right * colliderExtents.x * 0.7f * -dir;
        
        VFXManager.instance.EmitAtPosition(instance.wallJumpDustTemplate);

    }

    public static void PlayStunnedParticles (Vector3 position) {

        instance.stunnedDustTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.stunnedDustTemplate);

    }

    public static void PlayPlayerDeathParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //spirit explosion
        instance.spiritExplosionTemplate.emitParams.position = position;
        VFXManager.instance.EmitAtPosition(instance.spiritExplosionTemplate);

        //death insignia
        instance.deathInsigniaTemplate.emitParams.position = position;
        VFXManager.instance.EmitAtPosition(instance.deathInsigniaTemplate);

    }

    public static void PlaySludgeParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        instance.sludgeTemplate.emitParams.position = position;
        VFXManager.instance.EmitAtPosition(instance.sludgeTemplate);

    }







    //HATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHAT
    //HATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHAT
    //HATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHATHAT

    //new new new new new new new new new new new new new new new new new new new new new new new new new new new new
    public static void PlayHatGroundFrictionParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity, bool isDangerous) {
        
            instance.groundFrictionTemplate.numberOfParticles = (isDangerous) ? 2 : 1;
            instance.groundFrictionTemplate.emitParams.position = position
                                                                + Vector3.right * dir * colliderExtents.x;

            VFXManager.instance.EmitAtPosition(instance.groundFrictionTemplate);
        
    }

    //hit wall horizontal
    public static void PlayHatHitWallHorizontal(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity, bool isDangerous) {

        instance.hatHitWallTemplate.numberOfParticles = (isDangerous) ? 6 : 3;
        instance.hatHitWallTemplate.emitParams.position = position
                                                        + Vector3.right * dir * colliderExtents.x
                                                        + Vector3.right * dir * instance.hatHitWallTemplate.emitParams.positionOffset.x;

		instance.hatHitWallTemplate.emitParams.velocity = velocity;

        VFXManager.instance.EmitAtPosition(instance.hatHitWallTemplate);

    }

    //hit wall vertical
    public static void PlayHatHitWallVertical(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity, bool isDangerous) {

        instance.hatHitWallTemplate.numberOfParticles = (isDangerous) ? 6 : 3;
        instance.hatHitWallTemplate.emitParams.position = position
                                                        + Vector3.up * colliderExtents.y;

        instance.hatHitWallTemplate.emitParams.velocity = velocity;

        VFXManager.instance.EmitAtPosition(instance.hatHitWallTemplate);

    }

    //hit player
    public static void PlayHatHitPlayerParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {
        
        //genero
        instance.hatHitPlayerTemplate.emitParams.position = position;
                                                      
        VFXManager.instance.EmitAtPosition(instance.hatHitPlayerTemplate);

    }

    private static Vector3 minSlashSize = new Vector3(3, 13, 1);
    private static Vector3 maxSlashSize = new Vector3(6, 16, 1);
    public static void PlayDangerousHatHitPlayerParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //screen crack
        instance.screenCrackTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.screenCrackTemplate);



        //slash
        float slashAngle = Mathf.Atan2(-velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
        slashAngle += instance.slashTemplate.emitParams.rotationOffset;

        Vector3 slashSize3D = new Vector3(Random.Range(minSlashSize.x, maxSlashSize.x), Random.Range(minSlashSize.y, maxSlashSize.y), 1);

        instance.slashTemplate.emitParams.position = position;
        instance.slashTemplate.emitParams.startSize3D = slashSize3D;
        instance.slashTemplate.emitParams.rotation3D = new Vector3(0, 0, slashAngle);
        VFXManager.instance.EmitAtPosition(instance.slashTemplate);


        //blowback
        float blowBackAngle = Mathf.Atan2(-velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
        blowBackAngle += instance.blowBackFrontTemplate.emitParams.rotationOffset;

        instance.blowBackFrontTemplate.emitParams.position = position;
        instance.blowBackFrontTemplate.emitParams.rotation3D = new Vector3(0, 0, blowBackAngle);
        instance.blowBackFrontTemplate.emitParams.velocity = velocity.normalized * 5;
        VFXManager.instance.EmitAtPosition(instance.blowBackFrontTemplate);

        instance.blowBackBackTemplate.emitParams.position = position;
        instance.blowBackBackTemplate.emitParams.rotation3D = new Vector3(0, 0, blowBackAngle);
        instance.blowBackBackTemplate.emitParams.velocity = velocity.normalized * 5;
        VFXManager.instance.EmitAtPosition(instance.blowBackBackTemplate);

    }

    //hit hat
    public static void PlayHatHitHatParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        instance.hatHitHatTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.hatHitHatTemplate);

    }

    public static void PlayDangerousHatHitHatParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        instance.hatHitHatTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.hatHitHatTemplate);

    }

    public static void PlayThrowFXParticles (Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        //get the angle of the throw
        float angle = Mathf.Atan2(-velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
        //angle += 180;

        instance.throwFXTemplate.emitParams.rotation3D = new Vector3(0, 0, angle);
        instance.throwFXTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.throwFXTemplate);

    }

    public static void PlayDangerFinishParticles(Vector3 position, int dir, Vector3 colliderExtents, Vector2 velocity) {

        instance.dangerFinishTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.dangerFinishTemplate);

    }

    public static void PlayPickUpParticles(Vector3 position, int die, Vector3 colliderExtents, Vector2 velocity, LogicEntity e) {

        instance.pickUpTemplate.emitParams.position = position;

        VFXManager.instance.EmitAtPosition(instance.pickUpTemplate);


    }

    private static Vector3 minRaySize = new Vector3(2, 15, 0);
    private static Vector3 maxRaySize = new Vector3(4, 20, 0);
    public static void PlayHatDeathParticles(Vector3 position, int die, Vector3 colliderExtents, Vector2 velocity) {

        //ray center
        instance.rayCenterTemplate.emitParams.position = position;
        VFXManager.instance.EmitAtPosition(instance.rayCenterTemplate);

        //rays
        Vector3 raySize = new Vector3(Random.Range(minRaySize.x, maxRaySize.x), Random.Range(minRaySize.y, maxRaySize.y), 1);

        instance.raysTemplate.emitParams.position = position;
        instance.raysTemplate.emitParams.startSize3D = raySize;
        VFXManager.instance.EmitAtPosition(instance.raysTemplate);

        //fireworks
        instance.fireworksTemplate.emitParams.position = position;
        VFXManager.instance.EmitAtPosition(instance.fireworksTemplate);

        //sparkles
        instance.sparklesTemplate.emitParams.position = position;
        VFXManager.instance.EmitAtPosition(instance.sparklesTemplate);

    }























    //old old old old old old old old old old old old old old old old old old old old old old old old old 
    public static void PlayThrowTimeDoneParticles(Vector3 position, Vector2 direction) {

		//get the angle of the throw
		float angle = Mathf.Atan2(-direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;

		var poofEmitParams = new ParticleSystem.EmitParams();

		//add 180 to compensate for the initial offset
		if (direction != Vector2.zero)
			angle -= 90;
		else
			angle = 0;

		poofEmitParams.rotation3D = new Vector3(0, 0, angle);
		poofEmitParams.velocity = direction.normalized;

		VFXManager.instance.EmitAtPosition("Fire_Done_Smoke", poofEmitParams, 1, position + Vector3.up * 0.5f, false);

	}

	public static void PlayLandOnHeadEffect(Vector3 colliderExtents) {

		var energyEmitParams = new ParticleSystem.EmitParams();
		VFXManager.instance.EmitAtPosition("Pick_Up_Energy", energyEmitParams, 1, Vector3.down * colliderExtents.y, 0, 1, false);
		VFXManager.instance.EmitAtPosition("Pick_Up_Energy_White", 1, Vector3.down * colliderExtents.y, false);
		VFXManager.instance.EmitAtPosition("Pick_Up_Energy_Back", energyEmitParams, 1, Vector3.up * colliderExtents.y + Vector3.forward * 0.1f, 0, 1, false);

	}

	private static int lastSlashRotation = 0;
	private static Vector3 hitPlayerSizeMin = new Vector3(3, 13, 1);
	private static Vector3 hitPlayerSizeMax = new Vector3(6, 16, 1);
	public static void PlayHitEffect (Vector3 hitPoint, Vector3 thisVelocity, Vector3 thisLastVelocity, Vector3 otherVelocity) {

		VFXManager.instance.EmitAtPosition("Screen_Crack", 1, hitPoint, false);

		var spikeEmitParams = new ParticleSystem.EmitParams();

		//get the angle of the throw
		float otherAngle = Mathf.Atan2(-otherVelocity.normalized.y, otherVelocity.normalized.x) * Mathf.Rad2Deg;

		spikeEmitParams.rotation3D = new Vector3(0, 0, otherAngle);
		spikeEmitParams.startSize3D = new Vector3(5, 1.3f, 1);
		spikeEmitParams.velocity = otherVelocity.normalized * 3;
		VFXManager.instance.EmitAtPosition("Single_Spike", spikeEmitParams, 1, (hitPoint - Vector3.forward) + otherVelocity.normalized, 0, 0, false);

		float thisAngle = Mathf.Atan2(-thisVelocity.y, -thisVelocity.x) * Mathf.Rad2Deg;

		spikeEmitParams.rotation3D = new Vector3(0, 0, thisAngle);
		spikeEmitParams.startSize3D = new Vector3(4, 1f, 1);
		spikeEmitParams.velocity = thisVelocity.normalized * 2;
		VFXManager.instance.EmitAtPosition("Single_Spike", spikeEmitParams, 1, (hitPoint - Vector3.forward) - (Vector3) thisVelocity.normalized, 0, 0, false);

		float thisOldAngle = Mathf.Atan2(-thisLastVelocity.normalized.y, thisLastVelocity.normalized.x) * Mathf.Rad2Deg;

		spikeEmitParams.rotation3D = new Vector3(0, 0, thisOldAngle);
		spikeEmitParams.startSize3D = new Vector3(2, 0.75f, 1);
		spikeEmitParams.velocity = thisLastVelocity.normalized;
		VFXManager.instance.EmitAtPosition("Single_Spike", spikeEmitParams, 1, (hitPoint - Vector3.forward) + thisLastVelocity.normalized, false);



		float hitCircleAngle = Mathf.Atan2(-otherVelocity.normalized.y, otherVelocity.normalized.x) * Mathf.Rad2Deg;

		var hitCircleEmitParams = new ParticleSystem.EmitParams();
		hitCircleEmitParams.rotation3D = new Vector3(0, 0, hitCircleAngle);
		VFXManager.instance.EmitAtPosition("Hit_Circle", hitCircleEmitParams, 1, (hitPoint - Vector3.forward), 0, 0, false);

		//NEW SLASH LOGIC
		//get the angle of the throw
		float slashAngle = Mathf.Atan2(-otherVelocity.normalized.y, otherVelocity.normalized.x) * Mathf.Rad2Deg;
		slashAngle -= 90;

		var slashEmitParams = new ParticleSystem.EmitParams();
		slashEmitParams.velocity = otherVelocity.normalized;
		slashEmitParams.rotation3D = new Vector3(0, 0, slashAngle);
		slashEmitParams.startSize3D = new Vector3(Random.Range(3, 6), Random.Range(13, 16), 1);
		VFXManager.instance.EmitAtPosition("Slash", slashEmitParams, 1, hitPoint - Vector3.forward, 0, 0, false);

	}

}