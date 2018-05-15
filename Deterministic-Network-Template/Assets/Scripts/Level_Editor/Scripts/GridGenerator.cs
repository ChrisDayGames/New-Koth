using UnityEngine;
using LevelEditor;

public class GridGenerator : MonoBehaviour {

	public GameObject gridTile;

	public void Start () {

		GenerateGrid (MapEditor.instance.map);
	}

	public void GenerateGrid (TileGrid map) {

		for (int y = 0; y < map.height; y++) {

			for (int x = 0; x < map.width; x++) {

				GameObject go = Instantiate (gridTile);
				go.transform.position = map.TileToWorldPosition (new Vector2 (x, y));
				go.transform.localScale = new Vector3 (map.tileWidth, map.tileHeight, 1);

				go.transform.parent = this.transform;

			}

		}

	}

}
