using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	
	public int menuOption;
	public GameObject menuSprite;
	public GameObject menuBeanSprite;
	public GameObject subMenuBeanSprite;
	public GameObject menuPosition;
	public GameObject mainMenuDestination;
	public GameObject subMenuDestination;
	public GameObject MainMenuObserver;
	public int menuLevel;
	public int pushedButtonTimes;
	public int flashing = 0;
	public int subflashing = 0;
	public int flashingDirection = 1;

	// Use this for initialization
	void Start () {
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
		beanMover ();

	}
	
	// Update is called once per frame
	void Update () {
		
		 if(Vector3.Distance(GameObject.Find ("SubMenus").transform.position, subMenuDestination.transform.position) < 0.1f){
				flashing = 0;
		 }
		 
			if (Input.GetKeyUp ("up") && flashing == 0 && subflashing == 0) {
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

			if (Input.GetKeyUp ("down") && flashing == 0 && subflashing == 0) {
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

			if (Input.GetKeyUp ("a")) {
				if (menuLevel == 0) {
					menuLevel++;
				}
				pushedButtonTimes++;
				if (pushedButtonTimes >= 2 && GameObject.Find ("submenu0").GetComponent<Renderer>().enabled) {
					int nextLevelNum = 2;
					StartCoroutine(Begin(2f));
				}
			}

			if (menuLevel == 1) {
				StartCoroutine(Next (1f));
			}
		
		
		
		if (flashing == 1 && GameObject.Find("menubean") != null) {
			flashBean (GameObject.Find ("menubean"));
		}
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
	
	public void test(){
		Debug.Log("Test");
	}

	public void updateMenus(){
		
		
		float speed = 20f;
		float step = speed * Time.deltaTime;
		menuBeanSprite = GameObject.Find ("Menus");
		subMenuBeanSprite = GameObject.Find ("SubMenus");
	    subMenuBeanSprite.transform.position = Vector3.MoveTowards (subMenuBeanSprite.transform.position, subMenuDestination.transform.position,  step);
		menuBeanSprite.transform.position = Vector3.MoveTowards (menuBeanSprite.transform.position, mainMenuDestination.transform.position, step);
	}


	IEnumerator Begin(float delayTime){
		Destroy (GameObject.Find("menubean"));
		subflashing = 1;
		yield return new WaitForSeconds (delayTime);
		int nextLevelNum = 2;
		SceneManager.LoadScene (nextLevelNum);
	}

	void subMenuUpdater(int subMenuCurrent){
		switch (subMenuCurrent) {
		case 1:
			menuSprite = GameObject.Find ("submenu0");
			menuSprite.GetComponent<Renderer> ().enabled = true;
			menuSprite = GameObject.Find ("submenu1");
			menuSprite.GetComponent<Renderer> ().enabled = false;
			menuPosition = GameObject.Find ("submenu0pos");
			break;
		case 2:
			menuSprite = GameObject.Find ("submenu1");
			menuSprite.GetComponent<Renderer> ().enabled = true;
			menuSprite = GameObject.Find ("submenu0");
			menuSprite.GetComponent<Renderer> ().enabled = false;
			menuPosition = GameObject.Find ("submenu1pos");
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
}
