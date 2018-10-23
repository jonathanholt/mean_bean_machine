using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalancheController : MonoBehaviour {

	public int avalancheCount;
	public int avalancheToFall;
	public int processingAvalancheCount;
	public GameObject theOpponentArray;
	public GameObject theOpponent;
	public GameObject PlayerArray;


	void Start () {
		avalancheCount = 0;
	}
	
	public void incrementAvalancheCount(){
		Debug.Log("INCREMENTING FROM "+avalancheCount + " to "+ (avalancheCount +1));
		avalancheCount += 1;
	}
	
	public void queueAvalanche(){
		//Debug.Log ("Avalanche queued confirmation");
		theOpponent.GetComponent<AvalancheController>().avalancheToFall = avalancheCount;
		processingAvalancheCount = avalancheCount;
		avalancheCount = 0;
		theOpponentArray.GetComponent<BeanFactory>().avalancheNext = true;
	}

	public void processAvalanche(){
		//Debug.Log("Processing avalanche");
		//Pass in the name of the Gameobject that is being called
		// Add a new method to the BeanFactory class that 'loads' a nuisance bean instead of the next bean pair
		PlayerArray.GetComponent<BeanFactory> ().createBeanPair (true, avalancheToFall);
		theOpponent.GetComponent<AvalancheController>().processingAvalancheCount = 0;
		Debug.Log("Processing avalanche "+avalancheToFall);
		avalancheToFall = 0;
	}
}
