using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVColor {

	private float h, s, v;

	public HSVColor(float h, float s, float v){
		this.h = h;
		this.s = s;
		this.v = v;
	}

	public HSVColor(Color c){
		Color.RGBToHSV (c, out h, out s, out v);
	}

	public Color toRGB(){
		return Color.HSVToRGB (this.h, this.s, this.v);
	}

	public static HSVColor lerpHue(HSVColor a, HSVColor b, float t){
		return new HSVColor (Mathf.Lerp (a.h, b.h, t), a.s, a.v);
	}




}
