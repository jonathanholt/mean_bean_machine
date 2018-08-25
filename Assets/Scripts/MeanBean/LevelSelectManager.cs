using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	public AudioSource source;
	public AudioClip clip;
	public int pushedButtonTimes;
	AudioSource selectSfx;
	public AudioClip soundToPlay;
	public int flashing = 0;
	public int subflashing = 0;
	public int flashingDirection = 1;

	// Use this for initialization
	void Start () {
		selectSfx = GameObject.Find("Player").GetComponent<AudioSource> ();
		menuOption = 0;
		menuLevel = 0;
		pushedButtonTimes = 0;
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

	}
	
	// Update is called once per frame
	void Update () {
		if (videoPlaying < 3 && Input.GetKeyUp ("a")) {
			videoPlaying++;
			if (videoPlaying > 1) {
				selectSfx.PlayOneShot (soundToPlay, 1);
			}
		}
		if (videoPlaying >= 2) {
			if (Input.GetKeyUp ("up")) {
				if (menuLevel > 0) {
					subMenuUpdater (1);
					subBeanMover ();
				} else {
					if (menuOption - 1 < 0 != true) {
						menuOption -= 1;
						menuUpdater (menuOption);
						beanMover ();
					}
				}

			}

			if (Input.GetKeyUp ("down")) {
				if (menuLevel > 0) {
					subMenuUpdater (2);
					subBeanMover ();
				} else {
					if (menuOption + 1 > 3 != true) {
						menuOption += 1;
						menuUpdater (menuOption);
						beanMover ();
					}
				}
			}

			if (Input.GetKeyUp ("a") && videoPlaying >= 3) {
				if (menuLevel == 0) {
					menuLevel++;
				}
				pushedButtonTimes++;
				if (pushedButtonTimes >= 2 && GameObject.Find ("sub1").GetComponent<Renderer>().enabled) {
					selectSfx.PlayOneShot (soundToPlay, 1);
					int nextLevelNum = 2;
					StartCoroutine(Begin(2f));
				}
			}

			if (menuLevel == 1) {
				StartCoroutine(Next (1f));
			}
		}
		if (flashing == 1 && GameObject.Find("menubean") != null) {
			flashBean (GameObject.Find ("menubean"));
		}
		Debug.Log ("Are we getting here? "+subflashing);
		if (subflashing == 1) {
			flashBean (GameObject.Find ("submenubean"));
		}

	}

	public void flashBean(GameObject beanToFlash){
		if (flashingDirection == 0) {
			Color tmp = beanToFlash.GetComponent<SpriteRenderer> ().color;
			tmp.a = tmp.a - 0.2f;
			beanToFlash.GetComponent<SpriteRenderer> ().color = tmp;

			if (beanToFlash.GetComponent<SpriteRenderer> ().color.a < 0.2f) {
				flashingDirection = 1;
			}
		} else {
			Color tmp = beanToFlash.GetComponent<SpriteRenderer> ().color;
			tmp.a = tmp.a + 0.2f;
			beanToFlash.GetComponent<SpriteRenderer> ().color = tmp;

			if (beanToFlash.GetComponent<SpriteRenderer> ().color.a > 0.99f) {
				flashingDirection = 0;
			}
		}
	}

	IEnumerator Next(float delayTime){
		flashing = 1;
		yield return new WaitForSeconds (delayTime);
		updateMenus ();
	}

	public void updateMenus(){
		menuBeanSprite = GameObject.Find ("Menus");
		subMenuBeanSprite = GameObject.Find ("SubMenu");
		if (subMenuBeanSprite.transform.position != subMenuDestination.transform.position) {
			subMenuBeanSprite.transform.position = Vector3.Lerp (subMenuBeanSprite.transform.position, subMenuDestination.transform.position, 0.04f);
		}
		if (menuBeanSprite.transform.position != mainMenuDestination.transform.position) {
			menuBeanSprite.transform.position = Vector3.Lerp (menuBeanSprite.transform.position, mainMenuDestination.transform.position, 0.04f);
		} else {
			menuLevel = 2;
		}
	}


	IEnumerator Begin(float delayTime){
		Destroy (GameObject.Find("menubean"));
		subflashing = 1;
		Debug.Log ("Flashing!");
		yield return new WaitForSeconds (delayTime);
		int nextLevelNum = 2;
		SceneManager.LoadScene (nextLevelNum);
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

	void subBeanMover(){
		menuBeanSprite = GameObject.Find("submenubean");
		menuBeanSprite.transform.position = menuPosition.transform.position;
	}

	void mainMenuMover(){
		menuBeanSprite = GameObject.Find("Menus");
		menuBeanSprite.transform.position = mainMenuDestination.transform.position;
	}

}
