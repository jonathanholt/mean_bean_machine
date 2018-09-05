using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour {

	private static Object [] sprites;
	public int colour;
	public int size;
	private GameObject startPoint;
	public GameObject beanArray;
	public GameObject Player;
	public bool anyBeansFalling = true;
	public float waitingTime;

	void Start () {
		sprites = Resources.LoadAll ("beans");
		size = 0;
		waitingTime = 0.5f;
		this.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [colour];
		this.transform.parent = beanArray.transform;
	}

	void Update(){
		if(this.GetComponent<Rigidbody2D>().velocity.y != 0){
			Player.GetComponent<PlayerController> ().setMotion (true);
		}
	}

	public void moveToPosition(){
		startPoint = GameObject.Find("StartPoint");
		transform.position = startPoint.transform.position;
	}

	public void OnCollisionEnter2D(Collision2D other){
		Player.GetComponent<PlayerController> ().setMotion (false);
		string objectCollidedWith = other.collider.gameObject.name;

		if (objectCollidedWith.Contains (this.name.Substring(0, this.name.Length - 1))) {
			this.transform.parent = other.collider.gameObject.transform;
			string finalCharacter = this.name.Substring(other.collider.gameObject.name.Length - 1);
			int finalCharacterNumber = int.Parse (finalCharacter);
			finalCharacterNumber ++;
			string iteratedName = this.name.Substring (0, other.collider.gameObject.name.Length - 1) + finalCharacterNumber;
			this.name = iteratedName;

		}

		float height = this.GetComponent<SpriteRenderer> ().bounds.size.x;
		float width = this.GetComponent<SpriteRenderer> ().bounds.size.y + 0.03f;
		this.GetComponent<BoxCollider2D> ().size = new Vector3(width, height, width);
		width = this.GetComponent<SpriteRenderer> ().bounds.size.y - 0.03f;
		this.GetComponent<BoxCollider2D> ().size = new Vector3(width, height, width);
		Player.GetComponent<PlayerController> ().setMotion (false);

		StartCoroutine(BeanStopped (waitingTime));
	}
	
	public void DeleteAnyBeans(){
		foreach (Transform child in beanArray.transform) {
				if (int.Parse (child.gameObject.name.Substring (child.gameObject.name.Length - 1)) >= 4) {
					Destroy (child.gameObject);
				}
			}
	}

	IEnumerator BeanStopped(float delayTime){
		yield return new WaitForSeconds (delayTime);
		Player.GetComponent<PlayerController> ().setMotion (false);
		StartCoroutine(GameHaltedChecked (waitingTime));
	}
	
	IEnumerator GameHaltedChecked(float delayTime){
		yield return new WaitForSeconds (delayTime);
		anyBeansFalling = Player.GetComponent<PlayerController> ().getMotion ();
		if(!anyBeansFalling)
			DeleteAnyBeans();
	}
}
