using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveWindGenerator {

    int xSize, ySize;
    TemperatureMap tmap;
    WindMap[] layers;

    Direction[] dirs = new Direction[]
        {
            Direction.North(),
            Direction.South(),
            Direction.SouthEast(),
            Direction.SouthWest(),
            Direction.North(),
            Direction.North(),
            Direction.NorthEast(),
            Direction.NorthWest(),
            Direction.NorthWest(),
            Direction.SouthWest(),
            Direction.North(),
            Direction.West(),
            Direction.NorthEast(),
            Direction.SouthEast(),
            Direction.East(),
            Direction.North()
        };

    public RecursiveWindGenerator(TemperatureMap temperaturemap)
    {
        this.tmap = temperaturemap;
        this.xSize = temperaturemap.xSize;
        this.ySize = temperaturemap.ySize;
        this.layers = new WindMap[5];
        for (int i=0; i<5; i++)
        {
            layers[i] = new WindMap(xSize, ySize);
        }
    }


	public WindMap generateRecursiveWind()
    {
        generateRecursiveAux(0);
        return blendLayers();
    }

    private void generateRecursiveAux(int layerIndex)
    {
        if (layerIndex > 4) return;
        int size = 2 ^ (layerIndex + 1);
        for (int x = 0; x <= xSize -size; x += size)
        {
            for (int y = 0; y <= ySize -size; y += size)
            {
                squareStep(x, y, size-1, layerIndex);
            }
        }
        generateRecursiveAux(layerIndex + 1);
    }

    private void squareStep(int px, int py, int size, int layerIndex)
    {
        float[] NSEO = new float[] {
            lineAverage(new Point(px + size, py + size), Direction.West(), size),
            lineAverage(new Point(px, py), Direction.East(), size),
            lineAverage(new Point(px + size, py + size), Direction.South(), size),
            lineAverage(new Point(px, py), Direction.North(), size)
        };
        int mini = ArrayUtil<float>.argMin(NSEO, (a)=> { return a; });
        int maxi = ArrayUtil<float>.argMax(NSEO, (a) => { return a; });
        float minv = ArrayUtil<float>.min(NSEO, (a) => { return a; });
        float maxv = ArrayUtil<float>.max(NSEO, (a) => { return a; });
        setSquare(px, py, size, new Wind(squareDirection(mini, maxi), squareSpeed(minv, maxv)), layerIndex);
    }

    private void setSquare(int px, int py, int size, Wind value, int layerIndex)
    {
        for (int x = 0; x <= size; x++)
        {
            for (int y = 0; y <= size; y++)
            {
                layers[layerIndex].setAt(px + x, py + y, value);
            }
        }
    }

    private WindMap blendLayers()
    {
        return layers[2];
    }

    private float lineAverage(Point p, Direction d, int length)
    {
        float tot = 0;
        for (int i=0; i<length; i++)
        {
            tot += tmap.getAt(p);
            p=p.moveBy(d);
        }
        return tot / (float)length;
    }

    private Direction squareDirection(int min, int max)
    {
        if (min == max) throw new System.Exception("illegal argument: " + min + "," + max);
        return dirs[min * 4 + max];
    }

    private float squareSpeed(float min, float max)
    {
        return 1;
    }
}
