using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapGenerator {

	private int seed;

	public Heightmap generateMap(int xSize, int ySize){
		Heightmap map = new Heightmap (xSize, ySize, 0.5f);
		seed = Random.Range(int.MinValue, int.MaxValue);
		//Debug.Log (seed);
		//seed = -1949366325;
		map = new Heightmap (xSize, ySize, 0.5f);
		LayerManager lm = new LayerManager (xSize, ySize, seed);
		map.addLayer(lm.voronoiLayer(180,1.5f,1.3f,16)); //1.5 1.3
		map.addLayer (lm.perlinLayer (1, 1.1f, 4));
		map.stretch (1.5f);
		map.smoothBetween (3, 6, 2, -1);
		map.addLayer (lm.perlinLayer (8, 1.1f, 4));
		map.addLayer(lm.whiteNoiseLayer(8,2));
		map.stretch (1.2f);
		map.smoothBetween (2, 4, 2, -1);
		map.addLayer (lm.whiteNoiseLayer (2, 1));
		map.addLayer (lm.whiteNoiseLayer (1, 1));
		map.addLayer (lm.whiteNoiseLayer (2,2), 0.4f, 0.6f);
		map.smoothBetween (1, 2, 2, -1);
		map.stretch (1.2f);
		map.smoothBetween (7, 4, 0.525f, 0.49f);
		map.smoothBetween (1, 1, 2, -1);
		return map;
	}

}
