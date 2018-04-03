using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latitude {

	static float[] LinearMonthlyShifts = { -2, -1, 0, 1, 2, 3, 2, 1, 0, -1, -2, -3};

	public static float getLatitude(int yValue, int ySize){
		return Latitude.getLatitude ((float)yValue / (float)ySize);
	}

	public static float monthlyShift(int month, float delta){
		return (LinearMonthlyShifts [month%12]*delta);
	}


	public static float getLatitude(float ratio){
		return ratio * 180 - 90;
	}

	public static float getLatitudeOld(float yRatio){
		float x = (Mathf.Lerp(0,180,yRatio)) - 90;
		IFunction circle = new CircleAbsolute (90);
		if (x < 0) {
			return circle.calculate (x) - 90;
		}
		return 90 - circle.calculate (x);
	}


}
