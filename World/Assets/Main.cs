using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public int xSize, ySize;

	public GameObject mapObject;

	PhysicalMap physical;
    TemperatureMap temperaturemap;
    TemperatureRenderer tr;
    TerrainMap terrainmap;

    int loops;

	void Start(){
        loops = 0;
    }

	void Update(){
		if (Input.GetMouseButtonDown(0)) {           
            physical = mapObject.GetComponent<PhysicalMap>();
            physical.init(xSize, ySize);
            MapSerializable mser = MapSerializable.loadFromFile ("testHM");
            Heightmap heightmap = new Heightmap(xSize, ySize, 0.5f, mser.grid);
            WaterMap watermap = new WaterMap (heightmap);
            terrainmap = new TerrainMap (heightmap, watermap);
            mser = MapSerializable.loadFromFile("testTM");
            temperaturemap = new TemperatureMap(xSize, ySize, mser.grid, terrainmap);
            tr = new TemperatureRenderer(new LandmassMap(terrainmap), temperaturemap);
			physical.draw (tr);
		}
        if (Input.GetMouseButtonDown(1))
        {
            temperaturemap.grid = DataProcessor.reduceScale(temperaturemap.grid, xSize, ySize, -60, 40, 10);
            tr = new TemperatureRenderer(new LandmassMap(terrainmap), temperaturemap);
            physical.draw(tr);
        }
    }

}
