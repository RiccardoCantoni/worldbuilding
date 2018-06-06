using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMap : Map<myTerrain> {

	public TerrainMap(Heightmap hm, WaterMap wm){
		this.xSize = hm.xSize;
		this.ySize = hm.ySize;
		this.grid = new myTerrain[xSize, ySize];
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				grid [x, y] = new myTerrain (hm.grid [x, y], wm.grid [x, y]);
			}
		}
	}
}
