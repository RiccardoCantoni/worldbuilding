using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterMap : Map<TerrainType> {
	
	private bool ocean_flag;

	public WaterMap(Heightmap hm){
		List<Point> flooded;
		this.xSize = hm.xSize;
		this.ySize = hm.ySize;
		this.grid = new TerrainType[hm.xSize, hm.ySize];
		flagLand (hm);
		for (int x = 0; x < xSize; x++) {
			flagCol (x);
		}
		for (int y = 0; y < ySize; y++) {
			flagRow (y);
		}
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				if (grid [x, y] == 0) {
					flooded = flood (x, y, TerrainType.undecided);
					flag (flooded, ocean_flag ? TerrainType.sea : TerrainType.lake);
				}
			}
		}
	}
		
	private void flagRow(int y){
		int lowerBound = 0;
		for (int x = 0; x < xSize; x++) {
			if (grid [x, y] == TerrainType.land) {
				lowerBound = x;
				break;
			} else {
				flag (x, y, TerrainType.sea);
			}
		}
		for (int x = xSize-1; x > lowerBound; x--) {
			if (grid [x, y] == TerrainType.land) {
				break;
			} else {
				flag (x, y, TerrainType.sea);
			}
		}
	}

	private void flagCol(int x){
		int lowerBound = 0;
		for (int y = 0; y < ySize; y++) {
			if (grid [x, y] == TerrainType.land) {
				lowerBound = y;
				break;
			} else {
				flag (x, y, TerrainType.sea);
			}
		}
		for (int y = ySize-1; y > lowerBound; y--) {
			if (grid [x, y] == TerrainType.land) {
				break;
			} else {
				flag (x, y, TerrainType.sea);
			}
		}
	}

	private void flagLand(Heightmap hm){
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				if (hm.isLand(x,y)) {
					flag (x, y, TerrainType.land);
				}
			}
		}
	}

	private void flag(int x, int y, TerrainType t){
		grid [x, y] = t;
	}

	private void flag(List<Point> toFlag, TerrainType t){
		foreach (Point p in toFlag) {
			flag (p.x, p.y, t);
		}
	}

	private List<Point> flood(int x0, int y0, TerrainType color){
		List<Point> fringe = new List<Point> ();
		fringe.Add (new Point (x0, y0));
		Point curPoint;
		List<Point> succ = new List<Point> ();
		List<Point> flooded = new List<Point> ();
		ocean_flag = false;
		while (fringe.Count != 0) {
			if (fringe.Count > 20000) {
				break;
			}
			curPoint = fringe [fringe.Count - 1];
			fringe.RemoveAt (fringe.Count - 1);
			flooded.Add (curPoint);
			succ = getNeighbours (curPoint);
			flag (succ, color);
			fringe.AddRange (succ);
		}
		return flooded;
	}

	private List<Point> getNeighbours(Point p){
		List<Point> neighbours = new List<Point> ();
		Point pt;
		pt = getValidPoint (p.x - 1, p.y);
		if (pt!=null)	neighbours.Add (pt);
		pt = getValidPoint (p.x + 1, p.y);
		if (pt!=null)	neighbours.Add (pt);
		pt = getValidPoint (p.x, p.y - 1);
		if (pt!=null)	neighbours.Add (pt);
		pt = getValidPoint (p.x, p.y + 1);
		if (pt!=null)	neighbours.Add (pt);
		return neighbours;
	}

	private Point getValidPoint(int x, int y){
		if (isInMap(x,y)) {
			if (grid [x, y] == TerrainType.sea) {
				ocean_flag = true;
				return null;
			} else if (grid [x, y] == TerrainType.unknown) {
				return new Point (x, y);
			}
		}
		return null;
	}


}
