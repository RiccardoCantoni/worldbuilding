using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightmapLayer {

	public float[,] layer{ get;}
	public int xSize{ get;}
	public int ySize{ get;}
	public int weight{ get;}

	public HeightmapLayer(int xSize, int ySize, float[,] layer, int weight){
		this.xSize = xSize;
		this.ySize = ySize;
		this.layer = layer;
		this.weight = weight;
	}

}
