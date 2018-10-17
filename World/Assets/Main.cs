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
    WindMap wm;

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
                wm = new WindMap(xSize, ySize, terrainmap, temperaturemap, 3);
               
                WindSpeedRenderer wsr = new WindSpeedRenderer(wm, new LandmassMap(terrainmap));
                WindDirectionRenderer wdr = new WindDirectionRenderer(wm, new LandmassMap(terrainmap));
                
                physical.draw(wsr);
                state++;
            }
            else if (state >= 1)
            {
                RecursiveWindGenerator wgen = new RecursiveWindGenerator(temperaturemap);
                wm = new WindMap(xSize, ySize, terrainmap, temperaturemap, state);

                WindSpeedRenderer wsr = new WindSpeedRenderer(wm, new LandmassMap(terrainmap));
                WindDirectionRenderer wdr = new WindDirectionRenderer(wm, new LandmassMap(terrainmap));

                physical.draw(wdr);
                state++;
            }

        }
    }


}
