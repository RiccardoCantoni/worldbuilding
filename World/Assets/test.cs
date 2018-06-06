using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour{


	void Start(){

        Point p = new Point(100, 100);
        Direction d = Direction.North();
        Debug.Log(p.moveBy(d.rotateClockwise()));
        Debug.Log(p.moveBy(d.rotateCounterClockwise()));
        Debug.Log(p.moveBy(d));

    }


}
