using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Characters : byte {

	BIRTHDAY,
	KIARA,
	CAPTAIN_HAT,
	COLE,
	SNAPBACK,
	WASHING_MACHINE,
	DARK_BIRTHDAY,
	ZOE,
	FOREST,
	FATCAT,
	INSIGNIA,
	RANDOM

}
	
public enum Levels : byte {

	HATLANDIAN_HILLS,

}

public enum Environments : byte {

	GRASS,
	ICE,
	FIRE,
	SWAMP,
	VOID,
	FACTORY,
	CLOUDS
}

public static class Assets {

	public const string LEVEL_DIRECTORY = "/Resources/Levels/";

	private static Dictionary <Characters, CharacterBlueprint> allCharacters = new Dictionary <Characters, CharacterBlueprint> () {
		
		{Characters.BIRTHDAY, Resources.Load <CharacterBlueprint> ("Characters/Birthday")},
		{Characters.KIARA, Resources.Load <CharacterBlueprint> ("Characters/Kiara")},
		{Characters.CAPTAIN_HAT, Resources.Load <CharacterBlueprint> ("Characters/Captain Hat")},
		{Characters.COLE, Resources.Load <CharacterBlueprint> ("Characters/Cole")},
		{Characters.SNAPBACK, Resources.Load <CharacterBlueprint> ("Characters/Snapback")},
		{Characters.WASHING_MACHINE, Resources.Load <CharacterBlueprint> ("Characters/Washing Machine")},
		{Characters.DARK_BIRTHDAY, Resources.Load <CharacterBlueprint> ("Characters/Dark Birthday")},
		{Characters.ZOE, Resources.Load <CharacterBlueprint> ("Characters/Zoe")},
		{Characters.FOREST, Resources.Load <CharacterBlueprint> ("Characters/Forest")},
		{Characters.FATCAT, Resources.Load <CharacterBlueprint> ("Characters/Fatcat")},
		{Characters.INSIGNIA, Resources.Load <CharacterBlueprint> ("Characters/Insignia")},
		{Characters.RANDOM, Resources.Load <CharacterBlueprint> ("Characters/Random")},

	};

	private static Dictionary <Levels, TextAsset> allLevels = new Dictionary <Levels, TextAsset> () {
		//All Levels
		{Levels.HATLANDIAN_HILLS, Resources.Load ("Levels/hills.lvl") as TextAsset},

	};

	private static Dictionary <Environments, LevelBlueprint> allEnvironments = new Dictionary <Environments, LevelBlueprint> () {
		//Hatlandian Hills
		{Environments.GRASS, Resources.Load <LevelBlueprint> ("Environments/Grass")},
		{Environments.ICE, Resources.Load <LevelBlueprint> ("Environments/Ice")},
		{Environments.FIRE, Resources.Load <LevelBlueprint> ("Environments/Fire")},
		{Environments.SWAMP, Resources.Load <LevelBlueprint> ("Environments/Swamp")},
		{Environments.VOID, Resources.Load <LevelBlueprint> ("Environments/Void")},
		{Environments.FACTORY, Resources.Load <LevelBlueprint> ("Environments/Factory")},
		{Environments.CLOUDS, Resources.Load <LevelBlueprint> ("Environments/Clouds")},

	};


	public static CharacterBlueprint Get (Characters key) {

		if (allCharacters.ContainsKey (key))
			return allCharacters[key];

		Debug.LogError ("No character asset found at using " + key + ", make sure spelling is correct.");
		return null;
		
	}

	public static TextAsset Get (Levels key) {

		if (allLevels.ContainsKey (key))
			return allLevels[key];

		Debug.LogError ("No level asset found at using " + key + ", make sure spelling is correct.");
		return null;

	}

	public static LevelBlueprint Get (Environments key) {

		if (allEnvironments.ContainsKey (key))
			return allEnvironments[key];

		Debug.LogError ("No environment asset found at using " + key + ", make sure spelling is correct.");
		return null;

	}

}
