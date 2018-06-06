using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseLayerGenerator : ILayerGenerator {

	private int xSize, ySize, seed, frequency;
	private float samplingScale;

	public PerlinNoiseLayerGenerator (int xSize, int ySize, int frequency, float samplingScale, int seed){
		this.xSize = xSize;
		this.ySize = ySize;
		this.frequency = frequency;
		this.samplingScale = samplingScale;
		this.seed = seed;
	}
		
	public HeightmapLayer generateLayer(int weight){
		Random.InitState (seed);
			float[,] matrix = new float[xSize, ySize];
			float x0 = Random.value*1000;
			float y0 = Random.value*1000;
			for (int y = 0; y < ySize; y+=frequency) {
				for (int x = 0; x < xSize; x+=frequency) {
					float noise = Mathf.PerlinNoise (
						(x0 + x * samplingScale),
						(y0 + y * samplingScale)
					);
					for (int yy = 0; yy < frequency; yy++) {
						for (int xx = 0; xx < frequency; xx++) {
							matrix [x + xx, y + yy] = noise;
						}
					}
				}
			}
		HeightmapLayer l = new HeightmapLayer (xSize, ySize, matrix, weight);
		return l;
		}


		

}
