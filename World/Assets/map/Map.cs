using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour{

	public float[,] heightMap;
	public int renderMode;

	private int xSize;
	private int ySize;
	private Texture2D noiseTex;
	private Renderer rend;
	private MapRenderer mr;

	void Start(){

	}

	public void newMap(int x, int y){
		rend = GetComponent<Renderer>();
		mr = new MapRenderer ();
		xSize = x;
		ySize = y;
		heightMap = new float[xSize,ySize];
		noiseTex = new Texture2D(xSize, ySize);
		noiseTex.filterMode = FilterMode.Point;
		rend.material.mainTexture = noiseTex;
	}

	public void updateMap(){
		Color[] pix;
		switch (renderMode) {
			case 0:
				mr = new SatelliteRenderer ();
				break;
			case 1:
				mr = new GrayscaleRenderer ();
				break;
		default:
			mr = new SatelliteRenderer ();	
			break;
		}
		pix = mr.getColors (heightMap, xSize, ySize);
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}
}
