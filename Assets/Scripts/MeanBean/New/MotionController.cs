using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

	public bool inMotion;
	public GameObject[] startPoints;
	int currentPosition = 2;
	float movementShiftValue = 0.53f;
	float rotationtShiftValue = 0.555f;
	public int lastDirection;
	public GameObject beanArray;
	public bool isPlayer;
	
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
		if(beanArray.GetComponent<BeanFactory>().canMove){
		if(isPlayer){
		if (Input.GetKeyUp ("z")) {
			PauseController.togglePause ();
		}
		
		if (Input.GetKeyDown ("down")) {
			foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					child.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
					child.GetComponent<Animator> ().SetBool ("falling", true);
				}
			}
		}

		if (Input.GetKeyUp ("down")) {
			moveFaster();
		}

		if (Input.GetKeyDown ("left")) {			
			moveLeft();
		}
		
			if (Input.GetKeyDown ("right")) {
				moveRight();
			}
			
		if (Input.GetKeyDown ("a")) {
			rotateClockwise();
		}
		
		if (Input.GetKeyDown ("s")) {
			rotateAntiClockwise();
		}
		}
		}
	}
	
	public void moveBothBeans(GameObject destination, string direction){
		//Debug.Log("MOVEBOTHBEANS");
		foreach (Transform child in beanArray.transform) {
			//Debug.Log("INPLAY value = "+child.GetComponent<Bean> ().getInPlay());
			if(child.GetComponent<Bean> ().getInPlay() != 0){
				//Debug.Log("MOVING BEANS IN "+direction+" direction by "+movementShiftValue);
				if(direction == "right")
					child.transform.position -= Vector3.left * movementShiftValue;
				else
					child.transform.position -= Vector3.right * movementShiftValue;
			}
		}
	}
	
	public void rotateClockwise(){
		foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					switch (child.GetComponent<Bean>().getRotationInt() % 4)
					{
					case 0:
					////Debug.Log("A RIGHT");
					Vector2 directionright = new Vector2(1, 0);
						RaycastHit2D hitright = child.GetComponent<Bean> ().CheckRaycast(directionright);
						if(hitright.collider && hitright.collider.GetComponent<Bean> ().getInPlay() == 0){
							////Debug.Log("POINT A");
							////Debug.Log("Hit the collidable object " + hitright.collider.name);
							break;
						}
						else{
							//Debug.Log("POINT B");
					if(child.GetComponent<Bean> ().getHorizontalPosition () < 2){
						////Debug.Log("POINT C");
						child.transform.position -= Vector3.left * rotationtShiftValue;
						child.transform.position += Vector3.up * rotationtShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
					
					}
						}
						break;
					case 1: case -3:
						////Debug.Log("A UP");
						child.transform.position -= Vector3.right * rotationtShiftValue;
						child.transform.position += Vector3.up * rotationtShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
						break;
					case 2: case -2:
					////Debug.Log("A LEFT");
					////Debug.Log(child.GetComponent<Bean> ().getHorizontalPosition ());
					Vector2 directionleft = new Vector2(-1, 0);
						RaycastHit2D hitleft = child.GetComponent<Bean> ().CheckRaycast(directionleft);
						if(hitleft.collider && hitleft.collider.GetComponent<Bean> ().getInPlay() == 0){
							////Debug.Log("Hit the collidable object " + hitleft.collider.name);
							break;
						}
						else{
					if(child.GetComponent<Bean> ().getHorizontalPosition () > -2){
						child.transform.position -= Vector3.right * rotationtShiftValue;
						child.transform.position += Vector3.down * rotationtShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
					}
						}
						break;
					case 3: case -1:
					////Debug.Log("A DOWN");
						child.transform.position -= Vector3.left * rotationtShiftValue;
						child.transform.position += Vector3.down * rotationtShiftValue;
						child.GetComponent<Bean> ().incrementRotationInt ();
						break;
					default:
						break;
					}
					break;
				}
			}
	}
	
	public void rotateAntiClockwise(){
		foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					switch (child.GetComponent<Bean>().getRotationInt() % 4)
					{
					case 0:
					//////Debug.Log("S LEFT");
					Vector2 directionleft = new Vector2(-1, 0);
						RaycastHit2D hitleft = child.GetComponent<Bean> ().CheckRaycast(directionleft);
						if(hitleft.collider && hitleft.collider.GetComponent<Bean> ().getInPlay() == 0){
							//////Debug.Log("Hit the collidable object " + hitleft.collider.name);
							break;
						}
						else{
					if(child.GetComponent<Bean> ().getHorizontalPosition () > -2){
						child.transform.position += Vector3.left * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
					}
						}
						break;
					case -1: case 3:
					//////Debug.Log("S UP " + (child.GetComponent<Bean>().getRotationInt() % 4));
					child.transform.position += Vector3.right * movementShiftValue;
						child.transform.position += Vector3.up * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
						break;
					case 2: case -2:
					//////Debug.Log("S RIGHT");
					Vector2 directionright = new Vector2(1, 0);
						RaycastHit2D hitright = child.GetComponent<Bean> ().CheckRaycast(directionright);
						if(hitright.collider && hitright.collider.GetComponent<Bean> ().getInPlay() == 0){
							//////Debug.Log("Hit the collidable object " + hitright.collider.name);
							break;
						}
						else{
					if(child.GetComponent<Bean> ().getHorizontalPosition () < 2){
						child.transform.position += Vector3.right * movementShiftValue;
						child.transform.position += Vector3.down * movementShiftValue;
						child.GetComponent<Bean> ().pedalBackRotationInt ();
					}
						}
						break;
					case -3: case 1:
					//////Debug.Log("S DOWN");
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
	
	public void moveRight(){
		//Debug.Log("Moving Right AI!");
		if(currentPosition + 1 != 6){
			//Debug.Log("Current position = "+currentPosition);
					bool moveEnabled = true;
					foreach (Transform child in beanArray.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						Vector2 direction = new Vector2(-1, 0);
						RaycastHit2D hit = child.GetComponent<Bean> ().CheckRaycast(direction);
						if(hit.collider && hit.collider.GetComponent<Bean> ().getInPlay() == 0){
							//Debug.Log("MOVE NOT ENABLED 1");
							//////Debug.Log("Hit the collidable object " + hit.collider.name);
							moveEnabled = false;
						}
						else{
						int rotationInt = (int) (child.GetComponent<Bean>().getRotationInt() % 4);
						//////Debug.Log(rotationInt);
						if((rotationInt == -3 || rotationInt == 1) && 
						child.GetComponent<Bean>().getHorizontalPosition() == 1){
							//Debug.Log("MOVE NOT ENABLED 2");
							moveEnabled = false;	
						}
						}
					}
				}
				if(moveEnabled){
					//Debug.Log("MOVE ENABLED");
					currentPosition += 1;
					GameObject moveToPosition = startPoints[currentPosition];
					moveBothBeans(moveToPosition, "right");
	
					foreach (Transform child in beanArray.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						child.GetComponent<Bean> ().incrementHorizontalPosition ();
					}
				}
				}
				
				
				}
	}
	
	public void moveLeft(){
						if(currentPosition - 1 != -1){
					bool moveEnabled = true;
					foreach (Transform child in beanArray.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						
						Vector2 direction = new Vector2(-1, 0);
						RaycastHit2D hit = child.GetComponent<Bean> ().CheckRaycast(direction);
						if(hit.collider && hit.collider.GetComponent<Bean> ().getInPlay() == 0){
							//////Debug.Log("Hit the collidable object " + hit.collider.name);
							moveEnabled = false;
						}
						else{
								int rotationInt = (int) (child.GetComponent<Bean>().getRotationInt() % 4);
						if((rotationInt == 3 || rotationInt == -1) && 
						child.GetComponent<Bean>().getHorizontalPosition() == -1){
							moveEnabled = false;	
						}
						}
					}
				}
				if(moveEnabled){
					currentPosition -= 1;
				GameObject moveToPosition = startPoints[currentPosition];
				moveBothBeans(moveToPosition, "left");
				foreach (Transform child in beanArray.transform) {
					if (child.GetComponent<Bean> ().getInPlay () != 0) {
						child.GetComponent<Bean> ().decrementHorizontalPosition ();
						//////Debug.Log(child.GetComponent<Bean> ().getHorizontalPosition ());
					}	
				}
				}
				
			}
	}
	
	public void moveFaster(){
		foreach (Transform child in beanArray.transform) {
				if (child.GetComponent<Bean> ().getInPlay () != 0) {
					child.GetComponent<Animator> ().SetBool ("falling", false);
				}
			}
	}
}
