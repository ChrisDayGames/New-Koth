using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using Determinism;

public interface ISaveable {

	//levels/this.name
	//colors/this.name.
	string name {get;}
	string path {get;}
	
}

public static class SaveLoad {

	public const string folder = "/Resources/Levels";

	public static void Save <T> (T data, string folder) where T : ISaveable {

		string rootDirectory = Application.persistentDataPath;

		#if UNITY_EDITOR
		rootDirectory = Application.dataPath;
		#endif

		rootDirectory += folder;

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (rootDirectory + data.path);

		bf.Serialize(file, data);
		file.Close();

	}


	public static T Load <T> (string path) where T : ISaveable{

		string rootDirectory = Application.persistentDataPath;

		#if UNITY_EDITOR
		rootDirectory = Application.dataPath;
		#endif

		rootDirectory = path;

		if(File.Exists(rootDirectory)) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(rootDirectory, FileMode.Open);
			T data = (T) bf.Deserialize(file);

			file.Close();
			return data;

		} else {

			Debug.LogError ("No level files were found");
			return default (T);

		}

	}

	

	public static void SaveLevel(LevelData data) {

		string path = Application.persistentDataPath;

		#if UNITY_EDITOR
		path = Application.dataPath + folder;
		#endif


		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (path + "/hills.lvl");

		bf.Serialize(file, data);
		file.Close();

	}

	public static LevelData LoadLevel() {

		string path = Application.persistentDataPath;

		#if UNITY_EDITOR
		path = Application.dataPath + folder;
		#endif

		if(File.Exists(path + "/hills.lvl")) {
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path + "/hills.lvl", FileMode.Open);
			LevelData data = (LevelData)bf.Deserialize(file);
			file.Close();

			return data;

		} else {

			Debug.LogError ("No level files were found");
			return new LevelData ();

		}

	}

	public static LevelData ReadLevel (TextAsset textAsset) {

		Stream stream = new MemoryStream(textAsset.bytes);
		BinaryFormatter formatter = new BinaryFormatter();                
		return formatter.Deserialize(stream) as LevelData;

	}

}