using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDMap : Map<int> {

    public IDMap(int xSize, int ySize)
    {
        this.xSize = xSize;
        this.ySize = ySize;
        this.grid = new int[xSize, ySize];
        placePoints(80, 1, 13);
    }

    private void placePoints(int num, int val, int seed)
    {
        Random.InitState(seed);
        int x, y;
        for (int i=0; i<num; i++)
        {
            x = Random.Range(0, xSize - 1);
            y = Random.Range(0, ySize - 1);
            if (grid[x,y] != 0)
            {
                i--;
                continue;
            }
            grid[x, y] = val;
        }
    }
}
