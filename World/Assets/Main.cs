using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public int xSize, ySize;

	public GameObject mapObject;

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Map map = mapObject.GetComponent<Map> ();
			Noise gen = new Noise ();
			map.newMap(xSize, ySize);
			float [,] heightmap = gen.generateMap(xSize, ySize);
			map.heightMap = heightmap;
			map.updateMap ();
		}
	}

}
