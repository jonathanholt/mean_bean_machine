using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameController : MonoBehaviour {

	public Camera mainCamera;
	public GameObject cameraLocation;
	public GameObject gameArea;
	public GameObject UI;
	public GameObject BeanController;
	public GameObject AIBeanController;
	Animator cameraAnimator;
	public Text enemyDialog;

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
		Destroy(enemyDialog);
		cameraAnimator.SetBool ("cameraMove", true);
		UI.SetActive(true);
		StartCoroutine (controllersReady (3.125f));
	}
	
	IEnumerator controllersReady(float yieldTime){
		yield return new WaitForSeconds(yieldTime);
		BeanController.GetComponent<MotionObserver> ().isReady ();
		AIBeanController.GetComponent<MotionObserver> ().isReady ();
		Destroy (this);
	}
}