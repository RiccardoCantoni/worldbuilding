using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVFileManager {

	private string path;
	private bool append;

	public CSVFileManager(string path){
		this.path = path;
		append = false;
	}
	public CSVFileManager(string path, bool appendMode){
		this.path = path;
		append = appendMode;
	}

	public void writeLine(string line){
		StreamWriter outStream = new StreamWriter (path,append);
		outStream.WriteLine (line);
		outStream.Close ();
	}

	public void writeLine(string[] values, char separator){
		string str = "";
		for (int i=0; i<values.Length-1; i++){
			str += (values [i] + separator);
		}
		str += values [values.Length - 1];
		writeLine (str);
	}

	public void writeLine(int[] values, char separator){
		string str = "";
		for (int i=0; i<values.Length-1; i++){
			str += (values [i].ToString() + separator);
		}
		str += values [values.Length - 1];
		writeLine (str);
	}

	public void writeLine(float[] values, char separator){
		string str = "";
		for (int i=0; i<values.Length-1; i++){
			str += (values [i].ToString() + separator);
		}
		str += values [values.Length - 1];
		writeLine (str);
	}

}
