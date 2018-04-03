using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraClicker : MonoBehaviour {

	public string key;
	public string filename;

	void Update () {
		if (Input.GetKeyDown (key)) {
			Debug.Log ("screenshot taken");
			ScreenCapture.CaptureScreenshot(filename + ".png",2);
		}
	}
}
