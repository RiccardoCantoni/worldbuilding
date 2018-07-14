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
        return new Wind(Direction.fromVector(v), v.magnitude);
    }

}
