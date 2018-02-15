using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayscaleRenderer : MapRenderer {
	
	public override Color[] getColors(float [,] map, int xSize, int ySize){
		Color[] pix=new Color[xSize*ySize];
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				float v = map [x, y];
				pix [y * xSize + x] = new Color (v,v,v);
			}
		}
		return pix;
	}

}
