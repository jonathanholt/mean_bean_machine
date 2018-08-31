using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObserver : MonoBehaviour {

	public GameObject gameArea;

	public void hasBeanIntroAnimation(){
		Animator hasBeanAnimator;
		hasBeanAnimator = GameObject.Find("HasBean").GetComponent<Animator> ();
		hasBeanAnimator.SetBool ("gameReady", true);
	}

	public void enableMainGameComponents(){
		gameArea.SetActive(true);
	}

}
