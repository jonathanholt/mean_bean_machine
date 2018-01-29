using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int score;
	public static int standardScore;
	public static int scoreMultiplier;
	public static GameObject[] scoreNumbers;
	public static int pointsCache;

	void Start(){
		score = 0;
		standardScore = 40;
		scoreMultiplier = 8;
		pointsCache = 0;
		scoreNumbers = new GameObject[8];
		PopulateScoreNumbers ();
	}

	void Update(){
	}

	public static void PopulateScoreNumbers(){
		for (int i = 7; i > -1; i--) {
			GameObject findScoreObject = GameObject.Find("chars_score_"+i+"_p1");
			scoreNumbers [i] = findScoreObject;
		}
	}

	public static void AddPoints(int pointsToAdd){
		pointsCache += standardScore;
	}

	public static void Reset(){
		score = 0;
	}

	public static void AlterGUI(){
		Object [] sprites;
		sprites = Resources.LoadAll<Sprite> ("chars");
		int digits = (pointsCache + score).ToString ().Length;
		if (pointsCache == 40) {
			int counter = 7;
			foreach (GameObject scoreNumber in scoreNumbers) {
				if(counter < 6){
					scoreNumber.SetActive (false);
				}
				if (counter == 7) {
					scoreNumber.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [0];
				}
				if (counter == 6) {
					scoreNumber.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [4];
				}
				counter--;
			}
		}
		timeBreak ();
		endScoreAlter ();
	}

	public static IEnumerator timeBreak(){
		Debug.Log ("timebreak");
		yield return new WaitForSeconds (5f);
	}

	public static void endScoreAlter(){
		Debug.Log ("timebreak");
		Object [] sprites;
		sprites = Resources.LoadAll<Sprite> ("chars");
		string digits = (pointsCache + score).ToString ();
		int counter = 0;
		foreach (GameObject scoreNumber in scoreNumbers) {
			if (counter <= (digits.Length - 1)) {
				int digitGot = int.Parse (digits [counter].ToString ());
				scoreNumber.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites [digitGot];
			} else {
				scoreNumber.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [0];
			}
			scoreNumber.SetActive (true);
			counter++;
		}
		pointsCache = 0;
	}
}
