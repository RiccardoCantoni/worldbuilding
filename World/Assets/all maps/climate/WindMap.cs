using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMap : Map<Wind>{

    private Map<Vector2> vectormap;
    private int month;

    public WindMap(int xSize, int ySize)
    {
        this.vectormap = new Map<Vector2>(xSize, ySize);
        this.grid = new Wind[xSize, ySize];
        this.xSize = xSize;
        this.ySize = ySize;
    }

    public WindMap(int xSize, int ySize, TerrainMap terrainmap, TemperatureMap temperaturemap, int month)
    {
        this.vectormap = new Map<Vector2>(xSize, ySize);
        this.grid = new Wind[xSize, ySize];
        this.xSize = xSize;
        this.ySize = ySize;
        this.month = month;
        TradeWindGenerator twg = new TradeWindGenerator(xSize, ySize);
        RecursiveWindGenerator rwg = new RecursiveWindGenerator(temperaturemap);
        Map<Vector2> tradeWindMap = twg.generateTradeWindMap(terrainmap, month);
        Map<Vector2> recursiveWindMap = rwg.generateRecursiveWind();
        
        //blend
        float tw;

        IFunction combination = new ParabolaFunction(4,-10f,4.15f);
        for (int x = 0; x<xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                tw = combination.calculate(terrainmap.grid[x, y].height);
                tw = Mathf.Min(tw, 1);
                tw = Mathf.Max(tw, 0);
                vectormap.setAt(x, y, recursiveWindMap.grid[x, y] * (1.2f) * (1 - tw) + tradeWindMap.grid[x, y] * tw);
            }
        }
        smoothConvert(8, terrainmap);
    }

    public void smoothConvert(int smoothFactor, TerrainMap terrainmap)
    {
        Map<Vector2> newvectormap = new Map<Vector2>(xSize, ySize);
        newvectormap.grid = vectormap.grid;
        int cell;
        for (int s = 0; s < smoothFactor; s++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (terrainmap.getAt(x, y).terrainType == TerrainType.sea)
                    {
                        continue;
                    }
                    else
                    {
                        cell = TradeWindGenerator.cellIndex(y, ySize, month);
                        List<Point> neighbours = wraparoundNeighbours(x, y);
                        Vector2 acc = Vector2.zero;
                        int neighbourCount = 0;
                        foreach (Point p in neighbours)
                        {
                            if (TradeWindGenerator.cellIndex(p.y, ySize, month) == cell)
                            {
                                acc += vectormap.getAt(p);
                                neighbourCount++;
                            }
                        }
                        newvectormap.setAt(x, y, acc / (float)neighbourCount);
                    }
                }
            }
            vectormap.grid = newvectormap.grid;
        }
        for (int x=0; x<xSize; x++)
        {
            for (int y=0; y<ySize; y++)
            {
                setAt(x, y, Wind.fromVector(vectormap.getAt(x, y)));
            }
        }
    }
}
