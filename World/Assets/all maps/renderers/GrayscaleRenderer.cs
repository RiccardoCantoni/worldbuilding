using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayscaleRenderer : IMapRenderer {

	public Heightmap m;
	public int shift;
	
	public Color[] getColors(){
		int xSize = m.xSize;
		int ySize = m.ySize;
		Color[] pix=new Color[xSize*ySize];
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				float v = m.grid [x, y];
				pix [y * xSize + (x + shift)%xSize] = new Color (v,v,v);
			}
		}
		return pix;
	}

}
