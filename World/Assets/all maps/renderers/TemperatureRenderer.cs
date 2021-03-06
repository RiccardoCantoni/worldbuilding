﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureRenderer : IMapRenderer {

	float max = 30;
	float min = -50;
	public LandmassMap landmassmap;
	public TemperatureMap temperaturemap;

    public TemperatureRenderer(LandmassMap landmassmap, TemperatureMap temperaturemap)
    {
        this.landmassmap = landmassmap;
        this.temperaturemap = temperaturemap;
    }

	public Color[] getColors(){
		int xSize = temperaturemap.xSize;
		int ySize = temperaturemap.ySize;
		Color[] pixels = new Color[xSize * ySize];
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				if (landmassmap.grid [x, y] == 1) {
					pixels [y * xSize + x] = Color.black;
				} else {
					pixels [y * xSize + x] = temperatureColor(temperaturemap.grid[x,y]);
				}
			}
		}
		return pixels;
	}

	public Color temperatureColor(float temperature){
		HSVColor magenta = new HSVColor (0.90f, 1, 1);
		HSVColor red = new HSVColor (0, 1, 1);
		float t = MyMath.remap (temperature, min, max, 0, 1.166f);
		if (t >= 0.166f) {
			return HSVColor.lerpHue (magenta, red, t-0.166f).toRGB ();
		} else {
			return Color.Lerp (Color.white, magenta.toRGB(), t / 0.166f);
		}
	}

}
