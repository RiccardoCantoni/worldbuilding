using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaFunction : IFunction {

	private float a, b, c;

	public ParabolaFunction(float a, float b, float c){
		this.a = a;
		this.b = b;
		this.c = c;
	}

	public float calculate(float x){
		return a * x * x + b * x + c;
	}
}
