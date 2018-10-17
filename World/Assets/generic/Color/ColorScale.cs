using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScale {
    HSVColor min, max;
    float minValue, maxValue;

	public ColorScale(Color min, Color max, float minValue, float maxValue)
    {
        if (minValue >= maxValue)
        {
            throw new System.Exception("invalid parameters");
        }
        this.min = new HSVColor(min);
        this.max = new HSVColor(max);
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

    public Color getColor(float value)
    {
        if (value > maxValue  || value < minValue)
        {
            throw new System.Exception("invalid parameters: ["+min+","+max+"] "+value);
        }
        return HSVColor.lerpHue(min, max, (value - minValue) / (maxValue - minValue)).toRGB();
    }
}
