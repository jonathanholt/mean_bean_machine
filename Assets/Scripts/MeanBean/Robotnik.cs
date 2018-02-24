using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Robotnik : MonoBehaviour {
	public string colour;
	public int colourStreak;
	public ArrayList chainOfColour;

	void Start () {
		colour = "0";
		colourStreak = 1;
		chainOfColour = new ArrayList();
	}

	public void plusOneInStreak(){
		colourStreak++;
	}

	public void resetStreak(){
		colourStreak = 1;
	}

	public int chainCount(){
		return chainOfColour.Count;
	}


}
