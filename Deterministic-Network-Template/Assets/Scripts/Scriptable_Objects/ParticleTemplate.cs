using UnityEngine;

[System.Serializable]
public class EmitParamsTemplate {

	public bool enabled = false;

	public Vector3 positionOffset;
	public Vector3 velocity;
	public Vector3 velocityMultiplier;
	public long rotationOffset;
	public Vector3 rotation3DOffset;

	[HideInInspector]
	public Vector3 position;
	[HideInInspector]
	public float rotation;
	[HideInInspector]
	public Vector3 rotation3D;

	public float startSize;
    public Vector3 startSize3D;
	public float startLifetime;
	public Color startColor;
	public bool applyShapeToPosition;
	
}

[System.Serializable]
public class EmissionTemplate {

	//turn the module on if the module is off
	public bool enabled = false;

	public AnimationCurve rateOverTime;
	public float rateOverTimeMultiplier;

	public AnimationCurve rateOverDistance;
	public float rateOverDistanceMultiplier;

}

[System.Serializable]
public class ShapeTemplate {

	//turn the module on if the module is off
	public bool enabled = false;

	public ParticleSystemShapeType shapeType;

	public Vector3 position;
	public Vector3 rotation;
	public Vector3 scale;

	public bool alignToDirection;
	public float randomDirectionAmount;
	public float sphericalDirectionAmount;
	public float randomPositionAmount;

	public float angle;

	public float arc;
	public ParticleSystemShapeMultiModeValue arcMode;
	public float arcSpread;
	public float length = 5;

	public float radius;
	public float radiusThickness;
	public float donutRadius;
	public ParticleSystemShapeMultiModeValue radiusMode;
	public float radiusSpread;

}

[System.Serializable]
public class ColorOverLifeTimeTemplate {

	//turn the module on if the module is off
	public bool enabled = false;
	public Gradient color;

}

[System.Serializable]
public class TextureSheetAnimationTemplate {

    //turn the module on if the module is off
    public bool enabled = false;
    public int tilesX;
    public int tilesY;
    public float flipU;
    public float flipV;

}

[CreateAssetMenu]
public class ParticleTemplate : ScriptableObject {

    public string systemName;
	public int numberOfParticles;

	public EmitParamsTemplate emitParams;
	//public EmissionTemplate emission;
	public ShapeTemplate shape;
	public ColorOverLifeTimeTemplate colorOverLifetime;
    public TextureSheetAnimationTemplate textureSheetAnimation;

}