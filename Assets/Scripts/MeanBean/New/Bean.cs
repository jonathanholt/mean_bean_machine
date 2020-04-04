using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour {

	public int size;
	public GameObject beanArray;
	public GameObject Player;
	public bool anyBeansFalling = true;
	public float waitingTime;
	public int inPlay;
	public int position;
	public int rotationInt = 0;
	public int horizontalPosition;
	public float raycastMaxDistance = 0.5f;
	public bool inMotion = true;

	void Update(){
		
		if(inPlay != 0 && beanArray.GetComponent<BeanFactory>().isGameOver){
			Destroy(gameObject);
		}

		if (Input.GetKeyDown ("x")) {
			float directionOriginOffset = (0.35f);
			Vector2 direction = new Vector2 (1, 0);
			Vector2 startingPosition = new Vector2(transform.position.x + directionOriginOffset, transform.position.y);
			Debug.DrawRay(startingPosition, direction * raycastMaxDistance, Color.red);
		}

		if (Input.GetKeyDown ("c")) {
			float directionOriginOffset = (-0.35f);
			Vector2 direction = new Vector2 (-1, 0);
			Vector2 startingPosition = new Vector2(transform.position.x + directionOriginOffset, transform.position.y);
			Debug.DrawRay(startingPosition, direction * raycastMaxDistance, Color.red);
		}



		if(this.GetComponent<Rigidbody2D>().velocity.y != 0 && Player){
			Player.GetComponent<MotionController> ().setMotion (true);
			this.inMotion = true;
			}
			else{
				this.inMotion = false;
			}
		}
	
	public RaycastHit2D CheckRaycast(Vector2 direction){
		Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
		if(!(direction.y > 1)){
			float directionOriginOffset = (direction.x > 0 ? 0.35f :  -0.35f);
			startingPosition = new Vector2(transform.position.x + directionOriginOffset, transform.position.y);
		}
		Debug.DrawRay(startingPosition, direction * raycastMaxDistance, Color.red);
		return Physics2D.Raycast(startingPosition, direction, raycastMaxDistance);
	}
	
	void Start () {
		horizontalPosition = 0;
		size = 0;
		waitingTime = 0.5f;
		this.transform.parent = beanArray.transform;
	}
	

	public void setInPlay(int beanNumber){
		inPlay = beanNumber;
	}
	
	public void SetPlayer(GameObject thePlayer){
		Player = thePlayer;
	}

	public void SetBeanArray(GameObject arrayofBeans){
		beanArray = arrayofBeans;
	}
	
	public int getInPlay(){
		return inPlay;
	}

	public void incrementRotationInt(){
		rotationInt += 1;
	}
	
	public void incrementHorizontalPosition(){
		horizontalPosition += 1;
	}
	
	public void decrementHorizontalPosition(){
		horizontalPosition -= 1;
	}
	
	public int getHorizontalPosition(){
		return horizontalPosition;
	}
	
	public void pedalBackRotationInt(){
		rotationInt -= 1;
	}

	public int getRotationInt(){
		return rotationInt;
	}

	public void collisionMatchChecker(GameObject otherColliderGameObject, string debugMessage = null){
		string objectCollidedWith = otherColliderGameObject.name;

		if (objectCollidedWith.Contains (this.name.Substring(0, this.name.Length - 1)) && debugMessage != "up") {
			this.transform.parent = otherColliderGameObject.transform;
		}
	}

	public void OnCollisionEnter2D(Collision2D other){
		//if(inPlay == 1 && this.gameObject.transform.position.y > 1.69f){
		//	beanArray.GetComponent<BeanFactory>().isGameOver = true;
		//	beanArray.GetComponent<BeanFactory>().gameOver(Player.name);
		//}
		
		inPlay = 0;
		this.GetComponent<Animator> ().SetBool ("collision", true);
		if(this.name != "grey1"){
		//this.collisionMatchChecker (other.collider.gameObject, "Collision with bottom");
		Vector2 direction = new Vector2(1, 0);
		RaycastHit2D hit = this.CheckRaycast(direction);
		if (hit) {	
			//this.collisionMatchChecker (hit.collider.gameObject, "Raycast hit right");
		}
		direction = new Vector2(-1, 0);
		hit = this.CheckRaycast (direction);
		if (hit) {
			//this.collisionMatchChecker (hit.collider.gameObject, "Raycast hit left");
		}
		if(Player)
			Player.GetComponent<MotionController> ().setMotion (false);
		
		}
		StartCoroutine(BeanStopped (waitingTime));
		StartCoroutine(GameHaltedChecked (waitingTime));
		StartCoroutine(RoundOverCheck (waitingTime * 4));
		StartCoroutine (StopAnimation(4.0f));
	}
	
	public void DeleteAnyBeans(){
		bool anyBeansDeleted = false;
		foreach (Transform child in beanArray.transform) {
			int allChildCount = this.checkAllChildren (child);
				if (allChildCount >= 4) {
					anyBeansDeleted = true;
					this.checkNuisance(child.gameObject);
					Destroy (child.gameObject);
				}
			}
			if(anyBeansDeleted)
				Player.GetComponent<AvalancheController> ().incrementAvalancheCount ();
	}
	
	public void checkNuisance(GameObject oldChild){
		Vector2[] startPointArray = {new Vector2(transform.position.x + 0.35f, transform.position.y),
									new Vector2(transform.position.x - 0.35f, transform.position.y),
									new Vector2(transform.position.x, transform.position.y + 0.35f),
									new Vector2(transform.position.x, transform.position.y - 0.35f)};
									
		Vector2[] directionArray = {new Vector2 (1, 0), new Vector2 (-1, 0), new Vector2 (0, -1), new Vector2 (0, 1)}; 							
		foreach(Vector2 startPoint in startPointArray){
			foreach(Vector2 newdirection in directionArray){
		Debug.DrawRay(startPoint, startPoint * raycastMaxDistance, Color.red);
		RaycastHit2D hit = Physics2D.Raycast(startPoint, newdirection, raycastMaxDistance);	
		if(hit){
			string name = hit.collider.gameObject.name;
			if(name.Contains("grey")){
				Destroy (hit.collider.gameObject);
			}
		}
		}
		}
	}
	
	public int checkAllChildren(Transform cluster){
		int counter = 1;
		int clusterCount = cluster.transform.childCount;
		counter += clusterCount;
		if (clusterCount > 0) {
			foreach (Transform child in cluster.transform) {
				int childClusterCount = child.transform.childCount;
				counter += childClusterCount;
				if (childClusterCount > 0) {
					foreach (Transform grandchild in child.transform) {
						int grandchildClusterCount = grandchild.transform.childCount;
						counter += grandchildClusterCount;
					}
				}
			}
		}
		return counter;
	}
	
	IEnumerator RoundOverCheck(float delayTime){
		yield return new WaitForSeconds (delayTime);
		anyBeansFalling = Player.GetComponent<MotionController> ().getMotion ();
		if(!anyBeansFalling){
				Player.GetComponent<AvalancheController> ().queueAvalanche ();
		}
	}

	IEnumerator BeanStopped(float delayTime){
		yield return new WaitForSeconds (delayTime);
		Player.GetComponent<MotionController> ().setMotion (false);
	}

	IEnumerator StopAnimation(float delayTime){
		yield return new WaitForSeconds (delayTime);
		this.GetComponent<Animator> ().SetBool ("collision", false);
	}
	
	IEnumerator GameHaltedChecked(float delayTime){
		yield return new WaitForSeconds (delayTime);
		anyBeansFalling = Player.GetComponent<MotionController> ().getMotion ();
		if(!anyBeansFalling)
			DeleteAnyBeans();
	}
}
