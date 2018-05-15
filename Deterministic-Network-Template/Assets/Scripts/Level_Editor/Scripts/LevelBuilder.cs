using UnityEngine;
using LevelEditor;
using System.Collections.Generic;
using Determinism;

public static class LevelBuilder {

	public static GameObject GenerateGameObject (LevelData saveData) {

		TileGrid map = new TileGrid (saveData);
		LevelBlueprint l = Assets.Get (saveData.environment);
		Vector2Int textureSize = new Vector2Int (
			l.texture.width / saveData.pixelsPerTile,
			l.texture.height / saveData.pixelsPerTile
		);

		Mesh mesh = GenerateMesh (map, textureSize);
		DynamicMesh dm = new GameObject ().AddComponent <DynamicMesh> ();
		dm.CreateMesh (mesh, l.texture, l.mat);

		return dm.gameObject;

	}

	public static GameObject GenerateSkyBox (Environments e) {

		return Assets.Get (e).skybox;

	}

	public static Mesh GenerateMesh (TileGrid map, Vector2Int textureSize) {

		//create new mesh
		Mesh mesh = new Mesh ();

		//reset the verteces, uvs and tileCoords to an empty array
		Vector3[] vertices = new Vector3[(int)(map.width * map.height * 4)];
		Vector2[] uv = new Vector2 [vertices.Length];
		Vector2[] tileCoords = new Vector2[map.width * map.height];

		//populate the tile coords array
		DetermineTileCoords (map, ref tileCoords);

		//create a list for keeping track of corners
		List <Mesh> cornerMeshes = new List <Mesh> ();

		//iterate through the y values
		for (int i = 0, y = 0; y < map.height; y++) {

			//iterate through the x values
			for (int x = 0; x < map.width; x++) {

				//check if the tile is "lit"
				if (map.TileIsLit (x, y)) {
					
					float worldTileCoordX = x * map.tileWidth - map.offsetX; 
					float worldTileCoordY = y * map.tileHeight - map.offsetY;

					//add the four corners of the tilemap to the vertices array
					for (int c = 0; c < 4; c++, i++) {

						vertices [i] = new Vector3 (

							-map.tileWidth/2 + (worldTileCoordX + (c % 2) * map.tileWidth),
							-map.tileHeight/2 + (worldTileCoordY + (c / 2) * map.tileHeight),
							0

						);

						uv [i] = new Vector2 ((tileCoords [x + y * map.width].x + (c % 2)) / textureSize.x, ((textureSize.y-1) - tileCoords [x + y * map.width].y + (c / 2)) / textureSize.y);

					}

					if (map.TileIsLit (x + 1, y) && map.TileIsLit (x, y - 1) && !map.TileIsLit (x + 1, y - 1))
						cornerMeshes.Add (CreateCornerMesh (x, y, 5, 2, map, textureSize));

					if (map.TileIsLit (x + 1, y) && map.TileIsLit (x, y + 1) && !map.TileIsLit (x + 1, y + 1))
						cornerMeshes.Add (CreateCornerMesh (x, y, 5, 3, map, textureSize));

					if (map.TileIsLit (x - 1, y) && map.TileIsLit (x, y - 1) && !map.TileIsLit (x - 1, y - 1))
						cornerMeshes.Add (CreateCornerMesh (x, y, 5, 0, map, textureSize));

					if (map.TileIsLit (x - 1, y) && map.TileIsLit (x, y + 1) && !map.TileIsLit (x - 1, y + 1))
						cornerMeshes.Add (CreateCornerMesh (x, y, 5, 1, map, textureSize));

				}

			}

		}

		int[] triangles = new int [(int)(map.width * map.height * 6)];

		for (int ti = 0, vi = 0, y = 0; y < map.height ; y++) {

			for (int  x = 0; x < map.width; x++, ti += 6, vi += 4) {

				triangles [ti] = triangles [ti + 3] = vi;
				triangles [ti + 2] = vi + 1;
				triangles [ti + 1] = triangles [ti + 5] = vi + 3;
				triangles [ti + 4] = vi + 2;

			}

		}

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();

		return GenerateCombinedMesh (mesh, cornerMeshes);

	}

