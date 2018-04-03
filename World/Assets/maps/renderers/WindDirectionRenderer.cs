using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirectionRenderer : IMapRenderer {

	public WindMap m;

	public Color[] getColors (){
		int xSize = m.xSize;
		int ySize = m.ySize;
		Color[] pixels = new Color[xSize * ySize];

		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				pixels [y * xSize + x] = directionToColor (m.grid [x, y].direction);
			}
		}
		return pixels;
	}

	Color directionToColor(Direction d){
		HSVColor mag = new HSVColor (Color.magenta);
		HSVColor red = new HSVColor (Color.red);

		if (d.Equals (Direction.North())) {
			return HSVColor.lerpHue (mag, red, 0).toRGB();
		}
		if (d.Equals (Direction.NorthEast())) {
			return HSVColor.lerpHue (mag, red, 0.125f).toRGB();
		}
		if (d.Equals (Direction.East())) {
			return HSVColor.lerpHue (mag, red, 0.25f).toRGB();
		}
		if (d.Equals (Direction.SouthEast())) {
			return HSVColor.lerpHue (mag, red, 0.5f).toRGB();
		}
		if (d.Equals (Direction.South())) {
			return HSVColor.lerpHue (mag, red, 0.625f).toRGB();
		}
		if (d.Equals (Direction.SouthWest())) {
			return HSVColor.lerpHue (mag, red, 0.75f).toRGB();
		}
		if (d.Equals (Direction.West())) {
			return HSVColor.lerpHue (mag, red, 0.875f).toRGB();
		}
		if (d.Equals (Direction.NorthWest())) {
			return HSVColor.lerpHue (mag, red, 1).toRGB();
		}
		return Color.black;
	}
}
