using Determinism;
using Entitas;
using UnityEngine;

public static class Pixels {

	public const int PIXELS_PER_UNIT = (int) GameConstants.PPU;
	public const float UNITS_PER_PIXEL = 1f / PIXELS_PER_UNIT;

	public static int SCALED_PPU = PIXELS_PER_UNIT;

	public static float ContrainToPixel (this float value) {

		float pixelSize = (1f / (PIXELS_PER_UNIT));
		return Mathf.Round (value / pixelSize) * pixelSize;

	}

	public static float ContrainToPixelScale (this float value) {

		float pixelSize = (1f / (SCALED_PPU));
		return Mathf.Round (value / pixelSize) * pixelSize;

	}

	public static float CalculatePPUFromScale (float scale) {

		return (-PIXELS_PER_UNIT / 2) * scale + (3 * (PIXELS_PER_UNIT / 2));

	}

	public static float CalculateScaleFromPPU (float newPPU) {

		return newPPU / PIXELS_PER_UNIT;

	}

	public static float PixelRound (this float value)
	{
		float modValue = value % UNITS_PER_PIXEL;
		float flatValue = value - modValue;
		if (modValue >= UNITS_PER_PIXEL / 2)
			flatValue += UNITS_PER_PIXEL;
		return flatValue;

	}

	public static float PixelRoundScaled (this float value)
	{
		
		float scaledUPP = UNITS_PER_PIXEL * SCALED_PPU / PIXELS_PER_UNIT;

		float modValue = value % scaledUPP;
		float flatValue = value - modValue;
		if (modValue >= scaledUPP / 2)
			flatValue += scaledUPP;
		return flatValue;

	}

	public readonly static long MIN = FixedMath.Create (382, 1000);
	public readonly static long MED = FixedMath.Create (707, 1000);
	public readonly static long MAX = FixedMath.Create (924, 1000);

	public readonly static long ZERO_THRESHOLD = FixedMath.Create (2, 10);
	public readonly static long MIN_THRESHOLD = FixedMath.Create (62, 100);
	public readonly static long MED_THRESHOLD = FixedMath.Create (8, 10);
	public readonly static long MAX_THRESHOLD = FixedMath.Create (96, 100);

	public static long ContrainAxisTo16Angles (this long axis) {

		int sign = axis.Sign ();
		axis = axis.Abs ();

		if (axis <= ZERO_THRESHOLD) {
			axis = 0;

		} else if (axis <= MIN_THRESHOLD) {
			axis = MIN;

		} else if (axis <= MED_THRESHOLD) {
			axis = MED;

		} else if (axis <= MAX_THRESHOLD) {
			axis = MAX;

		} else {

			axis = FixedMath.ONE;

		}
			
		return axis * sign;

	}

	public static float ContrainAxisTo16Angles (this float axis) {
		
		return ContrainAxisTo16Angles (FixedMath.Create (axis)).ToFloat ();

	}

	public static FixedVector2 ContrainTo16Angles (this FixedVector2 normalizedVector) {

		return new FixedVector2 (

			normalizedVector.x.ContrainAxisTo16Angles (),
			normalizedVector.y.ContrainAxisTo16Angles ()
		
		);

	}

}
