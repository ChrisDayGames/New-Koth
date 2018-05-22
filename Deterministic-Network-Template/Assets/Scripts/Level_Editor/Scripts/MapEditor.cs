//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Determinism;
using Persistence;

namespace LevelEditor {

	public enum Tool {

		BRUSH,
		FILL,
		LINE

	}

	public interface IToolChangeListener {
		void OnToolChanged (Tool t);
	}

	public interface IMirrorChangeListener {
		void OnMirrorChanged (bool mirrorX, bool mirrorY);
	}

	public interface IModeChangeListener {
		void OnModeChanged (bool isEditMode);
	}

	public interface IMouseMoveListener {
		void OnMouseMoved (Vector2 mouseTilePosition);
	}

	public class MapEditor : MonoBehaviour {

		public string levelName;

		public static MapEditor instance;

		public List <IToolChangeListener> toolListeners;
		public List <IMirrorChangeListener> mirrorListeners;
		public List <IModeChangeListener> modeListeners;
		public List <IMouseMoveListener> mouseMoveListeners;

		public GameObject orthoCam;
		public GameObject cameraHolder;

		[Header("Environment Type")]
		public Environments environment;
		public LayerMask levelEditorObjects;

		[Header("World Params")]
		public Vector2Int mapSize = new Vector2Int (10, 10);		//the number of tiles in the map
		public Vector2 tileSize = new Vector2 (1, 1);		//the number of unity units for the tiles width and height
		public int pixelsPerTile = 16;

		[Range (1, 8)]
		public int numPlayers;
		public GameObject spawnPointPrefab;
		public GameObject levelBoundPrefab;

		Texture2D texture;
		Material mat;
		Vector2Int textureSize;

		DynamicMesh dynamicMesh;
		GameObject skybox;

		//Tools to make editing faster
		Tool currentTool = Tool.BRUSH;
		bool mirrorX = false;
		bool mirrorY = false;
		bool isInEditMode = true;
		bool isShowingObjects = false;

		GameObject selectedObject;

		private GameObject[] spawnPoints;
		private GameObject[] levelBounds;

		//the actual map of tiles
		//is represented by a grid of ints
		public TileGrid map;
		Stack<int[]> moves;
		Vector2[] tileCoords;

		//these offsets are used to center the map to (0,0)
		float mapOffsetX {get {return map.offsetX;}}
		float mapOffsetY {get {return map.offsetY;}}

		//a reference to the camera
		Camera cam;

		//variables for the mouse controls of the program
		float mouseMovementBorderSize = 100f;
		float maxCamBorder = 2f;

		float minScrollSpeed = 1f;
		float maxScrollSpeed = 10f;

		//min and max for zooming in and out
		float minOrthographicSize = 5f;
		float maxOrthographicSize {get {return ((mapOffsetX > mapOffsetY) ? mapOffsetX : mapOffsetY) + maxCamBorder;}}

		//the current type of tile that the user is drawing with
		int currentBrushValue = 0;

		//offset used for determining max scroll distance
		float camOffset {get {return cam.orthographicSize - maxCamBorder;}}

		public void Awake () {

			if (instance == null)
				instance = this;
			else
				Destroy (this);

			toolListeners = new List<IToolChangeListener> ();
			mirrorListeners = new List<IMirrorChangeListener> ();
			modeListeners =  new List<IModeChangeListener> ();
			mouseMoveListeners =  new List<IMouseMoveListener> ();

			//initialize the map
			map = new TileGrid (mapSize, tileSize);
			moves = new Stack<int[]>();

		}

		// Use this for initialization
		void Start () {

			levelBounds = new GameObject[numPlayers];

			GameObject go = new GameObject ("Mesh");
			dynamicMesh = go.AddComponent <DynamicMesh> ();
			dynamicMesh.transform.parent = transform;

			InitializeSpawnPoints ();
			InitializeLevelBounds ();

			LoadEnvironment (environment);

			cam = orthoCam.GetComponent <Camera> ();
			cam.orthographicSize = maxOrthographicSize;

			EnterEditMode ();
			ToggleObjects (isShowingObjects);
			ChangeTool (Tool.BRUSH);

		}

