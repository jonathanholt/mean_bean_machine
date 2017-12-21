using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; 

public class LevelLoader : MonoBehaviour {

	private bool playerInZone;
	public string levelToLoad;
	public string levelTag;
	public int justCompleted;
	public Game theGame;
	private PlayerController player;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		Debug.Log ("STARTED");
		if (audioSource) {
			audioSource.transform.position = transform.position;
			audioSource.Pause ();
		}
		playerInZone = false;
		player = FindObjectOfType<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetAxisRaw ("Vertical") > 0) && playerInZone) {
			Debug.Log ("Hello");
			levelEnd ();

			SaveLoad.Load (); 
			theGame = SaveLoad.savedGame;

			theGame.progress.time += 1;
			string levelName = SceneManager.GetActiveScene ().name;
			int position = theGame.progress.levels.IndexOf ("level1");
			theGame.progress.completed [position] = 1;
			theGame.progress.completed[position + 1] = 1;
			theGame.progress.score = theGame.progress.score + 100;
			SaveLoad.Save(theGame);
			PlayerPrefs.SetInt (levelTag, 1);
			Application.LoadLevel (levelToLoad);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.name == "Player"){
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.name == "Player"){
			playerInZone = false;
		}
	}

	void levelEnd(){
		if(audioSource)
			audioSource.Play ();
	}
}
