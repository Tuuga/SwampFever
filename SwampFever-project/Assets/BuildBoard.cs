using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildBoard : EditorWindow {

	public Vector2 boardSize;
	public GameObject tilePrefab;
	List<TileProperties> tiles = new List<TileProperties>();

	Material empty, path, swamp, wall;
	TileType typeToChangeTo;


	[MenuItem("Window/BuildBoard")]
	static void Init () {
		BuildBoard window = (BuildBoard)EditorWindow.GetWindow(typeof(BuildBoard));
		window.Show();
	}

	void OnGUI () {
		boardSize = EditorGUILayout.Vector2Field("Board Size", boardSize);
		tilePrefab = (GameObject)EditorGUILayout.ObjectField(tilePrefab, typeof(GameObject), false);

		if (GUILayout.Button("Build Board")) {
			var tileParent = GameObject.Find("Tiles").transform;

			foreach (TileProperties tp in FindObjectsOfType<TileProperties>()) {
					DestroyImmediate(tp.gameObject);
			}

			tiles = new List<TileProperties>();

			for (int x = 0; x < boardSize.x; x++) {
				for (int y = 0; y < boardSize.y; y++) {
					Vector3 pos = new Vector3(x, 0, y);
					var tileIns = (GameObject)Instantiate(tilePrefab, pos, tilePrefab.transform.rotation);
					tileIns.transform.parent = tileParent;
					var tp = tileIns.GetComponent<TileProperties>();
					tp.SetIndex(x * (int)boardSize.y + y);
					tiles.Add(tp);
				}
			}
		}

		EditorGUILayout.BeginHorizontal();
		empty = (Material)EditorGUILayout.ObjectField(empty, typeof(Material), false);
		path = (Material)EditorGUILayout.ObjectField(path, typeof(Material), false);
		swamp = (Material)EditorGUILayout.ObjectField(swamp, typeof(Material), false);
		wall = (Material)EditorGUILayout.ObjectField(wall, typeof(Material), false);
		EditorGUILayout.EndHorizontal();

		typeToChangeTo = (TileType)EditorGUILayout.EnumPopup("TileType", typeToChangeTo);
		if (GUILayout.Button("Change Types")) {
			foreach (GameObject g in Selection.gameObjects) {
				var tp = g.GetComponentInParent<TileProperties>();
				tp.ChangeTileType(typeToChangeTo);
				ChangeMaterial(tp);
			}
		}
	}

	void ChangeMaterial (TileProperties tile) {
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
}
