using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanFactory : MonoBehaviour {
	
	public GameObject startPoint;
	public GameObject startPointUpper;
	bool readyForNext = false;
	string[] prefabs = {"purple1", "red1", "blue1", "green1", "yellow1"};
	
	public void createBeanPair () {
		string randomPrefab1 = choosePrefab();
		Debug.Log(randomPrefab1);
		GameObject test = Instantiate(Resources.Load(randomPrefab1)) as GameObject;
		test.name = randomPrefab1;
		test.transform.position = startPoint.transform.position;
	
		string randomPrefab2 = choosePrefab();
		Debug.Log(randomPrefab2);
		GameObject test2 = Instantiate(Resources.Load(randomPrefab2)) as GameObject;
		test2.name = randomPrefab2;
		test2.transform.position = startPointUpper.transform.position;
	}
	
	public string choosePrefab(){
		int randomNumber = Random.Range(0, 5);
		return prefabs[randomNumber];
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
