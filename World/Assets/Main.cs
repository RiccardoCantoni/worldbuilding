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
			MapSerializable mser = MapSerializable.loadFromFile ("testHM");
            heightmap = new Heightmap(xSize, ySize, 0.5f, mser.grid);
			physical.init(xSize, ySize);
			watermap = new WaterMap (heightmap);
			terrainmap = new TerrainMap (heightmap, watermap);
            mser = MapSerializable.loadFromFile("testTM");
            TemperatureMap temperaturemap = new TemperatureMap(xSize, ySize, mser.grid, terrainmap);
            //TemperatureMap temperaturemap = new TemperatureMap (terrainmap, 2);
            //MapSerializable mst = new MapSerializable(xSize, ySize, temperaturemap.grid);

            TemperatureRenderer tr = new TemperatureRenderer(new LandmassMap(terrainmap), temperaturemap);
			physical.draw (tr);
		}
	}

}
