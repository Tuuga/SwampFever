using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacing : MonoBehaviour {

	public Material empty, path, swamp, wall;
	public LayerMask mask;

	List<TileProperties> tiles;
	TileType typeInUse = TileType.Empty;

	void Start () {
		var foundTiles = FindObjectsOfType<TileProperties>();
		tiles = new List<TileProperties>(foundTiles);
		for (int i = 0; i < foundTiles.Length; i++) {
			tiles[foundTiles[i].GetIndex()] = foundTiles[i];
		}
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			ChangeTile(typeInUse);
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			typeInUse = TileType.Empty;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			typeInUse = TileType.Path;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			typeInUse = TileType.Swamp;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			typeInUse = TileType.Wall;
		}
	}

	void ChangeTile (TileType type) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) {
			var tile = hit.transform.GetComponentInParent<TileProperties>();
			if (tile.GetTileType() == TileType.Empty || type == TileType.Empty) {
				if (!CheckAdjacentTiles(tile)) { print("can't"); return; }
				tile.ChangeTileType(type);
				ChangeTileColor(tile);
			}
		}
	}

	void ChangeTileColor (TileProperties tile) {
		var meshRenderer = tile.GetComponentInChildren<MeshRenderer>();
		var type = tile.GetTileType();

		if (type == TileType.Empty) {
			meshRenderer.material = empty;
		} else if (type == TileType.Path) {
			meshRenderer.material = path;
		} else if (type == TileType.Swamp) {
			meshRenderer.material = swamp;
		} else {
			meshRenderer.material = wall;
		}
	}

	bool CheckAdjacentTiles (TileProperties tile) {
		int index = tiles.IndexOf(tile);
		List<TileProperties> adjacentTiles = new List<TileProperties>();

		if (index + 1 < tiles.Count && Mathf.Abs(index % 10 - (index + 1) % 10) == 1) {
			adjacentTiles.Add(tiles[index + 1]);
		}
		if (index - 1 >= 0 && Mathf.Abs(index % 10 - (index - 1) % 10) == 1) {
			adjacentTiles.Add(tiles[index - 1]);
		}
		if (index + 10 < tiles.Count) {
			adjacentTiles.Add(tiles[index + 10]);
		}
		if (index - 10 >= 0) {
			adjacentTiles.Add(tiles[index - 10]);
		}

		foreach (TileProperties t in adjacentTiles) {
			if (t.GetTileType() != TileType.Empty) {
				return true;
			}
		}

		return false;
	}
}
