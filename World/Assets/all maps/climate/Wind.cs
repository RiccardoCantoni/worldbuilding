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

	public static Wind add(Wind w1, Wind w2){
		return new Wind (w1.speed > w2.speed ? w1.direction : w2.direction, (w1.speed + w2.speed));
	}
}
