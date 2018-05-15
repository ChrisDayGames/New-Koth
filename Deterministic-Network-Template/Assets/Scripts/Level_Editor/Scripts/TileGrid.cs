using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tiles {

	GRASS = 0,
	ICE = 1,

}

public class TileGrid {

	//the tiles in the level
	private int[] tiles;

	//the size of the level in tiles
	private Vector2Int mapSize;

	//the world size of each tile
	private Vector2 tileSize;

	//properties
	public int width {

		get {
			return mapSize.x;
		}

		set {
			mapSize.x = value;
		}

	}

	public int height {

		get {
			return mapSize.y;
		}

		set {
			mapSize.y = value;
		}

	}

	public float tileWidth {

		get {
			return tileSize.x;
		}

		set {
			tileSize.x = value;
		}

	}

	public float tileHeight {

		get {
			return tileSize.y;
		}

		set {
			tileSize.y = value;
		}

	}

	public float offsetX {

		get {
			return ((float) width * tileWidth) / 2f - tileWidth / 2f;
		}

	}

	public float offsetY {

		get {
			return ((float) height * tileHeight) / 2f - tileHeight / 2f;
		}

	}

	public int numTiles {

		get {
			return tiles.Length;
		}

	}

	//constructor with default tile size
	public TileGrid (int _width, int _height) {

		this.width = _width;
		this.height = _height;
		this.tileWidth = 1f;
		this.tileHeight = 1f;

		InitializeTileMap ();

	}

	//constructor with specified tile size
	public TileGrid (int _width, int _height, int _tileWidth, int _tileHeight) {

		this.width = _width;
		this.height = _height;
		this.tileWidth = _tileWidth;
		this.tileHeight = _tileHeight;

		InitializeTileMap ();

	}

	//constructor with specified tile size
	public TileGrid (Vector2Int _mapSize, Vector2 _tileSize) {

		this.mapSize = _mapSize;
		this.tileSize = _tileSize;

		InitializeTileMap ();

	}

	public TileGrid (LevelData data) {

		this.width = data.width;
		this.height = data.height;
		this.tileWidth = data.tileWidth;
		this.tileHeight = data.tileHeight;

		this.tiles = new int[this.width * this.height];

		for (int i = 0; i < this.tiles.Length; i++) {
			this.tiles[i] = data.tiles [i];
		}

	}

	//where to put this
	public LevelData GetSaveData () {

		return new LevelData (width, height, tileWidth, tileHeight, 16, Environments.GRASS, tiles);

	}

	public int[] GetAllTiles () {
		return (int[]) tiles.Clone ();
	}

	public void SetAllTiles (int[] newTiles) {
		tiles = newTiles;
	}

	public void InitializeTileMap () {

		tiles = new int [width * height];

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				if (IsOnBorder (x, y)) {
					tiles [x + y * width] = 0;
				} else {
					tiles [x + y * width] = -1;
				}
			}
		}

	}

	public bool IsInBounds (int x, int y) {
		return (x >= 0 && y >= 0 && x < width && y < height);
	}

	public bool IsOnBorder (int x, int y) {
		return (x == 0 || y == 0 || x == width-1 || y == height-1);
	}

	public int GetTile (int i) {

		if (0 <= i && i < tiles.Length)
			return tiles [i];

		return -1;

	}

	public int GetTile (int x, int y) {

		if (IsInBounds (x, y))
			return tiles [x + y * width];
		
		return -1;

	}

	public void SetTile (int i, int value) {

		if (0 <= i && i < tiles.Length)
			tiles [i] =  value;

	}

	public void SetTile (int x, int y, int value) {

		if (IsInBounds (x, y))
			tiles [x + y * width] = value;

	}

	public bool TileIsLit (int x, int y) {

		if (!IsInBounds (x, y)) return false;
		return tiles [x + y * width] >= 0;

	}

	public Vector3 TileToWorldPosition (Vector2 tilePosition) {

		float worldX = tilePosition.x * tileWidth - offsetX;
		float worldY = tilePosition.y * tileHeight - offsetY;

		return new Vector3 (worldX, worldY, 0);


	}

	public Vector2 WorldToTilePosition (Vector3 position) {

		int tilePositionX = (int) ((position.x + offsetX + tileWidth / 2f) / tileWidth);
		int tilePositionY = (int) ((position.y + offsetY + tileHeight / 2f) / tileHeight);

		return new Vector2 (tilePositionX, tilePositionY);

	}

	int GetTotalLitTiles (int tileMapIndex) {

		int litTiles = 0;

		for (int i = 0; i < numTiles; i++) {

			if (GetTile (i) == tileMapIndex)
				litTiles++;

		}

		return litTiles;

	}

}