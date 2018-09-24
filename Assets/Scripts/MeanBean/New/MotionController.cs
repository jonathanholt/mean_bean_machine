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
		if (Input.GetKeyDown ("down")) {
			GameObject beanArray = GameObject.Find ("allbeans");
			foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					child.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
					child.GetComponent<Animator> ().SetBool ("falling", true);
				}
			}
		}

		if (Input.GetKeyUp ("down")) {
			GameObject beanArray = GameObject.Find ("allbeans");
			foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					child.GetComponent<Animator> ().SetBool ("falling", false);
				}
			}
		}


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
		if (Input.GetKeyDown ("a")) {
			GameObject beanArray = GameObject.Find ("allbeans");
			foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					switch (child.GetComponent<Bean>().getRotationInt() % 4)
					{
					case 0:
						child.transform.position -= Vector3.left * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						break;
					case 1:
						child.transform.position -= Vector3.right * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						break;
					case 2:
						child.transform.position -= Vector3.right * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						break;
					case 3:
						child.transform.position -= Vector3.left * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						break;
					default:
						//transform.position = startPoint.transform.position;
						break;
					}
					child.GetComponent<Bean> ().incrementRotationInt ();
					break;
				}
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
