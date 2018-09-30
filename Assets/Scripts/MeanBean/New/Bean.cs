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
	
	void Start () {
		horizontalPosition = 0;
		Player = GameObject.Find("Player");
		beanArray = GameObject.Find("allbeans");
		size = 0;
		waitingTime = 0.5f;
		this.transform.parent = beanArray.transform;
	}

	public void setInPlay(int beanNumber){
		inPlay = beanNumber;
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

	void Update(){
		if(this.GetComponent<Rigidbody2D>().velocity.y != 0){
			Player.GetComponent<MotionController> ().setMotion (true);
		}
	}

	public void OnCollisionEnter2D(Collision2D other){
		inPlay = 0;
		this.GetComponent<Animator> ().SetBool ("collision", true);
		string objectCollidedWith = other.collider.gameObject.name;
		if (objectCollidedWith.Contains (this.name.Substring(0, this.name.Length - 1))) {
			this.transform.parent = other.collider.gameObject.transform;
		}
		float height = this.GetComponent<SpriteRenderer> ().bounds.size.y;
		this.GetComponent<BoxCollider2D> ().size = new Vector3(0.62f, height, height);
		Player.GetComponent<MotionController> ().setMotion (false);

		StartCoroutine(BeanStopped (waitingTime));
		StartCoroutine(GameHaltedChecked (waitingTime));
		StartCoroutine(RoundOverCheck (waitingTime));
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
				GameObject BeanController = GameObject.Find("allbeans");
				BeanController.GetComponent<BeanFactory> ().createNext ();
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
