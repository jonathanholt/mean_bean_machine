﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuisanceController : MonoBehaviour {

	public static int nuisanceState = 0;
	public static GameObject bean1;
	public static GameObject bean2;
	public static GameObject nextBean1;
	public static GameObject nextBean2;
	public static int nextRandomBean1 = 0;
	public static int nextRandomBean2 = 0;
	public static int randomBean1 = 0;
	public static int randomBean2 = 0;
	public static Object [] sprites;

 	void Start(){
		nuisanceState = 0;
		bean1 = GameObject.Find("bean1");
		bean2 = GameObject.Find("bean2");
		nextBean1 = GameObject.Find("nextBean1");
		nextBean2 = GameObject.Find("nextBean2");
		sprites = Resources.LoadAll<Sprite> ("nuisance");
		nextRandomBean1 = Random.Range (1, 6);
		nextRandomBean2 = Random.Range (1, 6);
	}

	public static void initNuisance(){
		string toGet = "Nuisance" + nuisanceState;
		GameObject nuisanceSquare = GameObject.Find(toGet);
		nuisanceSquare.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [0];
	}

	public static void createNewNuisancePair(){
		bean1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [1];
		bean2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [1];
	}
}
