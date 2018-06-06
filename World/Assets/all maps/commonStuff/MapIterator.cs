using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIterator<T> {
	
	T[,] grid;
	int curX, curY;
	int xSize, ySize;

	public MapIterator(Map <T> map){
		grid = map.grid;
		xSize = map.xSize;
		ySize = map.ySize;
		curX = 0;
		curY = 0;
	}

	public void set(int x, int y){
		curX = x;
		curY = y;
	}

	public T get(int x, int y){
		return grid [x, y];
	}

	public T getNext(Direction dir){ 
		curX += dir.dx;
		if (curX < 0)
			curX += xSize;
		if (curX >= xSize)
			curX -= xSize;
		curY += dir.dy;
		if (curY < 0)
			curY += ySize;
		if (curY >= ySize)
			curY -= ySize;
		return get (curX, curY);
	}

}
