using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private static Animator anim;
	public static int whichAnimation;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	public static void changeAnimationWinning(){
		whichAnimation = 10;
		anim.SetInteger ("Health", 10);
	}

	public static void changeAnimationIdle(){
		whichAnimation = 0;
		anim.SetInteger ("Health", 0);
	}

	public static void changeAnimationLosing(){
		whichAnimation = -1;
		anim.SetInteger ("Health", -1);
	}

	public static void changeAnimationLost(){
		whichAnimation = -10;
		anim.SetInteger ("Health", -10);
	}
}
