using UnityEngine;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {
	
	public int menuOption;
	public GameObject menuSprite;
	public GameObject menuBeanSprite;
	public GameObject subMenuBeanSprite;
	public GameObject menuPosition;
	public GameObject mainMenuDestination;
	public GameObject subMenuDestination;
	public GameObject MainMenuObserver;
	public int menuLevel;
	public int videoPlaying = 0;

	// Use this for initialization
	void Start () {
		menuOption = 0;
		menuLevel = 0;
		menuSprite = GameObject.Find("menu1");
		menuSprite.GetComponent<Renderer>().enabled = false;
		menuSprite = GameObject.Find("menu2");
		menuSprite.GetComponent<Renderer>().enabled = false;
		menuSprite = GameObject.Find("menu3");
		menuSprite.GetComponent<Renderer>().enabled = false;
		menuPosition = GameObject.Find("menu0pos");
		mainMenuDestination = GameObject.Find("MenusDestination");
		subMenuDestination = GameObject.Find("SubMenusDestination");
		MainMenuObserver = GameObject.Find("MainMenuObserver");
		beanMover ();
		Debug.Log ("initial debug " + menuLevel);

	}
	
	// Update is called once per frame
	void Update () {
		if (videoPlaying < 3 && Input.GetKeyUp ("a")) {
			videoPlaying++;
		}
		if (videoPlaying >= 2) {
			if (Input.GetKeyUp ("up")) {
				if (menuLevel > 0) {
					Debug.Log ("calling submenu updater");
					subMenuUpdater (1);
					beanMover ();
				} else {
					if (menuOption - 1 < 0 != true) {
						Debug.Log ("Menu up before press  " + menuOption);
						menuOption -= 1;
						Debug.Log ("Menu up  " + menuOption);
						menuUpdater (menuOption);
						beanMover ();
					}
				}

			}

			if (Input.GetKeyUp ("down")) {
				Debug.Log ("MenuLevel"+menuLevel);
				if (menuLevel > 0) {
					subMenuUpdater (2);
					beanMover ();
				} else {
					if (menuOption + 1 > 3 != true) {
						menuOption += 1;
						Debug.Log ("Menu down  " + menuOption);
						menuUpdater (menuOption);
						beanMover ();
					}
				}
			}

			if (Input.GetKeyUp ("a") && videoPlaying >= 3) {
				Debug.Log ("Upping menu level");
				if (menuLevel == 0)
					menuLevel++;
			}

			if (menuLevel == 1) {
				menuBeanSprite = GameObject.Find ("Menus");
				subMenuBeanSprite = GameObject.Find ("SubMenu");
				if (subMenuBeanSprite.transform.position != subMenuDestination.transform.position) {
					subMenuBeanSprite.transform.position = Vector3.Lerp (subMenuBeanSprite.transform.position, subMenuDestination.transform.position, 0.02f);
				}
				if (menuBeanSprite.transform.position != mainMenuDestination.transform.position) {
					menuBeanSprite.transform.position = Vector3.Lerp (menuBeanSprite.transform.position, mainMenuDestination.transform.position, 0.02f);
				} else {
					menuLevel = 2;
				}
			}
		}
	}

	void subMenuUpdater(int subMenuCurrent){
		switch (subMenuCurrent) {
		case 1:
			menuSprite = GameObject.Find ("sub1");
			menuSprite.GetComponent<Renderer> ().enabled = true;
			menuSprite = GameObject.Find ("sub2");
			menuSprite.GetComponent<Renderer> ().enabled = false;
			menuPosition = GameObject.Find ("submenu1pos");
			break;
		case 2:
			menuSprite = GameObject.Find ("sub2");
			menuSprite.GetComponent<Renderer> ().enabled = true;
			menuSprite = GameObject.Find ("sub1");
			menuSprite.GetComponent<Renderer> ().enabled = false;
			menuPosition = GameObject.Find ("submenu2pos");
			break;
		default:
			menuSprite.GetComponent<Renderer> ().enabled = true;
			break;
		}
	}

	void menuUpdater(int menuCurrent){
		switch (menuCurrent) {
		case 0:
			menuSprite = GameObject.Find("menu0");
			menuSprite.GetComponent<Renderer>().enabled = true;
			menuSprite = GameObject.Find("menu1");
			menuSprite.GetComponent<Renderer>().enabled = false;
			menuPosition = GameObject.Find("menu0pos");
			break;
		case 1:
			menuSprite = GameObject.Find("menu1");
			menuSprite.GetComponent<Renderer>().enabled = true;
			menuSprite = GameObject.Find("menu2");
			menuSprite.GetComponent<Renderer>().enabled = false;
			menuSprite = GameObject.Find("menu0");
			menuSprite.GetComponent<Renderer>().enabled = false;
			menuPosition = GameObject.Find("menu1pos");
			break;
		case 2:
			menuSprite = GameObject.Find("menu2");
			menuSprite.GetComponent<Renderer>().enabled = true;
			menuSprite = GameObject.Find("menu1");
			menuSprite.GetComponent<Renderer>().enabled = false;
			menuSprite = GameObject.Find("menu3");
			menuSprite.GetComponent<Renderer>().enabled = false;
			menuPosition = GameObject.Find("menu2pos");
			break;
		case 3:
			menuSprite = GameObject.Find("menu3");
			menuSprite.GetComponent<Renderer>().enabled = true;
			menuSprite = GameObject.Find("menu2");
			menuSprite.GetComponent<Renderer>().enabled = false;
			menuPosition = GameObject.Find("menu3pos");
			break;
		default:
			menuSprite.GetComponent<Renderer>().enabled = true;
			break;
		}

	}

	void beanMover(){
		menuBeanSprite = GameObject.Find("menubean");
		menuBeanSprite.transform.position = menuPosition.transform.position;
	}

	void mainMenuMover(){
		menuBeanSprite = GameObject.Find("Menus");
		menuBeanSprite.transform.position = mainMenuDestination.transform.position;
	}

}
