using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind  {

	public float speed;
	public Direction direction;

	public Wind(Direction d, float s){
		this.direction = d;
		this.speed = s;
	}

    public static Wind fromVector(Vector2 v)
    {
        float mag = Mathf.Min(v.magnitude, 60);
        mag = Mathf.Max(v.magnitude, 0);
        return new Wind(Direction.fromVector(v), mag);
    }

    public static Wind weightedSum(Wind a, float weighta, Wind b, float weightb)
    {
        Vector2 va = a.direction.toVector() * a.speed;
        Vector2 vb = b.direction.toVector() * b.speed;
        return Wind.fromVector((va * weighta + vb * weightb) / (weighta+weightb));
    }

}
