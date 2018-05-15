using UnityEngine;

public static class ParticleExtentions {

	public static void CopyEmission (this ParticleSystem.EmissionModule self, EmissionTemplate other) {

		//turn the module on if the module is off
		self.enabled = other.enabled;

		//set rate over time
		//self.rate = other.rateOverTime;
		self.rateOverTimeMultiplier = other.rateOverTimeMultiplier;

		//set rate over distance
		//self.rateOverDistance = other.rateOverDistance;
		self.rateOverDistanceMultiplier = other.rateOverDistanceMultiplier;

		//Add logic for bursts;

	}

    public static void CopyShape (this ParticleSystem.ShapeModule self, ShapeTemplate other) {

		//turn the module on if the module is off
		self.enabled = other.enabled;

		switch (self.shapeType) {

		case ParticleSystemShapeType.Box:
			//use only generic
			break;

		case ParticleSystemShapeType.BoxEdge:
			//use only generic
			break;

		case ParticleSystemShapeType.BoxShell:
			//use only generic
			break;

		case ParticleSystemShapeType.Circle:
			
			self.radius = other.radius;
			self.radiusThickness = other.radiusThickness;

			self.arc = other.arc;
			self.arcMode = other.arcMode;
			self.arcSpread = other.arcSpread;

			break;

		case ParticleSystemShapeType.Cone:

			self.angle = other.angle;

			self.radius = other.radius;
			self.radiusThickness = other.radiusThickness;

			self.arc = other.arc;
			self.arcMode = other.arcMode;
			self.arcSpread = other.arcSpread;
			self.length = other.length;
			break;

		case ParticleSystemShapeType.ConeVolume:

			self.angle = other.angle;

			self.radius = other.radius;
			self.radiusThickness = other.radiusThickness;

			self.arc = other.arc;
			self.arcMode = other.arcMode;
			self.arcSpread = other.arcSpread;
			self.length = other.length;

			break;

		case ParticleSystemShapeType.Donut:
			
			self.radius = other.radius;
			self.donutRadius = other.donutRadius;
			self.radiusThickness = other.radiusThickness;

			self.arc = other.arc;self.arcMode = other.arcMode;
			self.arcSpread = other.arcSpread;
			self.length = other.length;
			break;

		case ParticleSystemShapeType.Hemisphere:

			self.radius = other.radius;
			self.radiusThickness = other.radiusThickness;
			break;

		case ParticleSystemShapeType.Mesh:
			//not supporting mesh for the moment, ask me if needed
			break;

		case ParticleSystemShapeType.MeshRenderer:
			//not supporting mesh for the moment, ask me if needed
			break;

		case ParticleSystemShapeType.SingleSidedEdge:
			self.radius = other.radius;
			self.radiusMode = other.radiusMode;
			self.radiusSpread = other.radiusSpread;
			break;

		case ParticleSystemShapeType.SkinnedMeshRenderer:
			//not supporting mesh for the moment, ask me if needed
			break;

		case ParticleSystemShapeType.Sphere:

			self.radius = other.radius;
			self.radiusThickness = other.radiusThickness;
			break;


		}

		//Generic
		self.position = other.position;
		self.rotation = other.rotation;
		self.scale = other.scale;

		self.alignToDirection = other.alignToDirection;
		self.randomDirectionAmount = other.randomDirectionAmount;
		self.sphericalDirectionAmount = other.sphericalDirectionAmount;
		self.randomPositionAmount = other.randomPositionAmount;

		//not shown in UI, adding for safety
//		self.radiusSpeed = other.radiusSpeed;
//		self.radiusSpeedMultiplier = other.radiusSpeedMultiplier;
//		self.arcSpeed = other.arcSpeed;
//		self.arcSpeedMultiplier = other.arcSpeedMultiplier;

	}

	public static void CopyColorOverLifeTime (this ParticleSystem.ColorOverLifetimeModule self, ColorOverLifeTimeTemplate other) {

		//turn the module on if the module is off
		self.enabled = other.enabled;

		//copy color
		self.color = other.color;

	}

    public static void CopyTextureSheetAnimation (this ParticleSystem.TextureSheetAnimationModule self, TextureSheetAnimationTemplate other) {

        //turn the module on if the module is off
        self.enabled = other.enabled;

        self.numTilesX = other.tilesX;
        self.numTilesY = other.tilesY;
        self.flipU = other.flipU;
        self.flipV = other.flipV;

    }

}
