using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public static int playerHealth;
	public int maxPlayerHealth;
	//Text text;
	public Slider healthBar;
	private LevelManager levelManager;
	public bool isDead;
	private LifeManager lifeSystem;
	private TimeManager theTime;

	// Use this for initialization
	void Start () {
		theTime = FindObjectOfType<TimeManager> ();
		//text = GetComponent<Text> ();
		healthBar = GetComponent<Slider>();
		playerHealth = maxPlayerHealth;
		levelManager = FindObjectOfType<LevelManager>();
		isDead = false;
		lifeSystem = FindObjectOfType<LifeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth <= 0 && isDead == false) {
			lifeSystem.takeLife ();
			playerHealth = 0;
			levelManager.RespawnPlayer ();
			isDead = true;
			theTime.ResetTime ();
		}
		if (playerHealth > maxPlayerHealth) {
			playerHealth = maxPlayerHealth;
		}
		//text.text = "" + playerHealth;
		healthBar.value = playerHealth;
	}

	public static void HurtPlayer(int damageToGive){
		playerHealth -= damageToGive;
	}

	public void FullHeath(){
		playerHealth = maxPlayerHealth;
	}

	public void KillPlayer(){
		playerHealth = 0;
	}
}
