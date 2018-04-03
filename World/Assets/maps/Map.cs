using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map<T> {
	public T[,] grid;
	public int xSize;
	public int ySize;

	public bool isInMap(int x, int y){
		return (x >= 0 && x < xSize && y >= 0 && y < ySize);
	}
}