	public static Mesh GenerateCollisionMesh (TileGrid map) {

		Vector3[] vertices = new Vector3[(int)(map.width * map.height * 4)];

		//iterate through the y values
		for (int i = 0, y = 0; y < map.height; y++) {

			//iterate through the x values
			for (int x = 0; x < map.width; x++) {

				//check if the tile is "lit"
				if (map.GetTile (x, y) >= 0) {

					//add the four corners of the tilemap to the vertices array
					for (int c = 0; c < 4; c++, i++) {

						float worldTileCoordX = x * map.tileWidth - map.offsetX; 
						float worldTileCoordY = y * map.tileHeight - map.offsetY;

						vertices [i] = new Vector3 (

							-map.tileWidth/2 + (worldTileCoordX + (c % 2) * map.tileWidth),
							-map.tileHeight/2 + (worldTileCoordY + (c / 2) * map.tileHeight) + (!(map.TileIsLit (x, y + 1)) ? (-0.3f * ((c /2))) : 0),
							0

						);

					}

				}

			}

		}

		int[] triangles = new int [(int)(map.width * map.height * 6)];

		for (int ti = 0, vi = 0, y = 0; y < map.height ; y++) {

			for (int  x = 0; x < map.width; x++, ti += 6, vi += 4) {

				triangles [ti] = triangles [ti + 3] = vi;
				triangles [ti + 2] = vi + 1;
				triangles [ti + 1] = triangles [ti + 5] = vi + 3;
				triangles [ti + 4] = vi + 2;

			}

		}

		Mesh collisionMesh = new Mesh ();

		collisionMesh.vertices = vertices;
		collisionMesh.triangles = triangles;
		collisionMesh.RecalculateNormals ();

		return collisionMesh;

	}

	public static FixedLine2[] GenerateDeterministicCollider (TileGrid map) {

		Mesh collisionMesh = GenerateCollisionMesh (map);
		List<FixedVector2> points = new List <FixedVector2> ();

		for (int i = 0; i < collisionMesh.vertices.Length; i++) {
			points.Add (new FixedVector2 (collisionMesh.vertices [i]));
		}

		return RectilinearDecomposer.BreakIntoEdges (points);

	}

	private static void DetermineTileCoords (TileGrid map, ref Vector2[] tileCoords) {

		//iterate through the y values
		for (int y = 0; y < map.height; y++) {

			//iterate through the x values
			for (int x = 0; x < map.width; x++) {

				tileCoords [x + y * map.width] = DetermineTileCoords (x, y, map);

			}

		}

	}

	//rename this function
	private static Vector2 DetermineTileCoords (int x, int y, TileGrid map) {

		Vector2 coord = Vector2.zero;

		bool up = map.TileIsLit (x, y + 1);
		bool down = map.TileIsLit (x, y - 1);
		bool left = map.TileIsLit (x - 1, y);
		bool right = map.TileIsLit (x + 1, y);

		if (left && right)
			coord.x = 1;
		else if (left)
			coord.x = 2;
		else if (right)
			coord.x = 0;
		else
			coord.x = 3;

		if (up && down)
			coord.y = 1;
		else if (up)
			coord.y = 2;
		else if (down)
			coord.y = 0;	
		else 
			coord.y = 3;

		return coord;

	}

	private static Mesh CreateCornerMesh (int x, int y, int tileCoordX, int tileCoordY, TileGrid map, Vector2Int textureSize) {

		Mesh cornerMesh = new Mesh ();
		Vector3[] v = new Vector3[4];
		Vector2[] uvs = new Vector2 [v.Length];

		float worldTileCoordX = x * map.tileWidth - map.offsetX; 
		float worldTileCoordY = y * map.tileHeight - map.offsetY;

		//add the four corners of the tilemap to the vertices array
		for (int c = 0, i = 0; c < 4; c++, i++) {

			v [i] = new Vector3 (

				-map.tileWidth/2 + (worldTileCoordX + (c % 2) * map.tileWidth),
				-map.tileHeight/2 + (worldTileCoordY + (c / 2) * map.tileHeight),
				-0.0001f

			);

			uvs [i] = new Vector2 (((float)tileCoordX + (c % 2)) / textureSize.x, ((textureSize.y-1) - (float)tileCoordY + (c / 2)) / textureSize.y);

		}

		int[] t = new int [6];

		t [0] = t [0 + 3] = 0;
		t [0 + 2] = 0 + 1;
		t [0 + 1] = t [0 + 5] = 0 + 3;
		t [0 + 4] = 0 + 2;

		cornerMesh.vertices = v;
		cornerMesh.uv = uvs;
		cornerMesh.triangles = t;
		cornerMesh.RecalculateNormals ();

		return cornerMesh;

	}

	private static Mesh GenerateCombinedMesh (Mesh mesh, List <Mesh> meshes) {

		Mesh combinedMesh = new Mesh ();

		GameObject go = new GameObject ("cornersMesh");
		MeshFilter filter = go.AddComponent <MeshFilter> ();

		CombineInstance[] combine = new CombineInstance[meshes.Count + 1];
		combine[combine.Length - 1].mesh = mesh;
		combine[combine.Length - 1].transform = filter.transform.localToWorldMatrix;

		int i = 0;
		while (i < meshes.Count) {

			filter.mesh = meshes[i];
			combine[i].mesh = meshes[i];
			combine[i].transform = filter.transform.localToWorldMatrix;
			i++;

		}

		combinedMesh.CombineMeshes (combine);

		GameObject.Destroy (go);

		return combinedMesh;

	}

}
