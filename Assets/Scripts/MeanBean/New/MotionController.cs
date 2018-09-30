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
					bool moveEnabled = true;
					GameObject beanArray1 = GameObject.Find ("allbeans");
					foreach (Transform child in beanArray1.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						int rotationInt = (int) (child.GetComponent<Bean>().getRotationInt() % 4);
						if((rotationInt == 3 || rotationInt == -1) && 
						child.GetComponent<Bean>().getHorizontalPosition() == -1){
							moveEnabled = false;	
						}
					}
				}
				
				
				if(moveEnabled){
					currentPosition -= 1;
				GameObject moveToPosition = startPoints[currentPosition];
				moveBothBeans(moveToPosition, "left");
				foreach (Transform child in beanArray1.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						child.GetComponent<Bean> ().decrementHorizontalPosition ();
						Debug.Log(child.GetComponent<Bean> ().getHorizontalPosition ());
					}	
				}
				}
				
			}
		}
			if (Input.GetKeyDown ("right")) {
				if(currentPosition + 1 != 5){
					bool moveEnabled = true;
					GameObject beanArray1 = GameObject.Find ("allbeans");
					foreach (Transform child in beanArray1.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						int rotationInt = (int) (child.GetComponent<Bean>().getRotationInt() % 4);
						Debug.Log(rotationInt);
						if((rotationInt == -3 || rotationInt == 1) && 
						child.GetComponent<Bean>().getHorizontalPosition() == 1){
							moveEnabled = false;	
						}
					}
				}
				if(moveEnabled){
					currentPosition += 1;
					GameObject moveToPosition = startPoints[currentPosition];
					moveBothBeans(moveToPosition, "right");
					
					
										GameObject beanArray = GameObject.Find ("allbeans");
					foreach (Transform child in beanArray.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						child.GetComponent<Bean> ().incrementHorizontalPosition ();
					}
				}
				}
				
				
				}
			}
		if (Input.GetKeyDown ("a")) {
			GameObject beanArray = GameObject.Find ("allbeans");
			foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					switch (child.GetComponent<Bean>().getRotationInt() % 4)
					{
					case 0:
					Debug.Log("A RIGHT");
					if(child.GetComponent<Bean> ().getHorizontalPosition () < 2){
						child.transform.position -= Vector3.left * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
					}
						break;
					case 1: case -3:
					Debug.Log("A UP");
						child.transform.position -= Vector3.right * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
						break;
					case 2: case -2:
					Debug.Log("A LEFT");
					Debug.Log(child.GetComponent<Bean> ().getHorizontalPosition ());
					if(child.GetComponent<Bean> ().getHorizontalPosition () > -2){
						child.transform.position -= Vector3.right * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
					}
						break;
					case 3: case -1:
					Debug.Log("A DOWN");
						child.transform.position -= Vector3.left * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
						break;
					default:
						break;
					}
					break;
				}
			}
		}
		
		if (Input.GetKeyDown ("s")) {
			GameObject beanArray = GameObject.Find ("allbeans");
			foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					switch (child.GetComponent<Bean>().getRotationInt() % 4)
					{
					case 0:
					Debug.Log("S LEFT");
					if(child.GetComponent<Bean> ().getHorizontalPosition () > -2){
						child.transform.position += Vector3.left * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
					}
						break;
					case -1: case 3:
					Debug.Log("S UP " + (child.GetComponent<Bean>().getRotationInt() % 4));
					child.transform.position += Vector3.right * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
						break;
					case 2: case -2:
					Debug.Log("S RIGHT");
					if(child.GetComponent<Bean> ().getHorizontalPosition () < 2){
						child.transform.position += Vector3.right * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
					}
						break;
					case -3: case 1:
					Debug.Log("S DOWN");
						child.transform.position += Vector3.left * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
						break;
					default:
						//transform.position = startPoint.transform.position;
						break;
					}
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
