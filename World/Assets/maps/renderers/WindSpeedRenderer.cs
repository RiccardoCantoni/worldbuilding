using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpeedRenderer : IMapRenderer {

	public WindMap m;

	public Color[] getColors (){
		int xSize = m.xSize;
		int ySize = m.ySize;
		Color[] pixels = new Color[xSize * ySize];
		float c;

		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				c=MyMath.remap (m.grid [x, y].speed, 2, 10, 0, 1);
				pixels [y * xSize + x] = new Color (c, c, c);

			}
		}
		return pixels;
	}
}
