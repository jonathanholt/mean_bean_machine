using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

	public bool inMotion;
	
	void Start () {
		inMotion = true;	
	}
	
	public void setMotion(bool motion){
		inMotion = motion;
	}

	public bool getMotion(){
		return inMotion;
	}
}
