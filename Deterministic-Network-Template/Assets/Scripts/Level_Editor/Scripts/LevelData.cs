using Determinism;

[System.Serializable]
public class LevelData : ISaveable{

	public const string DIRECTORY = "/Resources/Levels";

	public string name {get; private set;}
	public string path {get { return DIRECTORY + name; }}

	//environment type --> might not be ideal what if just want to create a mesh
	public Environments environment;

	//level size
	public int width;
	public int height;

	//tile size
	public float tileWidth;
	public float tileHeight;

	//pixel size
	public int pixelsPerTile;

	//camera bound
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	//spawn points
	public FixedVector2[] spawnPositions;

	//the map data
	public int [] tiles;

	public LevelData () {

		this.name = "";
		this.width = 10;
		this.height = 10;
		this.tileWidth = 1;
		this.tileHeight = 1;
		this.tiles = new int[width * height];
		this.pixelsPerTile = 16;
		this.environment = Environments.GRASS;

	}

	public LevelData (int _width, int _height, float _tileWidth, float _tileHeight, int _pixelsPerTile, Environments _environment, int[] _tiles) {

		this.width = _width;
		this.height = _height;
		this.tileWidth = _tileWidth;
		this.tileHeight = _tileHeight;

		this.pixelsPerTile = _pixelsPerTile;
		this.environment = _environment;

		this.tiles = new int[this.width * this.height];

		for (int i = 0; i < this.tiles.Length; i++) {
			this.tiles[i] = _tiles [i];
		}

		minX = 0;
		maxX = 0;
		minY = 0;
		maxY = 0;

		spawnPositions = new FixedVector2[0];

	}

}