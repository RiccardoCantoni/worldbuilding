using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public int xSize, ySize;

	public GameObject mapObject;

	int update=0;
	int month;

	Heightmap heightmap;
	TerrainMap terrainmap;
	WaterMap watermap;
	PhysicalMap physical;

	void Start(){
		month = 0;
		physical = mapObject.GetComponent<PhysicalMap> ();
		//IFunction norm = new NormalFunction (0, 1.224f);
		//Debug.Log (norm.calculate (-3)*104.5f-7);
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			//HeightMapGenerator gen = new HeightMapGenerator ();
			MapSerializable mser = MapSerializable.loadFromFile ("temp1");
			heightmap = mser.toHeightMap (0.5f);
			physical.init(xSize, ySize);
			watermap = new WaterMap (heightmap);
			terrainmap = new TerrainMap (heightmap, watermap);
			TemperatureMap temperatureMap = new TemperatureMap(terrainmap,month%12);
			LandmassMap landmassmap = new LandmassMap (terrainmap);
			SatelliteRenderer sat = new SatelliteRenderer ();
			sat.temperatureMap = temperatureMap;
			sat.terrainMap = terrainmap;
			physical.draw (sat);
			Debug.Log (month);
			month++;
		}
	}

}
