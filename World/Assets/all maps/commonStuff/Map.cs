using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map<T> {
    public T[,] grid;
    public int xSize;
    public int ySize;

    public Map(int xSize, int ySize){
        grid = new T[xSize, ySize];
        this.xSize = xSize;
        this.ySize = ySize;
    }

    public Map()
    {

    }

	public bool isInMap(Point p){
		return isInXAxis(p) && isInYAxis(p);
	}

    public bool isInMap(int x, int y)
    {
        return (x >= 0 && x < xSize && y >= 0 && y < ySize);
    }

    public bool isInXAxis(Point p)
    {
        return (p.x >= 0 && p.x < xSize);
    }

    public bool isInYAxis(Point p)
    {
        return (p.y >= 0 && p.y < ySize);
    }

    public T getAt(Point p)
    {
        try
        {
            return grid[p.x, p.y];
        }
        catch (System.IndexOutOfRangeException)
        {
            throw new System.IndexOutOfRangeException("map.getAt index out of bounds: " + p.ToString());
        }
    }

    public T getAt(int x, int y)
    {
        try
        {
            return grid[x, y];
        }
        catch (System.IndexOutOfRangeException)
        {
            throw new System.IndexOutOfRangeException("map.getAt index out of bounds: " + x + "," + y);
        }
    }

    public void setAt(Point p, T value)
    {
        grid[p.x, p.y] = value;
    }

    public void setAt(int x, int y, T value)
    {
        grid[x, y] = value;
    }

    public void setRectAt(int x, int y, int dx, int dy, T value)
    {
        for (int xx =x; xx<x+dx; xx++)
        {
            for (int yy = y; yy < y + dy; yy++)
            {
                setAt(xx, yy, value);
            }
        }
    }

    public List<Point> wraparoundNeighbours(int x, int y)
    {
        List<Point> pts = new List<Point>();
        int xi, yi;
        for (int xx = 0; xx < 3; xx++)
        {
            for (int yy = 0; yy < 3; yy++)
            {
                if (xx == yy) continue;
                xi = xx+x;
                yi = yy+y;
                if (yi < 0 || yi >= ySize) continue;
                if (xi < 0)
                {
                    xi *= xSize;
                }
                else if (xi >= xSize)
                {
                    xi -= xSize;
                }
                pts.Add(new Point(xi, yi));
            }
        }
        return pts;
    }
    
}
