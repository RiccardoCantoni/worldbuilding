using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayUtil<T> {

	public static int argMin(T[] array, System.Func<T,float> comparator)
    {
        float minv=float.MaxValue, v;
        int mini=-1;
        for (int i=0; i<array.Length; i++)
        {
            v = comparator(array[i]);
            if (v <= minv)
            {
                minv = v;
                mini = i;
            }
        }
        if (mini < 0)
        {
            throw new System.Exception("argmin failed: array size "+array.Length);
        }
        return mini;
    }

    public static int argMax(T[] array, System.Func<T, float> comparator)
    {
        float maxv = float.MinValue, v;
        int maxi=-1;
        for (int i = 0; i < array.Length; i++)
        {
            v = comparator(array[i]);
            if (v > maxv)
            {
                maxv = v;
                maxi = i;
            }
        }
        return maxi;
    }

    public static float min(T[] array, System.Func<T, float> comparator)
    {
        float minv = float.MaxValue, v;
        for (int i = 0; i < array.Length; i++)
        {
            v = comparator(array[i]);
            if (v < minv)
            {
                minv = v;
            }
        }
        return minv;
    }

    public static float max(T[] array, System.Func<T, float> comparator)
    {
        float maxv = float.MinValue, v;
        for (int i = 0; i < array.Length; i++)
        {
            v = comparator(array[i]);
            if (v > maxv)
            {
                maxv = v;
            }
        }
        return maxv;
    }
}
