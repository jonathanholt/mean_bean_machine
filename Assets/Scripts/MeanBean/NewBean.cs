using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBean : MonoBehaviour {

	string[] beans = new string[] { "red", "purple", "yellow", "blue", "green" };
	public static GameObject bean1;
	public static GameObject bean2;
	public static int randomBean1 = 0;
	public static int randomBean2 = 0;

	// Use this for initialization
	void Start () {
		getNewBean ();

	}

	public static void getNewBean(){
		int randomColour1 = Random.Range(1, 5);
		int randomColour2 = Random.Range(1, 5);
		//Debug.Log (beans[randomColour1]+","+beans[randomColour2]);
		bean1 = GameObject.Find("bean1");
		bean2 = GameObject.Find("bean2");
		Object [] sprites;
		sprites = Resources.LoadAll ("beans");
		randomBean1 = Random.Range (1, 6);
		randomBean2 = Random.Range (1, 6);
		bean1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [randomBean1];
		bean2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [randomBean2];
	}
}
