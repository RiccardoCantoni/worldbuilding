using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTrailRenderer : IMapRenderer
{
    Heightmap heightmap;
    WindMap windmap;
    Color[] satColors;

    public AgentTrailRenderer(Heightmap heightmap, WindMap windmap, SatelliteRenderer sat)
    {
        this.heightmap = heightmap;
        this.windmap = windmap;
        satColors = sat.getColors();
    }

    public Color[] getColors()
    {
        Color[] pixels = new Color[heightmap.xSize * heightmap.ySize];
        for (int y = 0; y < heightmap.ySize; y++)
        {
            for (int x = 0; x < heightmap.xSize; x++)
            {
                if (windmap.grid[x, y] == null)
                {
                    pixels[y * heightmap.xSize + x] = satColors[y * heightmap.xSize + x];
                }
                else
                {
                    pixels[y * heightmap.xSize + x] = Color.red;
                }
            }
        }
        return pixels;
    }
}
