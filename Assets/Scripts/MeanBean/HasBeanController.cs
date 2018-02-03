using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBeanController : MonoBehaviour {

	private static Animator anim;
	public static int whichAnimation;
	public static int animationsCount;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		animationsCount = 10;
		whichAnimation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static void chooseAnimation(){
		if (whichAnimation == 0) {
			int animationsCountAmended = animationsCount;
			int randomAnimation = Random.Range (0, animationsCountAmended);
			changeAnimation (1);
		} else {
			changeAnimation (0);
		}
	}

	public static void changeAnimation(int animation){
		Debug.Log("Animation = "+animation);
		whichAnimation = animation;
		anim.SetInteger ("Animation", animation);
	}
}
