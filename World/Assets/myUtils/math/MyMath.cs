﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath  {

	public static float remap (float x, float a1, float b1, float a2, float b2){
		float oldGap = b1 - a1;
		float t = (x - a1) / oldGap;
		float newGap = b2 - a2;
		return t * newGap + a2;
	}
}
