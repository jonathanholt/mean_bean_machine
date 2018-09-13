using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

	public bool inMotion;
	public GameObject[] startPoints;
	int currentPosition = 2;
	float movementShiftValue = 0.63f;
	
	void Start () {
		inMotion = true;	
	}
	
	public void setMotion(bool motion){
		inMotion = motion;
	}

	public bool getMotion(){
		return inMotion;
	}
	
	public void resetCurrentPosition(int resetValue){
		currentPosition = resetValue;
	}
	
	void Update () {
		if (Input.GetKeyDown ("left")) {
				if(currentPosition - 1 != -1){
					currentPosition -= 1;
					GameObject moveToPosition = startPoints[currentPosition];
					moveBothBeans(moveToPosition, "left");
				}
			}
			if (Input.GetKeyDown ("right")) {
				if(currentPosition + 1 != 5){
					currentPosition += 1;
					GameObject moveToPosition = startPoints[currentPosition];
					moveBothBeans(moveToPosition, "right");
				}
			}
	}
	
	public void moveBothBeans(GameObject destination, string direction){
		GameObject beanArray = GameObject.Find("allbeans");
		foreach (Transform child in beanArray.transform) {
			if(child.GetComponent<Bean> ().getInPlay() != 0){
				if(direction == "right")
					child.transform.position -= Vector3.left * movementShiftValue;
				else
					child.transform.position -= Vector3.right * movementShiftValue;
			}
		}
	}
}
