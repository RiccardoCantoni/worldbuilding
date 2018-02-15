using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : HeightMapGenerator{

	private int xSize;
	private int ySize;

	float[,] matrix;
	float totalWeight=0;

	public override float[,] generateMap(int x, int y){
		this.xSize = x;
		this.ySize = y;
		float[,] newLayer;
		matrix = new float[xSize, ySize];
		newLayer = getVoronoiLayer (80, 1.1f, 1.3f);
		addLayer (newLayer, 16);
		stretch (1.5f);
		smoothBetween (3, 6, 2, -1);
		newLayer = getRandomNoiseLayer (8);
		addLayer (newLayer, 4);
		smoothBetween (2, 4, 2, -1);
		newLayer = getRandomNoiseLayer (2);
		addLayer (newLayer, 1);
		newLayer = getRandomNoiseLayer (1);
		addLayer (newLayer, 1);
		smoothBetween (1, 2, 2, -1);
		stretch (1.2f);
		smoothBetween (7, 4, 0.525f, 0.49f);
		smoothBetween (1, 1, 2, -1);
		return matrix;
	}
		
	#region methods

	private float[,] getPerlinNoiseLayer(int amplitude, float samplingScale){
		float[,] layer = new float[xSize, ySize];
		float x0 = Random.value*1000;
		float y0 = Random.value*1000;
		for (int y = 0; y < ySize; y+=amplitude) {
			for (int x = 0; x < xSize; x+=amplitude) {
				float noise = Mathf.PerlinNoise (
					(x0 + x * samplingScale),
					(y0 + y * samplingScale)
				);
				for (int yy = 0; yy < amplitude; yy++) {
					for (int xx = 0; xx < amplitude; xx++) {
						layer [x + xx, y + yy] = noise;
					}
				}
			}
		}
		return layer;
	}

	private float[,] getRandomNoiseLayer(int amplitude){
		float[,] layer = new float[xSize, ySize];
		for (int y = 0; y < ySize; y+=amplitude) {
			for (int x = 0; x < xSize; x+=amplitude) {
				float noise = Random.value;
				for (int yy = 0; yy < amplitude; yy++) {
					for (int xx = 0; xx < amplitude; xx++) {
						layer [x + xx, y + yy] = noise;
					}
				}
			}
		}
		return layer;
	}
		
	private float[,] getVoronoiLayer(int numberOfPoints, float samplingScale, float heightFactor){
		float x0 = Random.value*1000;
		float y0 = Random.value*1000;
		float[,] layer = new float[xSize, ySize];
		Vector2[] points = new Vector2[numberOfPoints];
		float[] values = new float[numberOfPoints];
		for (int i = 0; i < numberOfPoints; i++) {
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
				for (int i = 0; i < numberOfPoints; i++) {
					float d = Vector2.Distance (points [i], new Vector2 (x,y));
					if (d <= minD) {
						closest = i;
						minD = d;
					}
				}
				layer [x, y] = values [closest];
			}
		}
		return layer;
	}

	private void addLayer(float[,] layer, float newWeight){
		float newWeightRatio;
		float oldWeight;
		if (totalWeight == 0) {
			oldWeight = 0;
			newWeightRatio = newWeight;
		}else{
			oldWeight = 1;
			newWeightRatio = newWeight / totalWeight ;
		}
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				matrix [x, y] = (matrix [x, y] + layer[x,y] * newWeightRatio) / (oldWeight + newWeightRatio);
			}
		}
		totalWeight += newWeight;
	}
		
	private void stretch(float factor){
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				float v=((matrix [x, y] - 0.5f) * factor) + 0.5f;
				v = Mathf.Min (v, 1);
				v = Mathf.Max (v, 0);
				matrix [x, y] = v;
			}
		}
	}

	private void smoothBetween(int smoothingRadius, int counter, float upperBound, float lowerBound){
		float[,] newMatrix = new float[xSize, ySize];
		for (int i = 0; i < counter; i++) {
			for (int y = 0; y < ySize; y++) {
				for (int x = 0; x < xSize; x++) {
					if (matrix [x, y] <= upperBound && matrix[x,y] >= lowerBound) {
						newMatrix [x, y] = smoothedValue (x, y, smoothingRadius);
					} else {
						newMatrix [x, y] = matrix [x, y];
					}
				}
			}
		}
		matrix = newMatrix;
	}

	float smoothedValue(int px, int py, int smoothingRadius){
		float totalValue = 0;
		float totalCounted = 0;
		for (int y = py - smoothingRadius; y <= py + smoothingRadius; y++) {
			for (int x = px - smoothingRadius; x <= px + smoothingRadius; x++) {
				if (y < ySize && y >= 0 && x < xSize && x >= 0) {
					totalCounted++;
					totalValue += matrix [x, y];
				}
			}
		}
		return (totalValue / totalCounted);
	}

	#endregion

}