		private void InitializeSpawnPoints () {

			spawnPoints = new GameObject[numPlayers];
			for (int i = 0; i < spawnPoints.Length; i++) {

				spawnPoints[i] = Instantiate <GameObject> (spawnPointPrefab);
				transform.parent = this.transform;

			}

		}

		private void DestroySpawnPoints () {

			if (spawnPoints.Length == 0) return;

			for (int i = 0; i < spawnPoints.Length; i++) {

				Destroy(spawnPoints[i]);

			}

		}

		private void SetSpawnPoints (FixedVector2[] positions) {

			numPlayers = positions.Length;

			DestroySpawnPoints ();
			InitializeSpawnPoints ();

			for (int i = 0; i < positions.Length; i++) {

				spawnPoints[i].transform.position = positions[i].ToVector3 ();

			}

		}

		private void InitializeLevelBounds () {

			levelBounds = new GameObject[4];
			for (int i = 0; i < levelBounds.Length; i++) {

				levelBounds[i] = Instantiate <GameObject> (levelBoundPrefab);
				transform.parent = this.transform;

			}

			SetLevelBounds (-mapOffsetX, mapOffsetX, -mapOffsetY, mapOffsetY);

		}

		private void SetLevelBounds (float minX, float maxX, float minY, float maxY) {

			levelBounds[0].transform.position = new Vector3 (minX, 0, 0);
			levelBounds[0].transform.localScale = new Vector3 (1, mapOffsetY * 2, 0.2f);

			levelBounds[1].transform.position = new Vector3 (maxX, 0, 0);
			levelBounds[1].transform.localScale = new Vector3 (1, mapOffsetY * 2, 0.2f);

			levelBounds[2].transform.position = new Vector3 (0, minY, 0);
			levelBounds[2].transform.localScale = new Vector3 (mapOffsetX * 2, 1, 0.2f);

			levelBounds[3].transform.position = new Vector3 (0, maxY, 0);
			levelBounds[3].transform.localScale = new Vector3 (mapOffsetX * 2, 1, 0.2f);

		}
		
		public void ToggleObjects (bool isOn) {

			isShowingObjects = isOn;

			foreach (GameObject go in spawnPoints)
				go.SetActive (isOn);

			foreach (GameObject go in levelBounds)
				go.SetActive (isOn);

		}

		private void LoadEnvironment (Environments e) {

			environment = e;
			texture = Assets.Get (environment).texture;
			mat = Assets.Get (environment).mat;

			textureSize.x = texture.width / pixelsPerTile;
			textureSize.y = texture.height / pixelsPerTile;

			dynamicMesh.CreateMesh (LevelBuilder.GenerateMesh (map, textureSize), texture, mat);

			if (!isInEditMode) {
				Destroy (skybox);
				skybox = Instantiate <GameObject> (Assets.Get (environment).skybox);
			}

		}

		// Update is called once per frame
		bool shouldGenerateMesh = false;
		void Update () {

			if (isInEditMode) {
				
				HandleMovement ();
				HandleScrolling ();
				HandleHotKeyInput();
				HandleMouseInput ();

			}

			if (shouldUndo)
				UndoLastAction ();

			HandleLevelSelect ();
			if (shouldGenerateMesh)
				GenerateMesh ();

			if (Input.GetKeyDown(KeyCode.Alpha0))
				ToggleEditMode ();

		}

		private void GenerateMesh () {
			
			shouldGenerateMesh = false;
			dynamicMesh.UpdateMesh (LevelBuilder.GenerateMesh (map, textureSize));

		}
			
		private void HandleHotKeyInput () {

			//Determine which tool should be used
			if (Input.GetKeyDown(KeyCode.G))
				ChangeTool (Tool.FILL);

			else if (Input.GetKeyDown(KeyCode.LeftShift))
				ChangeTool (Tool.LINE);

			else if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.LeftShift))
				ChangeTool (Tool.BRUSH);



			//Determine the mirror state
			if (Input.GetKeyDown(KeyCode.M))
				ChangeMirroState (!mirrorX, mirrorY);

			if (Input.GetKeyDown(KeyCode.N))
				ChangeMirroState (mirrorX, !mirrorY);

