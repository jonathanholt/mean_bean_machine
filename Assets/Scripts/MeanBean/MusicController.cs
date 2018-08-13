using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public AudioClip mainMusic;
	public bool changedMusic;

	// Use this for initialization
	void Start () {
		changedMusic = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("a") && !changedMusic) {
			changeMusic ();
			changedMusic = true;
		}
	}

	public void changeMusic(){
		GameObject.Find ("MusicManager").GetComponent<AudioSource> ().clip = mainMusic;
		GameObject.Find ("MusicManager").GetComponent<AudioSource> ().Play ();
	}

}
