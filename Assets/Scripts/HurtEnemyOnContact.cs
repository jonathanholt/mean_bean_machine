using UnityEngine;
using System.Collections;

public class HurtEnemyOnContact : MonoBehaviour {

	public int damageToGive;
	public float bounceOnEnemy;
	private Rigidbody2D myrigidbody2d;

	// Use this for initialization
	void Start () {
		myrigidbody2d = transform.parent.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Enemy"){
			other.GetComponent<EnemyHealthManager> ().giveDamage (damageToGive);
			myrigidbody2d.velocity = new Vector2 (myrigidbody2d.velocity.x, bounceOnEnemy);
		}
	}
}
