using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanFactory : MonoBehaviour {
	
	public GameObject startPoint;
	public GameObject startPointUpper;
	bool readyForNext = false;
	string[] prefabs = {"purple1", "red1", "blue1", "green1", "yellow1"};
	
	public void createBeanPair () {
		GameObject.Find("Player").GetComponent<MotionController>().resetCurrentPosition(2);
		string randomPrefab1 = choosePrefab();
		GameObject bean1 = Instantiate(Resources.Load(randomPrefab1)) as GameObject;
		bean1.name = randomPrefab1;
		bean1.GetComponent<Bean> ().setInPlay (1);
		bean1.transform.position = startPoint.transform.position;
	
		string randomPrefab2 = choosePrefab();
		GameObject bean2 = Instantiate(Resources.Load(randomPrefab2)) as GameObject;
		bean2.name = randomPrefab2;
		bean2.GetComponent<Bean> ().setInPlay (2);
		bean2.transform.position = startPointUpper.transform.position;
	}
	
	public string choosePrefab(){
		int randomNumber = Random.Range(0, 5);
		return prefabs[2];
	}
	
	public void createNext(){
		if(!readyForNext){
			readyForNext = true;
			createBeanPair();
			StartCoroutine(LongPause (2f));
		}
	}
	
	IEnumerator LongPause(float delayTime){
		yield return new WaitForSeconds (delayTime);
		readyForNext = false;
	}
}
