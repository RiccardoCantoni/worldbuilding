using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction {

	public int dx, dy;

    private Direction(int x, int y)
    {
        this.dx = x;
        this.dy = y;
    }

    public static Direction[] clockOrder =
        new Direction[] {
            Direction.North(),
            Direction.NorthEast(),
            Direction.East(),
            Direction.SouthEast(),
            Direction.South(),
            Direction.SouthWest(),
            Direction.West(),
            Direction.NorthWest()         
            };

	public static Direction North(){
		Direction d = new Direction (0,1);
		return d;
	}

	public static Direction NorthEast(){
		return new Direction (1,1);
	}

	public static Direction East(){
        return new Direction(1, 0);
	}

	public static Direction SouthEast(){
        return new Direction(1, -1);
    }

	public static Direction South(){
        return new Direction(0,-1);
    }

	public static Direction SouthWest(){
        return new Direction(-1, -1);
    }

	public static Direction West(){
        return new Direction(-1, 0);
    }

	public static Direction NorthWest(){
        return new Direction(-1, 1);
    }

	public Direction invert(){
		return new Direction (this.dx * -1, this.dy * -1);
	}

    public Direction rotateClockwise(int times=1)
    {
        int i = this.GetHashCode() + times;
        while (i > 7) i -= 8;
        return Direction.clockOrder[i];
    }

    public Direction rotateCounterClockwise(int times=1)
    {
        int i = this.GetHashCode() - times;
        while (i < 0)
            i += 8;
        return Direction.clockOrder[i];
    }

    public static Direction wraparoundDelta(Point p1, Point p2, int xSize)
    {
        int xd;
        if (p1.x == 0 && p2.x == xSize - 1)
        {
            xd = -1;
        }else if (p1.x == xSize-1 && p2.x == 0)
        {
            xd = 1;
        }
        else
        {
            xd = p2.x - p1.x;
        } 
        return new Direction(xd, p2.y - p1.y);
    }

    public override bool Equals(System.Object obj){
		if (obj == null || GetType () != obj.GetType ()) {
			return false;
		}
		return (((Direction)obj).dx == dx && ((Direction)obj).dy == dy);
	}

    public override string ToString()
    {
        if (this.Equals(Direction.North()))
        {
            return "N";
        }
        if (this.Equals(Direction.NorthEast()))
        {
            return "NE";
        }
        if (this.Equals(Direction.East()))
        {
            return "E";
        }
        if (this.Equals(Direction.SouthEast()))
        {
            return "SE";
        }
        if (this.Equals(Direction.South()))
        {
            return "S";
        }
        if (this.Equals(Direction.SouthWest()))
        {
            return "SW";
        }
        if (this.Equals(Direction.West()))
        {
            return "W";
        }
        if (this.Equals(Direction.NorthWest()))
        {
            return "NW";
        }
        return null;
    }

    public override int GetHashCode()
    {
        if (this.Equals(Direction.North()))
        {
            return 0;
        }
        if (this.Equals(Direction.NorthEast()))
        {
            return 1;
        }
        if (this.Equals(Direction.East()))
        {
            return 2;
        }
        if (this.Equals(Direction.SouthEast()))
        {
            return 3;
        }
        if (this.Equals(Direction.South()))
        {
            return 4;
        }
        if (this.Equals(Direction.SouthWest()))
        {
            return 5;
        }
        if (this.Equals(Direction.West()))
        {
            return 6;
        }
        if (this.Equals(Direction.NorthWest()))
        {
            return 7;
        }
        throw new System.Exception("invalid Direction:"+dx+","+dy);
    }

    public Vector2 toVector()
    {
        return new Vector2(dx, dy).normalized;
    }

    public static Direction fromVector(Vector2 v)
    {
        v.Normalize();
        return new Direction(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }

}