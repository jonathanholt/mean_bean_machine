using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameController : MonoBehaviour {

	public Camera mainCamera;
	public GameObject cameraLocation;
	public GameObject gameArea;
	public GameObject UI;
	public GameObject BeanController;
	public GameObject AIBeanController;
	Animator cameraAnimator;

	void Start(){
		gameArea.SetActive(false);
		UI.SetActive (false);
		cameraAnimator = mainCamera.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("a")) {
			moveCamera ();
		}
	}

	public void moveCamera(){
		cameraAnimator.SetBool ("cameraMove", true);
		UI.SetActive(true);
		//BeanController.GetComponent<BeanFactory> ().createBeanPair ();
		//BeanController.GetComponent<BeanFactory> ().createBeanPair ();
		BeanController.GetComponent<MotionObserver> ().isReady ();
		AIBeanController.GetComponent<MotionObserver> ().isReady ();
		Destroy (this);
	}
}