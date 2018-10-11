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
		float directionOriginOffset = (direction.x > 0 ? 0.35f :  -0.35f);
		Vector2 startingPosition = new Vector2(transform.position.x + directionOriginOffset, transform.position.y);
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
		if (objectCollidedWith.Contains (this.name.Substring(0, this.name.Length - 1))) {
			this.transform.parent = otherColliderGameObject.transform;
			if (debugMessage != null)
				Debug.Log (debugMessage);
		}
	}

	public void OnCollisionEnter2D(Collision2D other){
		inPlay = 0;
		this.GetComponent<Animator> ().SetBool ("collision", true);
		this.collisionMatchChecker (other.collider.gameObject, "Collision with bottom");

		//float height = this.GetComponent<SpriteRenderer> ().bounds.size.y;
		//this.GetComponent<BoxCollider2D> ().size = new Vector3(0.62f, height, height);
		Vector2 direction = new Vector2(1, 0);
		RaycastHit2D hit = this.CheckRaycast(direction);
		if (hit) {	
			this.collisionMatchChecker (hit.collider.gameObject, "Raycast hit right");
		}
		direction = new Vector2(-1, 0);
		hit = this.CheckRaycast (direction);
		if (hit) {
			this.collisionMatchChecker (hit.collider.gameObject, "Raycast hit left");
		}
		if(Player)
			Player.GetComponent<MotionController> ().setMotion (false);

		StartCoroutine(BeanStopped (waitingTime));
		StartCoroutine(GameHaltedChecked (waitingTime));
		StartCoroutine(RoundOverCheck (waitingTime * 4));
		StartCoroutine (StopAnimation(4.0f));
	}
	
	public void DeleteAnyBeans(){
		foreach (Transform child in beanArray.transform) {
			int allChildCount = this.checkAllChildren (child);
				if (allChildCount >= 4) {
					Destroy (child.gameObject);
					Player.GetComponent<AvalancheController> ().incrementAvalancheCount ();
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
				Player.GetComponent<AvalancheController> ().processAvalanche ();
				beanArray.GetComponent<BeanFactory> ().createNext ();
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
