using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour {

	private int lifeCounter;
	//public int startingLives;
	private Text theText;
	public GameObject gameOverScreen;
	public PlayerController player;
	public string mainMenu;
	public float waitAfterGameOver;

	// Use this for initialization
	void Start () {
		theText = GetComponent<Text> ();
		lifeCounter = PlayerPrefs.GetInt("PlayerCurrentLives");
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeCounter == 0) {
			gameOverScreen.SetActive (true);
			player.gameObject.SetActive (false);
		}
		theText.text = "x " + lifeCounter;

		if (gameOverScreen.activeSelf) {
			waitAfterGameOver -= Time.deltaTime;
		}

		if (waitAfterGameOver < 0) {
			SceneManager.LoadScene (mainMenu);
		}
	}

	public void giveLife(){
		lifeCounter++;
		PlayerPrefs.SetInt ("PlayerCurrentLives", lifeCounter);
	}

	public void takeLife(){
		lifeCounter--;
		PlayerPrefs.SetInt ("PlayerCurrentLives", lifeCounter);
	}

}
