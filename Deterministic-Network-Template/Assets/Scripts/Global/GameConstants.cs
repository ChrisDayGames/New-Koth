using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Determinism;
using Entitas;

public enum Teams : int {

	NONE,
	BLUE,
	RED,
	GREEN,
	YELLOW

}

//should contain all constant values in the game
public static class GameConstants {

	public const int PPU = 16;
	public const int MAX_PLAYERS = 4;

	public const long THROW_SPEED_CUTOFF_X = FixedMath.Hundredth * 35 * 50;
	public const long THROW_SPEED_CUTOFF_Y = FixedMath.Tenth * 50;
	public const long MAX_HAT_SPEED = FixedMath.ONE * 75;	//was FixedMath.Create (75) before

}
