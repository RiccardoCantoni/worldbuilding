using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveWindGenerator {

    int xSize, ySize;
    TemperatureMap tmap;
    Map<Vector2>[] layers;

    int numLayers = 4;

    Vector2[] vecs = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        new Vector2(1,-1),
        new Vector2(-1,-1),
        Vector2.up,
        Vector2.up,
        new Vector2(1,1),
        new Vector2(-1,1),
        new Vector2(-1,1),
        new Vector2(-1,-1),
        Vector2.up,
        Vector2.left,
        new Vector2(1,1),
        new Vector2(1,-1),
        Vector2.right,
        Vector2.up
    };

    public RecursiveWindGenerator(TemperatureMap temperaturemap)
    {
        this.tmap = temperaturemap;
        this.xSize = temperaturemap.xSize;
        this.ySize = temperaturemap.ySize;
        this.layers = new Map<Vector2>[numLayers];
        for (int i=0; i<numLayers; i++)
        {
            layers[i] = new Map<Vector2>(xSize, ySize);
        }
    }


	public WindMap generateRecursiveWind()
    {
        for (int l =0; l<numLayers; l++)
        {
            generateLayer(l);
        }
        return blendLayers();
    }

    private void generateLayer(int layerIndex)
    {
        int size = (int)Mathf.Pow(2,(layerIndex + 2));
        
        for (int x = 0; x <= xSize -size; x += size)
        {
            for (int y = 0; y <= ySize -size; y += size)
            {
                squareStep(x, y, size-1, layerIndex);
            }
        }
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
        setSquare(px, py, size, squareDirection(mini, maxi)* squareSpeed(minv, maxv), layerIndex);
    }

    private void setSquare(int px, int py, int size, Vector2 value, int layerIndex)
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
        WindMap m = new WindMap(xSize, ySize);
        for (int x=0; x<xSize; x++)
        {
            for (int y=0; y<ySize; y++)
            {
                m.setAt(x, y, Wind.fromVector(meanOfLayers(x, y)));
            }
        }
        return m;
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

    private Vector2 squareDirection(int min, int max)
    {
        if (min == max) throw new System.Exception("illegal argument: " + min + "," + max);
        return vecs[min * 4 + max];
    }

    private float squareSpeed(float min, float max)
    {
        return max - min;
    }

    private Vector2 meanOfLayers(int x, int y)
    {
        Vector2 v = Vector2.zero;
        for (int l=0; l<numLayers; l++)
        {
            v += layers[l].grid[x, y];
        }
        return (v /= numLayers);
    }
}
