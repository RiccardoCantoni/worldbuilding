using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveWindGenerator {

    int xSize, ySize;
    TemperatureMap tmap;
    Map<Vector2>[] layers;
    float[] layerDirWeights;
    float[] layerSpeedWeights;

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
        this.layerDirWeights = new float[] { 6, 3, 2, 1};
    }


	public Map<Vector2> generateRecursiveWind()
    {
        for (int l =0; l<numLayers; l++)
        {

            int size = (int)Mathf.Pow(2, (l + 2));
            for (int x = 0; x <= xSize - size; x += size)
            {
                for (int y = 0; y <= ySize - size; y += size)
                {
                    squareStep(x, y, size-1, l);
                }
            }

        }
        return blendLayers();
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
        layers[layerIndex].setRectAt(px, py, size+1, size+1, squareDirection(mini, maxi) * squareSpeed(minv, maxv));
    }

    private Map<Vector2> blendLayers()
    {
        Map<Vector2> m = new Map<Vector2>(xSize, ySize);
        IFunction redistr = new LineFunction(0, 3, 60, 1.5f);
        Vector2 v;
        float mag;
        for (int x=0; x<xSize; x++)
        {
            for (int y=0; y<ySize; y++)
            {
                v = LayersWeightedAverage(x, y);
                mag = v.magnitude;
                v.Normalize();
                v *= mag+redistr.calculate(mag);
                m.setAt(x, y, v);
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
        return MyMath.remap(max - min,0,21,0,60);
    }

    private Vector2 LayersWeightedAverage(int x, int y)
    {
        Vector2 v = Vector2.zero;
        float totWeight = 0;
        for (int l=0; l<numLayers; l++)
        {
            v += layers[l].grid[x, y]* layerDirWeights[l];
            totWeight += layerDirWeights[l];
        }
        return (v / totWeight);
    }
}
