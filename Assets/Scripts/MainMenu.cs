using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;
	public string levelSelect;
	public int playerLives;
	public string level1Tag;

	public void NewGame(){
		Application.LoadLevel (startLevel);

		PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		PlayerPrefs.SetInt ("CurrentScore", 0);
		PlayerPrefs.SetInt (level1Tag, 1);
		PlayerPrefs.SetInt ("PlayerLevelSelectPosition", 0);
	}

	public void LevelSelect(){
		PlayerPrefs.SetInt ("PlayerCurrentLives", playerLives);
		PlayerPrefs.SetInt ("CurrentScore", 0);
		PlayerPrefs.SetInt (level1Tag, 1);

		if(!PlayerPrefs.HasKey("PlayerLevelSelectPosition")){
			PlayerPrefs.SetInt ("PlayerLevelSelectPosition", 0);
		}
		Application.LoadLevel (levelSelect);
	}

	public void QuitGame(){
		Debug.Log ("Game Exited");
		Application.Quit ();
	}
}
