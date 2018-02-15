using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapGenerator {

	public virtual float[,] generateMap(int x, int y){
		Debug.Log ("subclass needed");
		return new float[1,1];
	}
}
