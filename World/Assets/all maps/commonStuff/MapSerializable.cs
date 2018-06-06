using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class MapSerializable {

	const string datapath = "data/";
	const string extension = ".map";

	public int xSize;
	public int ySize;
	public float[,] grid;

	public MapSerializable(int xSize, int ySize, float[,] grid){
		this.xSize = xSize;
		this.ySize = ySize;
		this.grid = grid;
	}

	public void saveToFile(string filename){
		string destination = datapath + filename + extension;
		FileStream fs;
		fs = File.Open (destination, FileMode.OpenOrCreate);
		BinaryFormatter bf = new BinaryFormatter ();
		bf.Serialize (fs, this);
		fs.Close ();
	}

	public static MapSerializable loadFromFile(string filename){
		string destination = datapath + filename + extension;
		FileStream fs;
		if (!File.Exists (destination)) {
			Debug.LogError ("map file not found");
		}
		fs = File.OpenRead (destination);
		BinaryFormatter bf = new BinaryFormatter ();
		MapSerializable m = (MapSerializable)bf.Deserialize (fs);
		fs.Close ();
		return m;
	}

}
