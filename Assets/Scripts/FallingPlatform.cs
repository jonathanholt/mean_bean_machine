using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float fallDelay;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D collided){
		if (collided.transform.name == "Player") {
			StartCoroutine (Fall ());
		}
	}

	IEnumerator Fall(){
		yield return new WaitForSeconds (fallDelay);
		rb2d.isKinematic = false;
		yield return 0;
	}
}
