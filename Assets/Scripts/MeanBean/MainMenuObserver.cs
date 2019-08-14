using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenuObserver : MonoBehaviour {

	public GameObject scrollingBackground;
	public int menuProgress;
	public VideoClip secondIntroVideo;
    public int introVideoRecurringTimer;
	
    void Start () {
	    this.introVideoRecurringTimer = -1;
        this.menuProgress = 0;
		scrollingBackground = GameObject.Find("menus_6");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_10");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_10 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_8 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_8");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_9 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_9");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_9 (2)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_9 (3)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_7 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_7");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_7 (2)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		scrollingBackground = GameObject.Find("menus_7 (3)");
		scrollingBackground.GetComponent<Renderer>().enabled = false;
		
		GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>().loopPointReached += CheckOver;
	}
	
	void CheckOver(UnityEngine.Video.VideoPlayer vp)
	{
		vp.clip = secondIntroVideo;
	}

	void Update () {
        if(this.introVideoRecurringTimer != -1){
            this.introVideoRecurringTimer += 1;
            Debug.Log("test "+introVideoRecurringTimer);
            if(this.introVideoRecurringTimer == 400){
                videoRestart();
            }
        }
		if(Input.GetKeyUp("a")){
			if(GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>().clip.name != "sega"){
			this.menuProgress++;
			if (this.menuProgress == 1) {
				videoFinish ();
				GameObject.Find("black_background").GetComponent<Renderer>().enabled = false;
			}
			if (this.menuProgress == 2) {
				menuScreenFinish ();
				mainMenuCreate();
			}
			}
		}
	}

	public void menuScreenFinish(){
			Destroy(GameObject.Find("MenuSprites"));
	}
	
	public void mainMenuCreate(){
		scrollingBackground = GameObject.Find("menus_6");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_10");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_10 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_8 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_8");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_9 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_9");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_9 (2)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_9 (3)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_7 (1)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_7");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_7 (2)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		scrollingBackground = GameObject.Find("menus_7 (3)");
		scrollingBackground.GetComponent<Renderer>().enabled = true;
		GameObject.Find ("menu").GetComponent<AudioSource> ().Play ();
	}
	
	public void videoFinish(){
		scrollingBackground = GameObject.Find("VideoPlayer");
		VideoPlayer videoPlayer = scrollingBackground.GetComponent<VideoPlayer>();
		videoPlayer.enabled = false;
        this.introVideoRecurringTimer = 0;
	}
    
    public void videoRestart(){
        this.introVideoRecurringTimer = -1;
		scrollingBackground = GameObject.Find("VideoPlayer");
		VideoPlayer videoPlayer = scrollingBackground.GetComponent<VideoPlayer>();
		videoPlayer.enabled = true;
	}
}
