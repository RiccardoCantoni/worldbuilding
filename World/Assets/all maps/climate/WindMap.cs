using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMap : Map<Wind>{

	private static float midLatitude = 30;
	private static float polarLatitude = 60;
	private static float HalfMidLatitude = 45;

	private static IFunction hadleyLow, hadleyHigh, mid, polar;

	public WindMap(int xSize, int ySize){
		this.grid = new Wind[xSize, ySize];
		this.xSize = xSize;
		this.ySize = ySize;
		hadleyLow = new LineFunction (0, 4, midLatitude/2f, 7);
		hadleyHigh = new LineFunction (midLatitude / 2f, 7, midLatitude, 5);
		mid = new LineFunction (midLatitude, 5, polarLatitude, 10);
		polar = new LineFunction (polarLatitude, 10, 90, 6);
	}

	public void tradeWindMap(TerrainMap tm, int month){
		Wind w;
		for (int y = 0; y < tm.ySize; y++) {
			w = TradeWind (y, tm.ySize, month);
			for (int x = 0; x < tm.xSize; x++) {
				grid [x, y] = w;
			}
		}
	}

	private static Wind TradeWind(float y, float ySize, int month){
		float l = Latitude.monthlyShift(month, 5.83f) + Latitude.getLatitude (y/ySize);
		Direction d;
		float s;
		if (l < -polarLatitude) { //-polar
			d = Direction.North ();
			s = polarCellWindSpeed (-l);
		} else if (l < -HalfMidLatitude) { //-mid
			d = Direction.South ();
			s = midCellWindSpeed (-l);
		} else if (l < -midLatitude) { //-mid
			d = Direction.SouthEast ();
			s = midCellWindSpeed (-l);
		} else if (l < 0) { //-hadley
			d = Direction.NorthWest ();
			s = hadleyCellWindSpeed (-l);
		} else if (l > polarLatitude) { //+polar
			d = Direction.South ();
			s = polarCellWindSpeed (l);
		} else if (l > HalfMidLatitude) { //+mid
			d = Direction.North ();
			s = midCellWindSpeed (l);
		} else if (l > midLatitude) { //+mid
			d = Direction.NorthEast ();
			s = midCellWindSpeed (l);
		} else { //+hadley
			d = Direction.SouthWest ();
			s = hadleyCellWindSpeed (l);
		}
		return new Wind (d, s);
	}

	static float hadleyCellWindSpeed(float lat){
		if (lat<midLatitude/2f){
			return hadleyLow.calculate (lat);
		}
		return hadleyHigh.calculate (lat);
	}

	static float midCellWindSpeed(float lat){
		return mid.calculate (lat);
	}

	static float polarCellWindSpeed(float lat){
		return polar.calculate (lat);
	}

	#region smoothing

	public void smoothSpeed(int smoothingRadius, int counter){
		Wind[,] newMatrix = new Wind[xSize, ySize];
		for (int i = 0; i < counter; i++) {
			for (int y = 0; y < ySize; y++) {
				for (int x = 0; x < xSize; x++) {
					newMatrix [x, y].speed = smoothedValue (x, y, smoothingRadius);
				}
			}
		}
		this.grid = newMatrix;
	}

	private float smoothedValue(int px, int py, int smoothingRadius){
		float totalValue = 0;
		float totalCounted = 0;
		for (int y = py - smoothingRadius; y <= py + smoothingRadius; y++) {
			for (int x = px - smoothingRadius; x <= px + smoothingRadius; x++) {
				if (y < ySize && y >= 0 && x < xSize && x >= 0) {
					totalCounted++;
					totalValue += grid [x, y].speed;
				}
			}
		}
		return (totalValue / totalCounted);
	}

	#endregion
	
}
