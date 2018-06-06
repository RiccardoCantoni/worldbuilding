﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map<T> {
	public T[,] grid;
	public int xSize;
	public int ySize;

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

    public void setAt(Point p, T value)
    {
        grid[p.x, p.y] = value;
    }
}
