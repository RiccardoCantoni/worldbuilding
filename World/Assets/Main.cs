using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public int xSize, ySize;

	public GameObject mapObject;

	Heightmap heightmap;
	TerrainMap terrainmap;
	WaterMap watermap;
	PhysicalMap physical;

	void Start(){
		physical = mapObject.GetComponent<PhysicalMap> ();
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			MapSerializable mser = MapSerializable.loadFromFile ("wm");
			heightmap = mser.toHeightMap (0.5f);
			physical.init(xSize, ySize);
			watermap = new WaterMap (heightmap);
			terrainmap = new TerrainMap (heightmap, watermap);
			TemperatureMap temperatureMap = new TemperatureMap (terrainmap, 2);
			SatelliteRenderer srend = new SatelliteRenderer ();
			srend.temperatureMap = temperatureMap;
			srend.terrainMap = terrainmap;
			WindMap windmap = new WindMap (xSize, ySize);
			windmap.tradeWindMap (terrainmap, 2);
			WindSpeedRenderer wsr = new WindSpeedRenderer ();
			wsr.m = windmap;
			physical.draw (wsr);
		}
	}

}
