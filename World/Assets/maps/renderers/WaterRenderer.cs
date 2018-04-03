using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRenderer : IMapRenderer {

	private Color ocean = Color.blue;
	private Color land = Color.red;
	private Color lake = Color.green; 
	private Color undecided = Color.yellow; 
	private Color unknown = Color.black; 

	public TerrainMap m;

	public Color[] getColors(){
		Color[] pixels = new Color[m.xSize * m.ySize];
		for (int x=0; x<m.xSize; x++){
			for (int y=0; y<m.ySize; y++){
				if (m.grid [x, y].terrainType == TerrainType.sea) {
					pixels [y * m.xSize + x] = ocean;
				}
				else if (m.grid [x, y].terrainType == TerrainType.land) {
					pixels [y * m.xSize + x] = land;
				}
				else if (m.grid [x, y].terrainType == TerrainType.lake) {
					pixels [y * m.xSize + x] = lake;
				}
				else if (m.grid [x, y].terrainType == TerrainType.undecided) {
					pixels [y * m.xSize + x] = undecided;
				}
			}
		}
		return pixels;
	}


}
