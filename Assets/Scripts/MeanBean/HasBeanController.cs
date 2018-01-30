using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBeanController : MonoBehaviour {

	private static Animator anim;
	public static int whichAnimation;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void changeAnimation(int animation){
		whichAnimation = animation;
		anim.SetInteger ("Animation", 1);
	}
}
