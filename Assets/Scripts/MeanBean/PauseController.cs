using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

	public static bool isPaused;

	void Start () {
		isPaused = false;	
	}
	

	void Update () {
		if (isPaused) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	public static void togglePause(){
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}
}
