using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiLayerGenerator : ILayerGenerator {

	private int numPoints, seed, xSize, ySize;
	private float samplingScale;

	int countall, countskip;

	public VoronoiLayerGenerator(int xSize, int ySize, int numberOfPoints, float samplingScale, int seed){
		this.ySize = ySize;
		this.xSize = xSize;
		this.seed = seed;
		this.numPoints = numberOfPoints;
		this.samplingScale = samplingScale;
		countall = 0;
		countskip = 0;
	}

	public HeightmapLayer generateLayer(int weight){
		Random.InitState (seed);
		float x0 = Random.value*10000;
		float y0 = Random.value*10000;
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
		bool checkAcross;
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				checkAcross = true;
				float minD = xSize * ySize;
				float edgeDistance = Mathf.Min (x, xSize - x);
				int closest = -1;
				for (int i = 0; i < numPoints; i++) {
					float d = wraparoundDistance(points [i], new Vector2 (x,y), xSize, checkAcross);
					if (d <= minD) {
						closest = i;
						minD = d;
					}
					if (d < edgeDistance) {
						checkAcross = false;
					}
				}
				matrix [x, y] = values [closest];
			}
		}
		HeightmapLayer l = new HeightmapLayer (xSize, ySize, matrix, weight);
		return l;
	}

	private float wraparoundDistance(Vector2 v1, Vector2 v2, int xSize, bool checkAcross){
		countall++;
		float direct = Vector2.Distance (v1, v2);
		if (!checkAcross) {
			countskip++;
			return direct;
		}
		float dRight = xSize - v1.x;
		float dLeft = v1.x;
		if (dRight <= dLeft) {
			return Mathf.Min (direct, Vector2.Distance (v1, new Vector2 (v2.x + xSize, v2.y)));
		} else {
			return Mathf.Min (direct, Vector2.Distance (v1, new Vector2 (v2.x - xSize, v2.y)));
		}
	}
		
}
