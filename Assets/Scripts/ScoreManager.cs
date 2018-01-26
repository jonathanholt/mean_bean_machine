using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int score;

	void Start(){
		score = 0;
	}

	void Update(){
	}

	public static void AddPoints(int pointsToAdd){
		score += pointsToAdd;
		Debug.Log (score);
	}

	public static void Reset(){
		score = 0;
	}

	public static void changePoints(int value){
		
	}
}
