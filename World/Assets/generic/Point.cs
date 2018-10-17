using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point  {

	public int x,y;

	public Point(int x, int y){
		this.x = x;
		this.y = y;
	}

    public Point moveBy(Direction d)
    {
        return new Point(x+d.dx, y+d.dy);
    }

    public Point moveByWraparound(Direction d, int xSize)
    {
        int nx = x + d.dx;
        if (nx >= xSize) nx -= xSize;
        if (nx < 0) nx += xSize;
        return new Point(nx, y + d.dy);
    }

    public Direction directionTo(Point p, int xSize)
    {
        return Direction.wraparoundDelta(this, p, xSize);
    }

    public override string ToString()
    {
        return "pt(" + x + "," + y + ")";
    }
}
