using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuisanceController : MonoBehaviour {

	public static int nuisanceState = 0;

	public static void initNuisance(){
		string toGet = "Nuisance" + nuisanceState;
		Debug.Log (toGet);
		GameObject nuisanceSquare = GameObject.Find(toGet);
		Object [] sprites;
		sprites = Resources.LoadAll<Sprite> ("nuisance");
		nuisanceSquare.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [0];
	}

	public static void dropNuisance(){
	}
}
