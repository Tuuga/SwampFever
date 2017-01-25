using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Empty, Path, Swamp, Wall };

public class TileProperties : MonoBehaviour {

	public TileType thisTileType; // Public for debug
	public int index; // Public for debug

	public void ChangeTileType (TileType newType) { thisTileType = newType;	}
	public TileType GetTileType () { return thisTileType; }

	public int GetIndex() { return index; }
	public void SetIndex (int ind) { index = ind; }
}
