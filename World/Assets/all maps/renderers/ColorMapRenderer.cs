using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMapRenderer : IMapRenderer{

    IDMap idmap;
    Dictionary<int, Color> dictionary;

    SatelliteRenderer sat;

	public ColorMapRenderer(IDMap map, Dictionary<int,Color> dict, SatelliteRenderer sat)
    {
        this.idmap = map;
        this.dictionary = dict;
        this.sat = sat;
    } 

    public Color[] getColors()
    {
        Color[] satmap = sat.getColors();
        Color[] pix = new Color[idmap.xSize * idmap.ySize];
        Color c;
        for (int y = 0; y < idmap.ySize; y++)
        {
            for (int x = 0; x < idmap.xSize; x++)
            {
                if (idmap.grid[x, y] == 0) {
                    c = satmap[y * idmap.xSize + x];
                } else {
                    dictionary.TryGetValue(idmap.grid[x, y], out c);
                }
                pix[y * idmap.xSize + x] = c;
            }
        }
        return pix;
    }
}
