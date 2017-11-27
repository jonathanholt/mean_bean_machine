using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	public int chain;
	private string colour;
	public int directMatches;
	private int readyForDeletion;
	public GameObject[] matches;
	public GameObject[] chains;

	// Use this for initialization
	void Start () {
		chain = 0;
		colour = "";
		directMatches = 0;
		readyForDeletion = 0;
	}

	public int getChain(){
		return chain;
	}

	public string getColour(){
		return colour;
	}

	public int getDirectmatches(){
		return directMatches;
	}

	public void setChain(int newChain){
		chain = newChain;
	}

	public void setColour(string newColour){
		//Debug.Log (newColour);
		colour = newColour;
	}

	public void setDirectmatches(int newDirectmatches){
		directMatches = newDirectmatches;
		Debug.Log("new direct matches= "+directMatches);
	}

}
