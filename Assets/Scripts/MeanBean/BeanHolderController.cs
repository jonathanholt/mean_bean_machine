using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanHolderController : MonoBehaviour {

	public GameObject parentPlayer;

	void Start () {
		parentPlayer = GameObject.Find("Player");
	}

	public void OnCollisionEnter2D(Collision2D other){
		parentPlayer.GetComponent<PlayerController> ().childCollision ();
	}
}
