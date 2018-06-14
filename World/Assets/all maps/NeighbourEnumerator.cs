using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourEnumerator<T> : IEnumerator {

    private Map<T> map;
    int x, y;
    int i;

    int[] xs = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
    int[] ys = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };


    public NeighbourEnumerator(Map<T> map, int x, int y){
        this.map = map;
        this.x = x;
        this.y = y;
        this.i = 0;
    }

    public T Current{
        get{
            int xx = xs[i]+x;
            if (xx < 0) xx += map.xSize;
            else if (xx >= map.xSize) xx -= map.xSize;
            int yy = ys[i]+y;
            if (yy < 0) yy += map.ySize;
            else if (yy >= map.xSize) yy -= map.ySize;
            return map.grid[xx, yy];}
    }

    object IEnumerator.Current
    {
        get{ return Current;}
    }

    public bool MoveNext()
    {
        if (i >= 7)
            return false;
        i++;
        return true;
    }

    public void Reset()
    {
        this.i = 0;
    }

}
