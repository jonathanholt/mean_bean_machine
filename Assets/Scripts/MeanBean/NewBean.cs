using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBean : MonoBehaviour {
	
	public static GameObject bean1;
	public static GameObject bean2;
	public static GameObject nextBean1;
	public static GameObject nextBean2;
	public static int nextRandomBean1 = 0;
	public static int nextRandomBean2 = 0;
	public static int randomBean1 = 0;
	public static int randomBean2 = 0;
	public static Object [] sprites;

	void Start () {
		bean1 = GameObject.Find("bean1");
		bean2 = GameObject.Find("bean2");
		nextBean1 = GameObject.Find("nextBean1");
		nextBean2 = GameObject.Find("nextBean2");
		sprites = Resources.LoadAll ("beans");
		nextRandomBean1 = Random.Range (1, 6);
		nextRandomBean2 = Random.Range (1, 6);
		createNewBeanPair ();
	}

	public static void createNewBeanPair(){
		randomBean1 = nextRandomBean1;
		randomBean2 = nextRandomBean2;
		bean1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [randomBean1];
		bean2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [randomBean2];
		createNextBeanPair ();
	}

	public static void createNextBeanPair(){
		nextRandomBean1 = Random.Range(1, 6);
		nextRandomBean2 = Random.Range(1, 6);
		nextBean1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [nextRandomBean1];
		nextBean2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [nextRandomBean2];
	}

	private int getRandomBean1(){
		return randomBean1;
	}

	private int getRandomBean2(){
		return randomBean2;
	}
}
