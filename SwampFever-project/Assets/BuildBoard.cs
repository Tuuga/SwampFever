using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBoard : MonoBehaviour {

	public Vector2 boardSize;
	public GameObject tilePrefab;
	List<TileProperties> tiles = new List<TileProperties>();

	void Start () {
		var tileParent = GameObject.Find("Tiles").transform;
		
		for(int x = 0; x < boardSize.x; x++) {
			for(int y = 0; y < boardSize.y; y++) {
				Vector3 pos = new Vector3(x, 0, y);
				var tileIns = (GameObject)Instantiate(tilePrefab, pos, tilePrefab.transform.rotation);
				tileIns.transform.parent = tileParent;
				tiles.Add(tileIns.GetComponent<TileProperties>());
			}
		}
	}

	public List<TileProperties> GetTilesList () {
		return tiles;
	}
}
