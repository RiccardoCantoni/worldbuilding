using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshotClicker : MonoBehaviour {

	void Update(){
		if (Input.GetMouseButtonDown(1)) {
			ScreenCapture.CaptureScreenshot (Random.value + "x" + Random.value + "x" + Random.value +".png");
		}
	}

}
