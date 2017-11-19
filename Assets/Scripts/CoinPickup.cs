using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int pointsToAdd;
	public AudioSource coinPickupSound;

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerController>() == null)
			return;

		ScoreManager.AddPoints(pointsToAdd);
		coinPickupSound.Play ();
		Destroy (gameObject);
			
	}
}
