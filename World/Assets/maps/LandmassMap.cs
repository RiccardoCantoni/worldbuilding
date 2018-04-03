using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmassMap : Map<int> {

	public LandmassMap(TerrainMap tm){
		this.xSize = tm.xSize;
		this.ySize = tm.ySize;
		this.grid = new int[xSize, ySize];
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				if (tm.grid [x, y].terrainType == TerrainType.sea) {
					if (isCoastLine(x,y,tm)){
						this.grid [x, y] = 1;
					}
				}
			}
		}
	}

	bool isCoastLine(int x, int y, TerrainMap tm){
		if (tm.grid [Mathf.Max (0, x - 1), y].terrainType != TerrainType.sea)
			return true;
		if (tm.grid [Mathf.Min (x + 1, xSize-1), y].terrainType != TerrainType.sea)
			return true;
		if (tm.grid [x, Mathf.Max (0, y - 1)].terrainType != TerrainType.sea)
			return true;
		if (tm.grid [x, Mathf.Min (y + 1, ySize-1)].terrainType != TerrainType.sea)
			return true;
		return false;
	}

}
