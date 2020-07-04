using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameIntroObserver : MonoBehaviour {

	public int introProgress;
	public VideoClip secondIntroVideo;
    public int introVideoRecurringTimer;
	public VideoPlayer videoPlayer;
	
    void Start () {
	    this.introVideoRecurringTimer = -1;
        this.introProgress = 0;
		videoPlayer = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
		videoPlayer.loopPointReached += CheckOver;
	}
	
	void CheckOver(UnityEngine.Video.VideoPlayer vp)
	{
		vp.clip = secondIntroVideo;
	}

	void Update () {
        if(this.introVideoRecurringTimer != -1){
            this.introVideoRecurringTimer += 1;
            if(this.introVideoRecurringTimer == 400){
                videoRestart();
            }
        }
		if(Input.GetKeyUp("a")){
				buttonPress();
		}
	}
	
	public void left(){
		Debug.Log("Test");
	}
	
	public void buttonPress(){
					if(videoPlayer.clip.name != "sega"){
			this.introProgress++;
			if (this.introProgress == 1) {
				videoFinish ();
				GameObject.Find("BlackBackground").GetComponent<Renderer>().enabled = false;
			}
			if (this.introProgress == 2) {
				introScreenFinish ();
			}
			}
	}

	public void introScreenFinish(){
			Destroy(GameObject.Find("MenuSprites"));
	}
	
	public void videoFinish(){
		videoPlayer.enabled = false;
        this.introVideoRecurringTimer = 0;
	}
    
    public void videoRestart(){
        this.introVideoRecurringTimer = -1;
		videoPlayer.enabled = true;
	}
}