			//save and load the level
			if (Input.GetKeyDown (KeyCode.S)) {
				SaveLevel ();
			} else if (Input.GetKeyDown (KeyCode.D)) {
				LoadLevel ();
			}

			//show hide objects
			if (Input.GetKeyDown (KeyCode.H)) {
				isShowingObjects = !isShowingObjects;
				ToggleObjects (isShowingObjects);
			}


			//undo last action
			if (Input.GetKeyDown(KeyCode.Z)) {
				RequestUndo ();
			}

		}

		//load level skin
		private void HandleLevelSelect () {

			if (Input.GetKeyDown (KeyCode.Alpha1))
				LoadEnvironment (Environments.GRASS);

			else if (Input.GetKeyDown (KeyCode.Alpha2))
				LoadEnvironment (Environments.ICE);

			else if (Input.GetKeyDown (KeyCode.Alpha3))
				LoadEnvironment (Environments.FIRE);

			else if (Input.GetKeyDown (KeyCode.Alpha4))
				LoadEnvironment (Environments.SWAMP);

			else if (Input.GetKeyDown (KeyCode.Alpha5))
				LoadEnvironment (Environments.VOID);

			else if (Input.GetKeyDown (KeyCode.Alpha6))
				LoadEnvironment (Environments.FACTORY);
			
			else if (Input.GetKeyDown (KeyCode.Alpha7))
				LoadEnvironment (Environments.CLOUDS);

		}

		public void ChangeTool (Tool t) {

			this.currentTool = t;

			//event listeners here
			foreach (IToolChangeListener listener in toolListeners) {

				listener.OnToolChanged (currentTool);
			}

		}

		public void ChangeMirroState (bool _mirrorX, bool _mirrotY) {

			this.mirrorX = _mirrorX;
			this.mirrorY = _mirrotY;

			//event listeners here

			foreach (IMirrorChangeListener listener in mirrorListeners) {

				listener.OnMirrorChanged (mirrorX, mirrorY);

			}

		}

		private void HandleMovement () {

			Vector3 mousePosition = Input.mousePosition;
			Vector2 camMovement = new Vector2 (0, 0);

			//check for cam movement left and right
			if (mousePosition.x > 0 && mousePosition.x < mouseMovementBorderSize && cam.transform.position.x - camOffset > -mapOffsetX) {

				camMovement.x = -Mathf.Lerp (maxScrollSpeed, minScrollSpeed, (mousePosition.x / mouseMovementBorderSize));

			} else if (mousePosition.x < Screen.width && mousePosition.x > Screen.width - mouseMovementBorderSize && cam.transform.position.x + camOffset < mapOffsetX) {

				camMovement.x = Mathf.Lerp (maxScrollSpeed, minScrollSpeed, ((Screen.width - mousePosition.x) / mouseMovementBorderSize));

			}

			//check for cam movement up and down
			if (mousePosition.y > 0 && mousePosition.y < mouseMovementBorderSize && cam.transform.position.y - camOffset > -mapOffsetY) {

				camMovement.y = -Mathf.Lerp (maxScrollSpeed, minScrollSpeed, (mousePosition.y / mouseMovementBorderSize));

			} else if (mousePosition.y < Screen.height && mousePosition.y > Screen.height - mouseMovementBorderSize && cam.transform.position.y + camOffset < mapOffsetY) {

				camMovement.y = Mathf.Lerp (maxScrollSpeed, minScrollSpeed, ((Screen.height - mousePosition.y) / mouseMovementBorderSize));;

			}

			camMovement *= Time.deltaTime;
			cam.transform.Translate (camMovement.x, camMovement.y, 0);

		}

		private void HandleScrolling () {

			float scroll = Input.GetAxis("Mouse ScrollWheel");

			if (scroll < 0 && cam.orthographicSize < maxOrthographicSize) {
				cam.orthographicSize -= scroll;
			}

			if (scroll > 0 && cam.orthographicSize > minOrthographicSize) {
				cam.orthographicSize -= scroll;
			}

		}

		Vector2 lastCoordinate;
		int lastBrushValue;
		private void HandleMouseInput () {

			OnMouseMove ();

			if (Input.GetMouseButtonDown (0)) {
				OnMouseDown();

			} else if (Input.GetMouseButton(0) && currentTool == Tool.BRUSH && selectedObject == null) {
				shouldGenerateMesh = true;
				//get the mouse position in world coordinates
				Vector2 tilePosition = map.WorldToTilePosition (cam.ScreenToWorldPoint(Input.mousePosition));

				//check if we are on the same tile as last time
				if (lastCoordinate == new Vector2((int)tilePosition.x, (int)tilePosition.y)) return;

				//give the current tile a new value
				moves.Push(map.GetAllTiles());
				Paint(tilePosition, lastBrushValue);

			} else if  (Input.GetMouseButtonUp (0)) {
				OnMouseUp ();

			}

		}
			
		Vector2 lastClickPosition = Vector2.zero;
		Vector2 tilePosition = Vector2.zero;
		Vector2 worldTilePosition = Vector2.zero;
		private void OnMouseDown() {

			RaycastHit hit;
			if (selectedObject != null) {

				selectedObject = null;
				return;

			} else if (Physics.Raycast((Vector3) worldTilePosition - Vector3.forward, Vector3.forward, out hit, 2f, levelEditorObjects)) {

				selectedObject = hit.collider.gameObject;
				lastBrushValue = -1;
				return;

			}

			switch(currentTool) {
			case Tool.BRUSH:
				Paint(tilePosition, currentBrushValue, true);
				break;
			case Tool.FILL:
				FloodFill(tilePosition, currentBrushValue);
				break;
			case Tool.LINE:
				LineFill(lastClickPosition, tilePosition, currentBrushValue);
				break;
			default:
				break;
			}

			shouldGenerateMesh = true;
			moves.Push(map.GetAllTiles());

		}

		private void OnMouseMove() {

			//get the mouse position in world coordinates
			tilePosition = map.WorldToTilePosition(cam.ScreenToWorldPoint(Input.mousePosition));
			worldTilePosition = new Vector2 (tilePosition.x * tileSize.x - mapOffsetX, tilePosition.y * tileSize.y - mapOffsetY);

			if (selectedObject != null)
				selectedObject.transform.position = new Vector3 (worldTilePosition.x, worldTilePosition.y, 0);

			foreach (IMouseMoveListener listener in mouseMoveListeners) {
				listener.OnMouseMoved (worldTilePosition);
			}

			
		}

		private void OnMouseUp () {

			selectedObject = null;

		}

		//rename this
		private void Paint (Vector2 tilePosition, int brushValue, bool onDown = false) {

			//the last tile coordinate clicked on by the player is updated
			lastCoordinate = new Vector2((int)tilePosition.x, (int)tilePosition.y);

			//check if the current tile is not equal to the current brush value
			if (map.GetTile ((int)tilePosition.x, (int)tilePosition.y) != brushValue) {

				//set it equal to the current brush value
				map.SetTile ((int)tilePosition.x, (int)tilePosition.y, brushValue);

				//mirror logic
				if (mirrorX) map.SetTile((int)((map.width - 1) - tilePosition.x), (int)tilePosition.y, brushValue);
				if (mirrorY) map.SetTile((int)(tilePosition.x), (int)((map.height - 1) - tilePosition.y), brushValue);
				if (mirrorX && mirrorY) map.SetTile((int)((map.width - 1) - tilePosition.x), (int)((map.height - 1) - tilePosition.y), brushValue);

				if (onDown) 
					lastBrushValue = currentBrushValue;

			}

			else {

				if (onDown) {

					//if it is equal to the current brush value then make it an empty tile
					map.SetTile ((int)tilePosition.x, (int)tilePosition.y, -1);

					//mirror logic
					if (mirrorX) map.SetTile((int)((map.width - 1) - tilePosition.x), (int)tilePosition.y, -1);
					if (mirrorY) map.SetTile((int)(tilePosition.x), (int)((map.height - 1) - tilePosition.y), brushValue);
					if (mirrorX && mirrorY) map.SetTile((int)((map.width - 1) - tilePosition.x), (int)((map.height - 1) - tilePosition.y), brushValue);

					lastBrushValue = -1;

				}

			}

			if (brushValue >= 0)
				lastClickPosition = tilePosition;

		}
			
		private void LineFill(Vector2 _startTilePosition, Vector2 _currentTilePosition, int brushValue) {

			int xDistance = (int)_currentTilePosition.x - (int)_startTilePosition.x;
			int yDistance = (int)_currentTilePosition.y - (int)_startTilePosition.y;

			Vector2 tileCoord;

			if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance)) {
				//draw horizontal line
				for (int i = 0, l = Mathf.Abs(xDistance); i <= l ; i++) {

					tileCoord = new Vector2((int)_startTilePosition.x + i * Mathf.Sign(xDistance), (int)_startTilePosition.y);
					Paint(tileCoord, currentBrushValue);

				}

			} else {
				//draw vertical line
				for (int i = 0, l = Mathf.Abs(yDistance); i <= l ; i++) {

					tileCoord = new Vector2((int)_startTilePosition.x, (int)_startTilePosition.y + i * Mathf.Sign(yDistance));
					Paint(tileCoord, currentBrushValue);

				}
			}

		}

		private void FloodFill(Vector2 tilePosition, int brushValue) {

			var q = new Queue<Vector2>(mapSize.x * mapSize.y);
			q.Enqueue(tilePosition);
			Paint(tilePosition, brushValue);

			int iterations = 0;

			while (q.Count > 0) {

				Vector2 point = q.Dequeue();

				for (int i = 0; i < 9; i++) {

					if (i % 2 == 0) continue;

					int xOffset = (i % 3) - 1;
					int yOffset = (i / 3) - 1;

					Vector2 newPoint = new Vector2(point.x + xOffset, point.y + yOffset);

					if(CheckFillValidity(newPoint, brushValue)) {
						q.Enqueue(newPoint);
						Paint(newPoint, brushValue);
					}

				}

				iterations++;
			}

		}

		private bool CheckFillValidity(Vector2 point, int brushValue) {

			if (!map.IsInBounds ((int)point.x, (int)point.y)) return false;

			int destinationTile = map.GetTile((int)point.x, (int)point.y);
			return brushValue != destinationTile;

		}

		private void Drag() {

		}

		bool shouldUndo = false;
		private void UndoLastAction() {


			if (moves.Count > 0) {
				moves.Pop();
				moves.Pop();

				map.SetAllTiles(moves.Peek());
			}

			shouldUndo = false;
			shouldGenerateMesh = true;

		}

		public void RequestUndo () {

			shouldUndo = true;

		}

		public void CreateNewLevel () {

			//initialize the map
			map = new TileGrid (mapSize, tileSize);
			moves = new Stack<int[]>();

			DestroySpawnPoints ();
			InitializeSpawnPoints ();
			InitializeLevelBounds ();

			LoadEnvironment (environment);
			shouldGenerateMesh = true;

			ToggleObjects (isShowingObjects);

		}

		public void SaveLevel () {

			LevelData saveData = map.GetSaveData ();

			saveData.minX = levelBounds[0].transform.position.x; 
			saveData.maxX = levelBounds[1].transform.position.x;
			saveData.minY = levelBounds[2].transform.position.y;
			saveData.maxY = levelBounds[3].transform.position.y;

			saveData.spawnPositions = new FixedVector2[numPlayers];

			for (int i = 0; i < numPlayers; i++) {
				saveData.spawnPositions[i] = new FixedVector2 (spawnPoints[i].transform.position);
			}

			saveData.name = levelName;
							
			SaveLoad.Save (saveData);
			Debug.Log ("Saved Successfully");


//			//check if the outer file exists
//			//if not create it
//			if ( !Directory.Exists("Assets/CustomLevels") ) {
//				AssetDatabase.CreateFolder("Assets", "CustomLevels");
//			}
//
//			//the number of levels the player has created up till now
//			int levelNumber = 1;
//
//			//while the directory exists, increment the level number
//			//to ensure levels are not overwritten
//			while (Directory.Exists("Assets/CustomLevels/Level_" + levelNumber)) levelNumber++;
//
//			//create a folder for the new level
//			AssetDatabase.CreateFolder("Assets/CustomLevels", "Level_" + levelNumber);
//
//			//create a folder for the new Mesh
//			AssetDatabase.CreateFolder("Assets/CustomLevels/Level_" + levelNumber, "Mesh");	
//
//			//save the mesh
//			AssetDatabase.CreateAsset(mesh, "Assets/CustomLevels/Level_" + levelNumber + "/Mesh/Mesh.asset");
//			AssetDatabase.SaveAssets();
//
//			//create and save the prefab
//			var emptyPrefab = PrefabUtility.CreateEmptyPrefab("Assets/CustomLevels/Level_" + levelNumber + "/Level_" + levelNumber + "_Prefab.prefab");
//			PrefabUtility.ReplacePrefab(transform.GetChild (0).gameObject, emptyPrefab);

		}

		public void LoadLevel () {

			LevelData saveData = SaveLoad.LoadAll <LevelData> (LevelData.DIRECTORY, LevelData.EXTENSION)[0];
			map = new TileGrid (saveData);

			SetLevelBounds (saveData.minX, saveData.maxX, saveData.minY, saveData.maxY);
			SetSpawnPoints (saveData.spawnPositions);
			Debug.Log ("Loaded Successfully");

			shouldGenerateMesh = true;

		}

		public void ToggleEditMode () {

			if (isInEditMode) 
				EnterPlayTestMode ();
			else
				EnterEditMode ();

			foreach (IModeChangeListener listener in modeListeners) {

				listener.OnModeChanged (isInEditMode);

			}

		}

		private void EnterEditMode () {

			GameController.DestroyAllGameEntities ();
			Destroy (skybox);

			orthoCam.SetActive (true);
			cameraHolder.SetActive (false);

			isInEditMode = true;

		}

		private void EnterPlayTestMode () {

			skybox = Instantiate <GameObject> (Assets.Get (environment).skybox);

			LogicEntity e = Contexts.sharedInstance.logic.CreateEntity ();
			e.AddCollider (new Determinism.RectilinearCollider (LevelBuilder.GenerateDeterministicCollider (map)));
			e.collider.value.mask = Mask.DEFAULT;
			e.collider.value.tag = Tag.DEFAULT;

			FixedVector2[] spawnPositions = new FixedVector2[numPlayers];

			for (int i = 0; i < numPlayers; i++) {
				spawnPositions[i] = new FixedVector2 (spawnPoints[i].transform.position);
			}

			Contexts.sharedInstance.logic.ReplaceSpawnPoints(spawnPositions);

			orthoCam.SetActive (false);
			cameraHolder.SetActive (true);

			isInEditMode = false;

		}


		void OnDrawGizmos () {

			//if (!EditorApplication.isPlaying) return;
			if (!isInEditMode) return;

			switch(currentTool) {
			case Tool.BRUSH:
				Gizmos.color = Color.black;
				break;
			case Tool.FILL:
				Gizmos.color = Color.red;
				break;
			case Tool.LINE:
				Gizmos.color = Color.blue;
				break;
			default:
				Gizmos.color = Color.black;
				break;
			}

			for (int y = 0; y < map.height; y++) {
				for (int x = 0; x < map.width; x++) {

					Gizmos.DrawWireCube(new Vector3(x * tileSize.x - mapOffsetX, y * tileSize.y - mapOffsetY, 0), new Vector3(tileSize.x, tileSize.y, 1));

				}

			}

			Vector2 mousePosition = map.WorldToTilePosition(cam.ScreenToWorldPoint(Input.mousePosition));

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(new Vector3(mousePosition.x * tileSize.x - mapOffsetX, mousePosition.y * tileSize.y - mapOffsetY, 0), new Vector3(tileSize.x, tileSize.y, 1));

			Gizmos.color = Color.cyan;
			if (mirrorX)
				Gizmos.DrawCube(Vector3.zero, new Vector3(0.2f, (mapSize.y * tileSize.y) + tileSize.y * 2, 0.2f));
			if (mirrorY)
				Gizmos.DrawCube(Vector3.zero, new Vector3((mapSize.x * tileSize.x) + tileSize.x * 2, 0.2f, 0.2f));


		}

	}

}

