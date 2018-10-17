using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour{


	void Start(){
        int ySize = 256;
        for (int y=0; y<ySize; y++)
        {
            TradeWindGenerator.cellIndex(y, ySize, 3);
        }
        

    }


}
