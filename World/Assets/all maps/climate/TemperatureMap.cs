using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureMap : Map<float> {

	TerrainMap terrainmap;
	private const float landMonthlyDelta = 8.33f;
	private const float seaMonthlyDelta = 5.83f;

    public TemperatureMap(int xSize, int ySize, float[,] grid, TerrainMap terrainmap)
    {
        this.terrainmap = terrainmap;
        this.xSize = xSize;
        this.ySize = ySize;
        this.grid = grid;
    }

    public TemperatureMap(TerrainMap tm, int month){
		this.xSize = tm.xSize;
		this.ySize = tm.ySize;
		this.grid = new float[xSize, ySize];
		this.terrainmap = tm;
		float st, lt, lat;
		for (int y=0; y<ySize; y++){
			lat = Latitude.getLatitude(y,ySize);
			st = getSeaTemperature (lat,month);
			lt = getLandTemperature (lat,month);
			for (int x = 0; x < xSize; x++) {
				if (tm.grid [x, y].terrainType == TerrainType.sea) {
					this.grid [x, y] = st;
				} else {
					this.grid [x, y] = lt;
				}
			}
		}
		smooth (5, 5);
		smooth (1, 10);
		IFunction altitudeTemperature = new LineFunction (-80, 40);
		IFunction depthTemperature = new LineFunction (10, -5);
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				if (tm.grid [x, y].terrainType != TerrainType.sea) {
					this.grid [x, y] += altitudeTemperature.calculate (tm.grid [x, y].height);
				} else {
					this.grid [x, y] += depthTemperature.calculate (tm.grid [x, y].height);
				}
			}
		}
	}

	private float getSeaTemperature(float latitude, int month){
		IFunction par = new ParabolaFunction ((-1f / 252f), (5f/36f), (275f/14f));
		return par.calculate (latitude+Latitude.monthlyShift(month+6, seaMonthlyDelta)+3*seaMonthlyDelta);
	}

	private float getLandTemperature(float latitude, int month){
		IFunction par = new ParabolaFunction ((-4f / 555f), (40f/111f), (1140f/37f));
		return par.calculate (latitude+Latitude.monthlyShift(month+6, landMonthlyDelta)+3*landMonthlyDelta);
	}
		

	#region smoothing

	public void smooth(int smoothingRadius, int counter){
		float[,] newMatrix = new float[xSize, ySize];
		for (int i = 0; i < counter; i++) {
			for (int y = 0; y < ySize; y++) {
				for (int x = 0; x < xSize; x++) {
					if (terrainmap.grid [x, y].terrainType != TerrainType.sea) {
						newMatrix [x, y] = smoothedValue (x, y, smoothingRadius);
					} else {
						//newMatrix [x, y] = grid [x, y];
						newMatrix [x, y] = smoothedValue (x, y, smoothingRadius);
					}
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
					totalValue += grid [x, y];
				}
			}
		}
		return (totalValue / totalCounted);
	}

	#endregion
}
