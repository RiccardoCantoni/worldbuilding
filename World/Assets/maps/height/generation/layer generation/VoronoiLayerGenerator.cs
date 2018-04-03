using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiLayerGenerator : ILayerGenerator {

	private int numPoints, seed, xSize, ySize;
	private float samplingScale;

	public VoronoiLayerGenerator(int xSize, int ySize, int numberOfPoints, float samplingScale, int seed){
		this.ySize = ySize;
		this.xSize = xSize;
		this.seed = seed;
		this.numPoints = numberOfPoints;
		this.samplingScale = samplingScale;
	}

	public HeightmapLayer generateLayer(int weight){
		Random.InitState (seed);
		float x0 = Random.value*1000;
		float y0 = Random.value*1000;
		float[,] matrix = new float[xSize, ySize];
		Vector2[] points = new Vector2[numPoints];
		float[] values = new float[numPoints];
		for (int i = 0; i < numPoints; i++) {
			points [i] = new Vector2 (Random.Range (0, xSize - 1), Random.Range (0, ySize - 1));
			float v = Mathf.PerlinNoise (x0+points [i].x/128f*samplingScale, y0+points [i].y/128f*samplingScale);
			v = Mathf.Min (v, 1f);
			v = Mathf.Max (v, 0f);
			values [i] = v;
		}
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				float minD = xSize * ySize;
				int closest = -1;
				for (int i = 0; i < numPoints; i++) {
					float d = Vector2.Distance (points [i], new Vector2 (x,y));
					if (d <= minD) {
						closest = i;
						minD = d;
					}
				}
				matrix [x, y] = values [closest];
			}
		}
		HeightmapLayer l = new HeightmapLayer (xSize, ySize, matrix, weight);
		return l;
	}
		
}
