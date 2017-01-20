using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Empty, Path, Swamp, Wall };

public class TileProperties : MonoBehaviour {

	TileType thisTileType;

	public void ChangeTileType (TileType newType) {
		thisTileType = newType;
	}

	public TileType GetTileType () {
		return thisTileType;
	}
}
