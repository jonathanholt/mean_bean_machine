using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMugshotController : MonoBehaviour {

	int direction = 0;
	int flashes = 10;
	bool startFlashing = false;

	void Start () {
		StartCoroutine(DelayFlash(1f));
	}

	IEnumerator DelayFlash(float delayTime){
		yield return new WaitForSeconds (delayTime);
		startFlashing = true;
	}

	void Update () {
		if (startFlashing) {
			if (flashes > 0) {
				Color tmp = GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color;
				if (direction == 0) {
					tmp.a = tmp.a - 0.25f;
					if (GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color.a < 0.4f) {
						direction = 1;
					}
				} else {
					tmp.a = tmp.a + 0.25f;
					if (GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color.a > 0.99f) {
						direction = 0;
						flashes--;
					}
				}
				GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color = tmp;
			}
			else{
				SceneManager.LoadScene (1);
			}
		}
	}
}
