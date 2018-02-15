using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer {

	public virtual Color[] getColors (float[,] map, int xSize, int ySize){
		Debug.Log ("this requires derived class");
		return new Color[0];
	}

}
