using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMap : MonoBehaviour{

	private Texture2D mapTexture;
	private Renderer rend;
	private int xSize, ySize;


	public void init(int xSize, int ySize){
		rend = GetComponent<Renderer>();
		mapTexture = new Texture2D(xSize, ySize);
		mapTexture.filterMode = FilterMode.Point;
		rend.material.mainTexture = mapTexture;
	}

	public void draw(IMapRenderer mapRenderer){
		Color[] pix;
		pix = mapRenderer.getColors ();
		mapTexture.SetPixels(pix);
		mapTexture.Apply();
	}
}
