using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessor {

	public static int[] getDistribution(float[] values, float min, float max){
		int[] counts = new int[100];
        int i, skippedH = 0, skippedL = 0;
        float n;
		foreach (float val in values) {
            n = MyMath.remap(val, min, max, 0, 1);
			i = (int)Mathf.Round (100 * n);
            if (i >= 100)
            {
                skippedH++;
                continue;
            }
            if (i < 0)
            {
                skippedL++;
                continue;
            }
		    counts [i]++;
		}
		return counts;
	}

    public static float assignToCluster(float value, float min, float max, int clusters)
    {
        float n = MyMath.remap(value, min, max, 0, 1);
        n = Mathf.Round(clusters * n);
        return min + (n / (float)clusters) * Mathf.Abs(max - min);
    }

    public static float[,] reduceScale(float[,] matrix, int xSize, int ySize, float min, float max, int clusters)
    {
        float[,] res = new float[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                 res[x, y] = assignToCluster(matrix[x, y], min, max, clusters);
            }
        }
        return res;
    }

    public static T[] flatten<T>(T[,] matrix, int xSize, int ySize)
    {
        T[] flat = new T[xSize * ySize];
        for (int x=0; x<xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                flat[y * xSize + x] = matrix[x, y];
            }
        }
        return flat;
    }

    public static void propertyCheck<T>(T[,] matrix, int xSize, int ySize, System.Func<T,bool> eval)
    {
        for(int x=0; x<xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (eval(matrix[x, y]))
                {
                    Debug.Log("property check positive");
                    break;
                }
            }
        }
        Debug.Log("property check negative");
    }

}
