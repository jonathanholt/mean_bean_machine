using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuObserver : MonoBehaviour {

	public GameObject scrollingBackground;
	public int menuProgress;

	void Start () {
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
	}

	void Update () {
		if(Input.GetKeyUp("a")){
			this.menuProgress++;
			if (this.menuProgress == 2) {
				videoFinish ();
			}
		}
	}

	public int getMenuProgress(){
		return this.menuProgress;
	}

	public void videoFinish(){
		scrollingBackground = GameObject.Find("VideoPlayer");
		scrollingBackground.videoFinish ();
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
	}
}
