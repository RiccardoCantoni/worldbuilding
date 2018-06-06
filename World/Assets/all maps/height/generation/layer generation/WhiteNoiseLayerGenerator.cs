using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoiseLayerGenerator : ILayerGenerator {

	private int seed, xSize, ySize;
	public int frequency{ get; set;}

	public WhiteNoiseLayerGenerator(int xSize, int ySize, int frequency, int seed){
		this.frequency = frequency;
		this.xSize = xSize;
		this.ySize = ySize;
		this.seed = seed;
	}

	public void setFrequency(int frequency){
		this.frequency = frequency;
	}

	public HeightmapLayer generateLayer(int weight){
		float[,] matrix = new float[xSize, ySize];
		Random.InitState (seed);
		for (int y = 0; y < ySize; y+=frequency) {
			for (int x = 0; x < xSize; x+=frequency) {
				float noise = Random.value;
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
