using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAnalyzer {

	private const float resolution = 0.01f;

	public int[] valueCount(Map<float> map){
		int[] counts = new int[101];
		for (int y = 0; y < map.ySize; y++) {
			for (int x = 0; x < map.xSize; x++) {
				if (map.grid [x, y] >= 0 && map.grid [x, y] <= 1) {
					counts [(int)(map.grid [x, y] * 100)]++;
				}
			}
		}
		return counts;
	}

}
