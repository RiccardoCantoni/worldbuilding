using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction {

	public int dx, dy;

	public static Direction North(){
		Direction d = new Direction ();
		d.dx = 0;
		d.dy = 1;
		return d;
	}

	public static Direction NorthEast(){
		Direction d = new Direction ();
		d.dx = 1;
		d.dy = 1;
		return d;
	}

	public static Direction East(){
		Direction d = new Direction ();
		d.dx = 0;
		d.dy = 1;
		return d;
	}

	public static Direction SouthEast(){
		Direction d = new Direction ();
		d.dx = -1;
		d.dy = 1;
		return d;
	}

	public static Direction South(){
		Direction d = new Direction ();
		d.dx = 0;
		d.dy = -1;
		return d;
	}

	public static Direction SouthWest(){
		Direction d = new Direction ();
		d.dx = -1;
		d.dy = -1;
		return d;
	}

	public static Direction West(){
		Direction d = new Direction ();
		d.dx = -1;
		d.dy = 0;
		return d;
	}

	public static Direction NorthWest(){
		Direction d = new Direction ();
		d.dx = 1;
		d.dy = -1;
		return d;
	}

	public static Point movePoint(Point p, Direction d){
		return new Point (p.x + d.dx, p.y + d.dy);
	}

	public override bool Equals(System.Object obj){
		if (obj == null || GetType () != obj.GetType ()) {
			return false;
		}
		return (((Direction)obj).dx == dx && ((Direction)obj).dy == dy);
	}

	public bool Equals(Direction d){
		return (d.dx == dx && d.dy == dy);
	}
}
