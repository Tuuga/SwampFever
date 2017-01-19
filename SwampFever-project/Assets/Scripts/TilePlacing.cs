using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacing : MonoBehaviour {

	public GameObject pathPrefab;
	public GameObject swampPrefab;
	public GameObject wallPrefab;

	public LayerMask mask;
	public Vector2 boardSize;

	List<GameObject> tiles;
	GameObject tileTypeInUse;
	Transform tilesParent;

	void Start () {
		tileTypeInUse = pathPrefab;     // Default
		tilesParent = GameObject.Find("Tiles").transform;
		tiles = new List<GameObject>(new GameObject[(int)(boardSize.x * boardSize.y)]);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			SpawnTile(tileTypeInUse);
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			tileTypeInUse = pathPrefab;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			tileTypeInUse = swampPrefab;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			tileTypeInUse = wallPrefab;
		}
	}

	Vector3 RoundPos (Vector3 pos) {
		var newPos = pos;
		newPos.x = Mathf.Round(pos.x);
		newPos.y = 0.05f;			// MAGIC NUBER
		newPos.z = Mathf.Round(pos.z);
		return newPos;
	}

	// Messy function
	// Spawn a tile at the rounded mouse position
	void SpawnTile (GameObject tile) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) {
			var roundedHitPos = RoundPos(hit.point);
			var index = (int)(roundedHitPos.x * (boardSize.y + roundedHitPos.y) + roundedHitPos.z);
			if (tiles[index] != null) {
				print(index + " index already has a tile");
				return;
			}
			var tileIns = (GameObject)Instantiate(tile, roundedHitPos, tile.transform.rotation);
			tileIns.transform.parent = tilesParent;
			tiles[index] = tileIns;
		}
	}
}
