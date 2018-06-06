using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAbsolute : IFunction {

	float radius;

	public CircleAbsolute (float radius){
		this.radius = radius;
	}
	
	public float calculate(float x){
		return Mathf.Sqrt (Mathf.Pow (radius, 2) - Mathf.Pow (x, 2));
	}
}
