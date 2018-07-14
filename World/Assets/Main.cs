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

    int state;

	void Start(){
        state = 0;
    }

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
            /*physical = mapObject.GetComponent<PhysicalMap>();
            physical.init(xSize, ySize);
            HeightMapGenerator hgen = new HeightMapGenerator();
            Heightmap heightmap = hgen.generateMap(xSize, ySize);
            WaterMap watermap = new WaterMap(heightmap);
            terrainmap = new TerrainMap(heightmap, watermap);
            temperaturemap = new TemperatureMap(terrainmap, Random.Range(0, 11));
            tr = new TemperatureRenderer(new LandmassMap(terrainmap), temperaturemap);
            physical.draw(tr);*/
            physical = mapObject.GetComponent<PhysicalMap>();
            physical.init(xSize, ySize);
            MapSerializable mser = MapSerializable.loadFromFile ("testHM");
            Heightmap heightmap = new Heightmap(xSize, ySize, 0.5f, mser.grid);
            WaterMap watermap = new WaterMap (heightmap);
            terrainmap = new TerrainMap (heightmap, watermap);
            mser = MapSerializable.loadFromFile("testTM");
            temperaturemap = new TemperatureMap(xSize, ySize, mser.grid, terrainmap);
            tr = new TemperatureRenderer(new LandmassMap(terrainmap), temperaturemap);
            physical.draw(tr);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (state == 0)
            {
                RecursiveWindGenerator wgen = new RecursiveWindGenerator(temperaturemap);
                WindMap wm = wgen.generateRecursiveWind();
                WindDirectionRenderer wr = new WindDirectionRenderer(wm);
                physical.draw(wr);
            }
        }
    }


}
