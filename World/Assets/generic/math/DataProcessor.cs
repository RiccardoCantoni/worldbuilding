using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessor {

	public static int[] getDistribution(float[] values, float lowerBound, float upperBound, int countDistinct){
		int[] counts = new int[countDistinct+1];
		int i;
		foreach (float val in values) {
			i = (int)Mathf.Round (100 * (val - lowerBound));
			if (i <=countDistinct && i>=0) {
				counts [i]++;
			}

		}
		return counts;
	}
}
