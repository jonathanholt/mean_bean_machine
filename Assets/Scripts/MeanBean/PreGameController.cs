using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameController : MonoBehaviour {

	public Camera mainCamera;
	public GameObject cameraLocation;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("a")) {
			mainCamera.transform.position = new Vector3 (cameraLocation.transform.position.x, cameraLocation.transform.position.y, mainCamera.transform.position.z);
		}
	}
}