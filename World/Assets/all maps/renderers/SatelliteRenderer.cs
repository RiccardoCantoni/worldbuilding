using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRenderer : IMapRenderer {

	TemperatureMap temperaturemap;
	TerrainMap terrainmap;
	IFunction snowFunction;

	public bool snow;

    public SatelliteRenderer(TerrainMap terrainmap, TemperatureMap temperaturemap, bool snow = false){
        this.terrainmap = terrainmap;
        this.temperaturemap = temperaturemap;
		snowFunction = new LineFunction (-10, 0.75f, -30, 1);
		snow = false;
	}

	public SatelliteRenderer(bool snow){
		snowFunction = new LineFunction (-10, 0.75f, -30, 1);
		this.snow = snow;
	}

	public Color[] getColors (){
		int xSize = terrainmap.xSize;
		int ySize = terrainmap.ySize;
		Color[] pixels = new Color[xSize * ySize];
		float seaLevel = 0.5f;

		Color yellow = getColor (236, 244, 124);
		Color lGreen = getColor (10, 132, 39);
		Color dGreen = getColor (4, 58, 17);
		Color brown = getColor (63, 56, 21);
		Color gray = getColor (186, 186, 186);
//		Color white = getColor (198, 195, 180);
//		Color white2 = getColor (247, 248, 249);
		Color dBlue = getColor (0, 8, 71);
		Color lBlue = getColor (21, 66, 214);


		Color pixel;
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				if (terrainmap.grid [x, y].terrainType == TerrainType.sea) {
					pixel = lerpBetween (dBlue, lBlue, terrainmap.grid [x, y].height, seaLevel, 0);
				} else {
					if (terrainmap.grid [x, y].height < seaLevel + 0.03f) {
						pixel = yellow;
					} else if (terrainmap.grid [x, y].height < seaLevel + 0.20f) {
						pixel = lerpBetween (lGreen, dGreen, terrainmap.grid [x, y].height, seaLevel + 0.20f, seaLevel + 0.05f);
					} else if (terrainmap.grid [x, y].height < seaLevel + 0.32f) {
						pixel = lerpBetween (dGreen, brown, terrainmap.grid [x, y].height, seaLevel+0.32f, seaLevel + 0.20f);
					} else {
						pixel = lerpBetween (brown, gray, terrainmap.grid [x, y].height, seaLevel+0.45f, seaLevel + 0.32f);
					}
				}
				pixel = Color.Lerp (pixel, Color.white, snowAmount (x, y));
				pixels [y * xSize + x] = pixel;
			}
		}
		return pixels;
	}

	Color getColor(float r, float g, float b){
		return new Color (r / 256f, g / 256f, b / 256f);
	}

	Color lerpBetween(Color a, Color b, float value, float upperBound, float lowerBound){
		return Color.Lerp (a, b, (value - lowerBound) / (upperBound - lowerBound));
	}

	float snowAmount(int x, int y){
		if (terrainmap.grid [x, y].terrainType == TerrainType.sea) {
			if (temperaturemap.grid [x, y] < -10) {
				return 0.9f;
			} else {
				return 0;
			}
		} else {
			if (temperaturemap.grid [x, y] < -5) {
				return snowFunction.calculate (temperaturemap.grid [x, y]);
			}
			return 0;
		}
	}
}
