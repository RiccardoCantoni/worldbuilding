using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeWindGenerator {

    public static float midLatitude = 30;
    public static float polarLatitude = 60;
    public static float HalfMidLatitude = 45;
    private static IFunction hadleyLow, hadleyHigh, mid, polar;

    private int xSize, ySize;
    private Map<Vector2> wm;

    public TradeWindGenerator(int xSize, int ySize)
    {
        this.xSize = xSize;
        this.ySize = ySize;
        hadleyLow = new LineFunction(0, 4, midLatitude / 2f, 7);
        hadleyHigh = new LineFunction(midLatitude / 2f, 7, midLatitude, 5);
        mid = new LineFunction(midLatitude, 5, polarLatitude, 10);
        polar = new LineFunction(polarLatitude, 10, 90, 6);
        wm = new Map<Vector2>(xSize, ySize);
    }

    public Map<Vector2> generateTradeWindMap(TerrainMap tm, int month)
    {
        Vector2 v;
        for (int y = 0; y < tm.ySize; y++)
        {
            v = TradeWind((float)y, (float)tm.ySize, month);
            for (int x = 0; x < tm.xSize; x++)
            {
                wm.setAt(x,y,v);
            }
        }
        return wm;
    }

    private static Vector2 TradeWind(float y, float ySize, int month)
    {
        float l = Latitude.monthlyShift(month, 5.83f) + Latitude.getLatitude(y / ySize);
        Vector2 v = new Vector2(-100,-100);
        float mag;
        if (l <= -polarLatitude)
        { //-polar
            v = Vector2.up * polarCellWindSpeed(-l);
        }
        else if (l <= -HalfMidLatitude)
        { //-mid
            v = Vector2.down * midCellWindSpeed(-l);
        }
        else if (l <= -midLatitude)
        { //-mid
            v = new Vector2(1,-1).normalized * midCellWindSpeed(-l);
        }
        else if (l <= 0)
        { //-hadley
            v = new Vector2(-1,1).normalized* hadleyCellWindSpeed(-l);
        }
        else if (l >= polarLatitude)
        { //+polar
            v = Vector2.down * polarCellWindSpeed(l);
        }
        else if (l >= HalfMidLatitude)
        { //+mid
            v = Vector2.up * midCellWindSpeed(l);
        }
        else if (l >= midLatitude)
        { //+mid
            v = new Vector2(1,1).normalized * midCellWindSpeed(l);
        }
        else
        { //+hadley
            v = new Vector2(-1,-1).normalized * hadleyCellWindSpeed(l);
        }
        mag = v.magnitude;
        v = v.normalized * MyMath.remap(mag, 4, 10, 0.1f, 60);
        return v;
    }

    public static int cellIndex(float y, float ySize, int month)
    {
        float l = Latitude.monthlyShift(month, 5.83f) + Latitude.getLatitude(y / ySize);
        if (l <= -polarLatitude)
        { //-polar
            return -3;
        }
        else if (l <= -HalfMidLatitude)
        { //-mid
            return -2;
        }
        else if (l <= -midLatitude)
        { //-mid
            return -2;
        }
        else if (l <= 0)
        { //-hadley
            return 1;
        }
        else if (l >= polarLatitude)
        { //+polar
            return 3;
        }
        else if (l >= HalfMidLatitude)
        { //+mid
            return 2;
        }
        else if (l >= midLatitude)
        { //+mid
            return 2;
        }
        else
        { //+hadley
            return 1;
        }
    }

    static float hadleyCellWindSpeed(float lat)
    {
        if (lat < midLatitude / 2f)
        {
            return hadleyLow.calculate(lat);
        }
        return hadleyHigh.calculate(lat);
    }

    static float midCellWindSpeed(float lat)
    {
        return mid.calculate(lat);
    }

    static float polarCellWindSpeed(float lat)
    {
        return polar.calculate(lat);
    }

    #region smoothing

    public void smoothSpeed(int smoothingRadius, int counter)
    {
        Vector2[,] newMatrix = new Vector2[xSize, ySize];
        for (int i = 0; i < counter; i++)
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    newMatrix[x, y] = newMatrix[x, y].normalized * smoothedValue(x, y);
                }
            }
        }
        wm.grid = newMatrix;
    }

    private float smoothedValue(int px, int py)
    {
        List<Point> neighbours = wm.wraparoundNeighbours(px, py);
        float acc = 0;
        foreach (Point p in neighbours)
        {
            acc += wm.getAt(p).magnitude;
        }
        return acc / (float)neighbours.Count;
    }

    #endregion
}
