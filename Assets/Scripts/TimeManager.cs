using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public float startingTime;
	private Text theText;
	private PauseMenu pauseMenu;
	//public GameObject gameOverScreen;
	//public PlayerController player;
	private HealthManager theHealth;
	private float countingTime;

	// Use this for initialization
	void Start () {
		theHealth = FindObjectOfType<HealthManager> ();
		countingTime = startingTime;
		theText = GetComponent<Text> ();
		pauseMenu = FindObjectOfType<PauseMenu> ();
		//player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pauseMenu.isPaused) {
			return;
		}
		countingTime -= Time.deltaTime;
		if (countingTime <= 0) {
			//gameOverScreen.SetActive (true);
			//player.gameObject.SetActive (false);
			theHealth.KillPlayer ();
		}
		theText.text = "" + Mathf.Round(countingTime);
	}

	public void ResetTime(){
		countingTime = startingTime;
	}
}
