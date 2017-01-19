using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {
	void Update () {
		transform.position += new Vector3((Input.GetKey("a") ? -1 : 0) + (Input.GetKey("d") ? 1 : 0), 0, (Input.GetKey("w") ? 1 : 0) + (Input.GetKey("s") ? -1 : 0)).normalized * Time.deltaTime * 10f;
	}	
}
