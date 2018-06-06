using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpeedRenderer : IMapRenderer {

	private WindMap m;
    private LandmassMap lm;

    private ColorScale scale;

    public WindSpeedRenderer(WindMap windmap, LandmassMap landmassmap)
    {
        scale = new ColorScale(Color.green, Color.red, 0, 1);
        m = windmap;
        lm = landmassmap;
    }

	public Color[] getColors (){
		int xSize = m.xSize;
		int ySize = m.ySize;
		Color[] pixels = new Color[xSize * ySize];
		float c;

		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
                if (lm.grid[x, y] == 1)
                {
                    pixels[y * xSize + x] = Color.black;
                }
                else {
                    c = MyMath.remap(m.grid[x, y].speed, 2, 10, 0, 1);
                    pixels[y * xSize + x] = scale.getColor(c);
                }
			}
		}
		return pixels;
	}
}
