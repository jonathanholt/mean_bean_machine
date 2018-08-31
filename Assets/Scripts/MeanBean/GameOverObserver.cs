using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameOverObserver : MonoBehaviour {

	GameObject videoPlayerObject;
	VideoPlayer videoPlayer;
	bool videoStarted = false;

	void Start(){
		videoPlayerObject = GameObject.Find("VideoPlayer");
		videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
	}

	void Update () {
		if (Input.GetKeyUp ("a")) {
			continueGame ();
		}
		if (videoPlayer.isPlaying) {
			videoStarted = true;
		} 
		if(!videoPlayer.isPlaying && videoStarted) {
			SceneManager.LoadScene (0);
		}
	}

	public void continueGame(){
		SceneManager.LoadScene (2);
	}
}
