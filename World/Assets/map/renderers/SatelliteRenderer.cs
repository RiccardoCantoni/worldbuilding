using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRenderer : MapRenderer {

	public override Color[] getColors (float[,] map, int xSize, int ySize){
		Color[] pixels = new Color[xSize * ySize];
		float seaLevel = 0.5f;

		Color yellow = getColor (236, 244, 124);
		Color lGreen = getColor (10, 132, 39);
		Color dGreen = getColor (4, 58, 17);
		Color brown = getColor (63, 56, 21);
		Color gray = getColor (71, 68, 54);
		Color white = getColor (198, 195, 180);
		Color white2 = getColor (247, 248, 249);
		Color dBlue = getColor (0, 8, 71);
		Color lBlue = getColor (21, 66, 214);

		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				if (map [x, y] < seaLevel) {
					pixels [y * xSize + x] = lerpBetween(dBlue, lBlue, map[x,y], seaLevel, 0);
					continue;
				}
				if (map [x, y] < seaLevel+0.05f) {
					pixels [y * xSize + x] = yellow;
					continue;
				}
				if (map [x, y] < seaLevel+0.25f) {
					pixels [y * xSize + x] = lerpBetween(lGreen, dGreen, map[x,y], seaLevel+0.25f, seaLevel+0.05f);
					continue;
				}
				if (map [x, y] < seaLevel+0.4f) {
					pixels [y * xSize + x] = lerpBetween(brown, gray, map[x,y], seaLevel+0.4f, seaLevel+0.25f);
					continue;
				}
				pixels [y * xSize + x] = lerpBetween(white, white2, map[x,y], 1, seaLevel+0.4f);
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

}
