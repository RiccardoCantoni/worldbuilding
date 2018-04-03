using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFunction : IFunction {

	private float intercept;
	private float gradient;

	public LineFunction(float gradient, float intercept){
		this.intercept = intercept;
		this.gradient = gradient;
	}

	public LineFunction(float x1, float y1, float x2, float y2){
		this.gradient = (y2 - y1) / (x2 - x1);
		this.intercept = y1 - gradient * x1;
	}

	public float calculate(float x){
		return gradient * x + intercept;
	}



}
