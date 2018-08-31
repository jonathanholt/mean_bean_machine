using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMugshotController : MonoBehaviour {

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
				if (direction == 0) {
					Color tmp = GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color;
					tmp.a = tmp.a - 0.1f;
					GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color = tmp;

					if (GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color.a < 0.4f) {
						direction = 1;
					}
				} else {
					Color tmp = GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color;
					tmp.a = tmp.a + 0.1f;
					GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color = tmp;

					if (GameObject.Find ("EnemyMugshotFullColour").GetComponent<SpriteRenderer> ().color.a > 0.99f) {
						direction = 0;
						flashes--;
					}
				}
			}
			else{
				SceneManager.LoadScene (1);
			}
		}
	}
}
