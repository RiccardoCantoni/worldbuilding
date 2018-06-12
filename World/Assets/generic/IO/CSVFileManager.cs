using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVFileManager {

	private string path;
    private bool append, overwrite;

	public CSVFileManager(string path){
		this.path = path;
		append = false;
	}
    public CSVFileManager(string path, bool appendMode = false, bool overwriteMode = false){
		this.path = path;
		append = appendMode;
        overwrite = overwriteMode;
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

    public void writeMatrix(float[,]matrix, int xSize, int ySize, char separator)
    {
        if (overwrite)
            append = false;
        string line;
        for (int y=0; y<ySize; y++)
        {
            line = "";
            for (int x=0; x<xSize; x++)
            {
                line += matrix[x, y].ToString() + separator;
            }
            writeLine(line);
            append = true;
        }
    }

}
