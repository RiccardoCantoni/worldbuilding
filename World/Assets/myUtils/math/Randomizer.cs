using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer {

	public static float NextGaussian(float mean = 0, float stdDev = 1)
	{
		float u1 = Random.value;
		float u2 = Random.value;

		float rand_std_normal = Mathf.Sqrt (-2f * Mathf.Log (u1)) * Mathf.Sin (2f * Mathf.PI * u2);
		float rand_normal = mean + stdDev * rand_std_normal;
		return rand_normal;
	}
}
