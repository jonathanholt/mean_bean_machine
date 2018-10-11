using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionObserver : MonoBehaviour {

	public GameObject beanArray;

	void Start(){
		Debug.Log ("MotionController Start()");
	}

	void Update () {
		int count = 0;
		int stoppedCount = 0;
		foreach (Transform child in beanArray.transform) {
			count += 1;
			if(!child.GetComponent<Bean> ().inMotion){
				stoppedCount += 1;
			}
		}
		if(count == stoppedCount){
			Debug.Log("Nothing moving");
			beanArray.GetComponent<BeanFactory> ().createNext ();
		}
	}
}
