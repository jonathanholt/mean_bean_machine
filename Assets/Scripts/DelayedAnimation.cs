using UnityEngine;
using System.Collections;

public class DelayedAnimation : MonoBehaviour {

	Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
		StartCoroutine(MyCoroutine());
		animator.SetBool("Waiting",true);
	}

	void RepeatMyCoroutine() {
		StartCoroutine(MyCoroutine());
	}

	private IEnumerator MyCoroutine() {
		while (1 == 1) {
			animator.SetBool ("Waiting", false);
			yield return new WaitForSeconds (5f);
			animator.SetBool ("Waiting", true);
			yield return new WaitForSeconds (5f);
			RepeatMyCoroutine ();
		}
	}
}
