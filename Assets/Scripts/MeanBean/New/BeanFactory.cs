using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanFactory : MonoBehaviour {
	
	public GameObject startPoint;
	public GameObject[] startPointArray;
	public GameObject startPointUpper;
	bool readyForNext = false;
	string[] prefabs = {"purple1", "red1", "blue1", "green1", "yellow1"};
	string greyString = "grey1";
	public GameObject thePlayer;
	public GameObject beanArray;
	public bool avalancheNext;
	public bool canMove = true;

	public void createBeanPair (bool isAvalanche, int avalancheCount = 0) {
		//HasBeanController.chooseAnimation ();
		if(avalancheCount > 2){
			Debug.Log("HIGHER THAN 2!!!");
				thePlayer.GetComponent<MotionController>().resetCurrentPosition(2);
				string randomPrefab1 = greyString;
				for(int i = 0; i < 5; i ++){
					GameObject bean1 = Instantiate(Resources.Load(randomPrefab1)) as GameObject;
					bean1.GetComponent<Bean>().SetPlayer(thePlayer);
					bean1.GetComponent<Bean>().SetBeanArray(beanArray);
					bean1.name = randomPrefab1;
					bean1.GetComponent<Bean> ().setInPlay (1);
					bean1.transform.position = startPointArray[i].transform.position;
				}
		}
		else{
		thePlayer.GetComponent<MotionController>().resetCurrentPosition(2);
		string randomPrefab1;
		if(isAvalanche)
			randomPrefab1 = greyString;
		else
			randomPrefab1 = choosePrefab();
		
		GameObject bean1 = Instantiate(Resources.Load(randomPrefab1)) as GameObject;
		bean1.GetComponent<Bean>().SetPlayer(thePlayer);
		bean1.GetComponent<Bean>().SetBeanArray(beanArray);
		bean1.name = randomPrefab1;
		bean1.GetComponent<Bean> ().setInPlay (1);
		if(isAvalanche){
			Debug.Log("avalanche..."+thePlayer.GetComponent<AvalancheController>().avalancheToFall);
			int resetPosition = Random.Range(0, 4);
			bean1.transform.position = startPointArray[resetPosition].transform.position;
		}
		else{
			bean1.transform.position = startPoint.transform.position;
		}
		if (!isAvalanche) {
			string randomPrefab2 = choosePrefab ();
			GameObject bean2 = Instantiate (Resources.Load (randomPrefab2)) as GameObject;
			bean2.GetComponent<Bean> ().SetPlayer (thePlayer);
			bean2.GetComponent<Bean> ().SetBeanArray (beanArray);
			bean2.name = randomPrefab2;
			bean2.GetComponent<Bean> ().setInPlay (2);
			bean2.transform.position = startPointUpper.transform.position;
		}
		}
	}
	
	public string choosePrefab(){
		int randomNumber = Random.Range(0, 5);
		return prefabs[randomNumber];
	}
	
	public void createNext(){
		if(!readyForNext){
			readyForNext = true;
			if (thePlayer.GetComponent<AvalancheController>().avalancheCount > 0) {
				//Debug.Log ("Avalanche!!");
				thePlayer.GetComponent<AvalancheController> ().queueAvalanche ();
			}

			if(avalancheNext){
				canMove = false;
				thePlayer.GetComponent<AvalancheController> ().processAvalanche ();
				avalancheNext = false;
				StartCoroutine (LongPause (2f));
			}
			else {
				canMove = true;
				createBeanPair (false);
				StartCoroutine (LongPause (2f));
			}
		}
	}
	
	IEnumerator LongPause(float delayTime){
		yield return new WaitForSeconds (delayTime);
		readyForNext = false;
	}
}
