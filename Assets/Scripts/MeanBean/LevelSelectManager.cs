using UnityEngine;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {
	
	public int menuOption;
	public GameObject menuSprite;
	public GameObject menuBeanSprite;
	public GameObject menuPosition;

	// Use this for initialization
	void Start () {
		menuOption = 0;
		Debug.Log ("Menu  " + menuOption);
		menuSprite = GameObject.Find("menu1");
		menuSprite.GetComponent<Renderer>().enabled = false;

		menuSprite = GameObject.Find("menu2");
		menuSprite.GetComponent<Renderer>().enabled = false;

		menuSprite = GameObject.Find("menu3");
		menuSprite.GetComponent<Renderer>().enabled = false;
		menuPosition = GameObject.Find("menu0pos");
		beanMover ();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("up")){
			if (menuOption - 1 < 0 != true) {
				Debug.Log ("Menu up before press  " + menuOption);
				menuOption -= 1;
				Debug.Log ("Menu up  " + menuOption);
				menuUpdater (menuOption);
				beanMover ();
			}
		}

		if(Input.GetKeyUp("down")){
			if (menuOption + 1 > 3 != true) {
				menuOption += 1;
				Debug.Log ("Menu down  " + menuOption);
				menuUpdater (menuOption);
				beanMover ();
			}
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
		Debug.Log (menuBeanSprite.transform.position+" vs "+menuPosition.transform.position);
		menuBeanSprite.transform.position = menuPosition.transform.position;
	}

}
