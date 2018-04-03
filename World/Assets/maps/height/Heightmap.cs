using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heightmap : Map<float> {

	private int totalWeight;
	public float seaLevel;

	public Heightmap(int xSize, int ySize, float seaLevel){
		this.xSize = xSize;
		this.ySize = ySize;
		this.grid = new float[xSize, ySize];
		this.totalWeight = 0;
		this.seaLevel = seaLevel;
	}

	public bool isLand(int x, int y){
		return (grid [x, y] > seaLevel);
	}

	public void addLayer(HeightmapLayer newLayer){
		float newWeightRatio;
		float oldWeight;
		if (totalWeight == 0) {
			oldWeight = 0;
			newWeightRatio = newLayer.weight;
		}else{
			oldWeight = 1;
			newWeightRatio = (float)newLayer.weight / (float)totalWeight ;
		}
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				grid [x, y] = (grid [x, y] + newLayer.layer[x,y] * newWeightRatio) / (oldWeight + newWeightRatio);
			}
		}
		totalWeight += newLayer.weight;
	}

	public void addLayer(HeightmapLayer newLayer, float lowerLimit, float upperLimit){
		float newWeightRatio;
		float oldWeight;
		float newValue;
		if (totalWeight == 0) {
			oldWeight = 0;
			newWeightRatio = newLayer.weight;
		}else{
			oldWeight = 1;
			newWeightRatio = (float)newLayer.weight / (float)totalWeight ;
		}
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				newValue = (grid [x, y] + newLayer.layer[x,y] * newWeightRatio) / (oldWeight + newWeightRatio);
				if (newValue >= lowerLimit & newValue <= upperLimit) {
					grid [x, y] = newValue;
				}
			}
		}
		totalWeight += newLayer.weight;
	}

	public void stretch(float factor){
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				float v=((grid [x, y] - 0.5f) * factor) + 0.5f;
				v = Mathf.Min (v, 1);
				v = Mathf.Max (v, 0);
				grid [x, y] = v;
			}
		}
	}

	public void smooth(int smoothingRadius, int counter){
		smoothBetween (smoothingRadius, counter, -1, 2);
	}

	public void smoothBetween(int smoothingRadius, int counter, float upperBound, float lowerBound){
		float[,] newMatrix = new float[xSize, ySize];
		for (int i = 0; i < counter; i++) {
			for (int y = 0; y < ySize; y++) {
				for (int x = 0; x < xSize; x++) {
					if (grid [x, y] <= upperBound & grid[x,y] >= lowerBound) {
						newMatrix [x, y] = smoothedValue (x, y, smoothingRadius);
					} else {
						newMatrix [x, y] = grid [x, y];
					}
				}
			}
		}
		this.grid = newMatrix;
	}

	private float smoothedValue(int px, int py, int smoothingRadius){
		float totalValue = 0;
		float totalCounted = 0;
		for (int y = py - smoothingRadius; y <= py + smoothingRadius; y++) {
			for (int x = px - smoothingRadius; x <= px + smoothingRadius; x++) {
				if (y < ySize && y >= 0 && x < xSize && x >= 0) {
					totalCounted++;
					totalValue += grid [x, y];
				}
			}
		}
		return (totalValue / totalCounted);
	}

	public void flattenSea(){
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				grid [x, y] = isLand (x, y) ? grid [x, y] : seaLevel;
			}
		}
	}
}
