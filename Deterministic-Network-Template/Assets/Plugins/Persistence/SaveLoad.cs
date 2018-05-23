using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

namespace Persistence {

	public static class SaveLoad {

		#region saveable directories

		public static readonly string EDITOR_ROOT = Application.dataPath + "/Resources/"; 
		public static readonly string IN_GAME_ROOT = Application.persistentDataPath;

		#endregion

		#if UNITY_EDITOR
		private static readonly string rootDirectory = EDITOR_ROOT;
		#else
		private static readonly string rootDirectory = IN_GAME_ROOT;
		#endif

		#region binary save and load

		public static void Save <T> (T data) where T : ISaveable{

			string path = rootDirectory + data.path;

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (path);
			Debug.Log ("Saved Successful " + file.Name);

			bf.Serialize(file, data);
			file.Close();

		}
			
		public static T Load <T> (string path, bool addRoot = true) where T : ISaveable {


			if (addRoot)
				path = rootDirectory + path;

			if(File.Exists(path)) {

				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(path, FileMode.Open);
				T data = (T) bf.Deserialize(file);

				file.Close();
				return data;

			} else {

				throw new IOException ("No files were found at " + path);

			}

		}

		public static T[] LoadAll<T> (string path, string extension, bool addRoot = true) where T : ISaveable {

			if (addRoot)
				path = rootDirectory + path;

			if(Directory.Exists(path)) {

				Debug.Log ("exists");
				Debug.Log (path);

				string[] files = Directory.GetFiles(path, "*" + extension);
				List <T> data = new List <T> ();

				for (int i = 0; i < files.Length; i++)
					data.Add(Load <T> (files [i], false));
				

				return data.ToArray ();

			} else {
				
				throw new IOException ("No directory found at " + path);

			}

		}

		#endregion

	}
		
}