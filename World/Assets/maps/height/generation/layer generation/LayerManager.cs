using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager {

	private int xSize, ySize, seed;

	public LayerManager(int xSize, int ySize, int seed){
		this.xSize = xSize;
		this.ySize = ySize;
		this.seed = seed;
	}

	public HeightmapLayer whiteNoiseLayer(int frequency, int weight){
		ILayerGenerator gen = new WhiteNoiseLayerGenerator (xSize, ySize, frequency, seed);
		return gen.generateLayer (weight);
	}

	public HeightmapLayer voronoiLayer(int numberOfPoints, float samplingScale, float amplitude, int weight){
		ILayerGenerator gen = new VoronoiLayerGenerator (xSize, ySize, numberOfPoints, samplingScale, seed);
		return gen.generateLayer (weight);
	}

	public HeightmapLayer perlinLayer(int frequency, float samplingScale, int weight){
		ILayerGenerator gen = new PerlinNoiseLayerGenerator (xSize, ySize, frequency, samplingScale, seed);
		return gen.generateLayer (weight);
	}
}
