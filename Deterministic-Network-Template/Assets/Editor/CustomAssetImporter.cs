using UnityEngine;
using UnityEditor;  

internal sealed class CustomAssetImporter : AssetPostprocessor {
	
	private const int textureSize = 1024;

	#region Methods

	//Pre Processors
	//
	// This event is raised when a texture asset is imported
	private void OnPreprocessTexture() {

		var fileNameIndex = assetPath.LastIndexOf('/');
		var fileName = assetPath.Substring(fileNameIndex + 1);

		//setting default import settings
		var importSettings = new TextureImporterPlatformSettings ();
		importSettings.overridden = true;
		importSettings.maxTextureSize = textureSize;
		importSettings.format = TextureImporterFormat.RGBA32;

		//creating the importer and adding the settings
		var importer = assetImporter as TextureImporter;
		if( importer == null ) return;

		Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
		if (asset) {
			EditorUtility.SetDirty(asset);
			return;
		}

		//use per file type import
		//string name = importer.assetPath.ToLower();
		//string extension = name.Substring(name.Length - 3, 3);

		//set import settings for cpu
		importSettings.name = "Standalone";
		importer.SetPlatformTextureSettings(importSettings);

		//set import settings for xbox
		//importSettings.name = "XboxOne";
		//importer.SetPlatformTextureSettings(importSettings);

		//set import settings for switch
		//importSettings.name = "Switch";
		//importer.SetPlatformTextureSettings(importSettings);

		//sprite settings
		importer.textureType = TextureImporterType.Sprite;

		//advanced settings
		importer.alphaSource = TextureImporterAlphaSource.FromInput;
		importer.alphaIsTransparency = importer.DoesSourceTextureHaveAlpha();
		importer.isReadable = false;
		importer.mipmapEnabled = false;
		importer.filterMode = FilterMode.Point;

		if (fileName.Contains("cha")) {

			importer.spriteImportMode = SpriteImportMode.Multiple;
			importer.spritePixelsPerUnit = 24;

		} else if (fileName.Contains("hat")) {

			importer.spriteImportMode = SpriteImportMode.Single;
			importer.spritePixelsPerUnit = 24;
			
		} else {

			//sprite settings
			importer.spriteImportMode = SpriteImportMode.Single;
			importer.spritePixelsPerUnit = 16;

		}

	}

	// This event is raised when a audio asset is imported
	private void OnPreprocessAudio() {
//		var fileNameIndex = assetPath.LastIndexOf('/');
//		var fileName = assetPath.Substring(fileNameIndex + 1);
//
//		if (!fileName.Contains("snd")) return;

		var importer = assetImporter as AudioImporter;
		if(importer == null) return;
	}

	//Post Processors
	private void OnPostprocessTexture(Texture2D import) {}

	private void OnPostprocessAudio(AudioClip import) {}

	#endregion
}