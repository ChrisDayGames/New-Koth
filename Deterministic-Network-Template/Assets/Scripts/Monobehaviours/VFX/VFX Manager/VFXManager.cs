using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (VFXLibrary))]
public class VFXManager : MonoBehaviour {

	public static VFXManager instance;

    private int i;

	[SerializeField]
	private VFXLibrary library;

	// Use this for initialization
	void Awake () {

		instance = this;
		
	}

	void OnValidate () {

		if (Application.isPlaying) return;
		library = GetComponent<VFXLibrary> ();

	}

	public void EmitAtPosition (ParticleTemplate template) {

		ParticleSystem system = library.GetParticleSystemFromName(template.systemName);
		if (system == null)
			return;

		var emitParams = new ParticleSystem.EmitParams ();

		if (template.emitParams.enabled) {

			emitParams.position = template.emitParams.position + template.emitParams.positionOffset;
			emitParams.rotation = template.emitParams.rotation + template.emitParams.rotationOffset;
			emitParams.rotation3D = template.emitParams.rotation3D + template.emitParams.rotation3DOffset;

			if (template.emitParams.startSize >= 0) emitParams.startSize = template.emitParams.startSize;
            else emitParams.startSize3D = template.emitParams.startSize3D;
			emitParams.startLifetime = template.emitParams.startLifetime;
			emitParams.startColor = template.emitParams.startColor;

			emitParams.velocity = template.emitParams.velocity.ComponentMultiply(template.emitParams.velocityMultiplier);

			emitParams.applyShapeToPosition = template.emitParams.applyShapeToPosition;

		}

		if (template.shape.enabled) {

			ParticleSystem.ShapeModule shape = system.shape;
			shape.CopyShape (template.shape);

		}

		if (template.colorOverLifetime.enabled) {

			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = system.colorOverLifetime;
			colorOverLifetime.CopyColorOverLifeTime (template.colorOverLifetime);

		}

        if (template.textureSheetAnimation.enabled) {

            ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = system.textureSheetAnimation;
            textureSheetAnimation.CopyTextureSheetAnimation(template.textureSheetAnimation);

        }

        system.Emit(emitParams, template.numberOfParticles);

	}






	public void EmitAtPosition(ParticleSystem system, int numberOfParticles, Vector3 position, bool retainShape = false, Transform relativeTo = null) {

		if (system == null) return;
		
		// Any parameters we assign in emitParams will override the current system's when we call Emit.
		// Here we will override the position and velocity. All other parameters will use the behavior defined in the Inspector.
		var emitParams = new ParticleSystem.EmitParams();
		emitParams.position = position;
		emitParams.applyShapeToPosition = retainShape;
		system.Emit(emitParams, numberOfParticles);

		if (relativeTo != null) {
			
			var main = system.main;
			main.simulationSpace = ParticleSystemSimulationSpace.Custom;
			main.customSimulationSpace = relativeTo;

		}

	}

    public void EmitAtPosition(ParticleSystem system, ParticleSystem.EmitParams emitParams, int numberOfParticles, Vector3 position, bool retainShape = false) {

        if (system == null) return;

        // Any parameters we assign in emitParams will override the current system's when we call Emit.
        // Here we will override the position and velocity. All other parameters will use the behavior defined in the Inspector.
        emitParams.position = position;
        emitParams.applyShapeToPosition = retainShape;
        system.Emit(emitParams, numberOfParticles);

    }

    public void EmitAtPosition(string systemName, int numberOfParticles, Vector3 position, bool retainShape = false, Transform relativeTo = null) {
		EmitAtPosition(library.GetParticleSystemFromName(systemName), numberOfParticles, position, retainShape, relativeTo);
    }

    public void EmitAtPosition(string systemName, ParticleSystem.EmitParams emitParams, int numberOfParticles, Vector3 position, bool retainShape = false) {
        EmitAtPosition(library.GetParticleSystemFromName(systemName), emitParams, numberOfParticles, position, retainShape);
    }

    public void EmitAtPosition(string systemName, ParticleSystem.EmitParams emitParams, int numberOfParticles, Vector3 position, int _playerIndex,  int _effectIndex, bool retainShape = false) {

        ParticleSystem p = library.GetParticleSystemFromName(systemName);
//        ParticleSystem.ColorOverLifetimeModule c = p.colorOverLifetime;
//
//        switch(_effectIndex) {
//            case 0:
//                c.color = PlayerColorManager.instance.players[_playerIndex].hatFireColor;
//                break;
//            case 1:
//                c.color = PlayerColorManager.instance.players[_playerIndex].hatLoseLifeColor;
//                break;
//            case 2:
//                break;
//            case 3:
//                break;
//            default:
//                break;
//        }

        EmitAtPosition(library.GetParticleSystemFromName(systemName), emitParams, numberOfParticles, position, retainShape);
    
    }

}