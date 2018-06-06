using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFunction : IFunction{

	private float mean;
	private float stdDev;

	public NormalFunction(float mean, float stdDev){
		this.mean = mean;
		this.stdDev = stdDev;
	}

	public float calculate(float x){
		float variance = Mathf.Pow (stdDev, 2f);
		float a = Mathf.Sqrt (2 * Mathf.PI * variance);
		float b = (-1) * Mathf.Pow ((x - mean), 2f) / (2f * variance);

		return (1 / a) * Mathf.Exp (b);
	}


}
