using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	private PlayerController player;
	public GameObject deathParticle;
	public GameObject respawnParticle;
	public float respawnDelay;
	private CameraController camera;
	public int pointPenaltyOnDeath;
	private float gravityStore;
	public HealthManager healthManager;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController>();
		camera = FindObjectOfType<CameraController> ();
		healthManager = FindObjectOfType<HealthManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RespawnPlayer(){
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo(){
		Debug.Log("Player"+player.enabled );
		player.enabled = false;
		Debug.Log("Player"+player.enabled );
		player.GetComponent<CircleCollider2D>().enabled = false;
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		player.GetComponent<Renderer> ().enabled = false;
		camera.isFollowing = false;
		//player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		//gravityStore = player.GetComponent<Rigidbody2D> ().gravityScale;
		//player.GetComponent<Rigidbody2D> ().gravityScale = 5;
		ScoreManager.AddPoints (pointPenaltyOnDeath);
		Debug.Log("Player Respawn");
		yield return new WaitForSeconds (respawnDelay);
		player.GetComponent<CircleCollider2D>().enabled = true;
		//player.GetComponent<Rigidbody2D> ().gravityScale = gravityStore;
		Application.LoadLevel(Application.loadedLevel);
		player.transform.position = currentCheckpoint.transform.position;
		camera.isFollowing = true;
		player.enabled = true;
		player.GetComponent<Renderer> ().enabled = true;
		healthManager.FullHeath ();
		healthManager.isDead = false;
		Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
}
}
