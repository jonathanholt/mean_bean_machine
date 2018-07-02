using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBeanController : MonoBehaviour {

	private static Animator anim;
	public static int whichAnimation;
	public static int animationsCount;
	public static int newAnimationNow;

	void Start () {
		anim = GetComponent<Animator> ();
		animationsCount = 10;
		whichAnimation = 0;
		newAnimationNow = 0;
	}

	public static void chooseAnimation(){
		newAnimationNow++;
		if (whichAnimation == 0) {
			int animationsCountAmended = animationsCount;
			int randomAnimation = Random.Range (0, animationsCountAmended);
			changeAnimation (randomAnimation);
		} else {
			if(newAnimationNow%3 ==0)
				changeAnimation (0);
		}
	}

	public static void changeAnimation(int animation){
		whichAnimation = animation;
		anim.SetInteger ("Animation", animation);
	}
}
