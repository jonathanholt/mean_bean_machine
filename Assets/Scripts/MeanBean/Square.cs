using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Square : MonoBehaviour {

	public int chain;
	public string colour;
	public int directMatches;
	public List<GameObject> matches;
	public List<GameObject> chains;

	// Use this for initialization
	void Start () {
		chain = 0;
		colour = "";
		directMatches = 0;
	}
		

	public string getColour(){
		return colour;
	}

	public void setColour(string newColour){
		//Debug.Log (newColour);
		colour = newColour;
	}

	public int getDirectmatches(){
		return directMatches;
	}

	public void setDirectMatches(int newMatches){
		directMatches = newMatches;
	}

	public void clearMatches(List<GameObject> freshMatches){
		matches = freshMatches;
	}

	public int getChain(){
		return chain;
	}

	public void setChain(int newChain){
		chain = newChain;
	}

	public void setChainLinks(List<GameObject> masterChain){
		chains = masterChain;
	}


	public void setDirectmatches(int newDirectmatches){
		directMatches = newDirectmatches;
		//Debug.Log("new direct matches= "+directMatches);
	}

	public void addMatch(GameObject match){
		matches.Add(match);
	}

	public void addChain(GameObject chain){
		chains.Add(chain);
	}

	public int chainCount(){
		return chains.Count;
	}

	public List<GameObject> getChainList(){
		return chains;
	}

	public List<GameObject> getMatchList(){
		return matches;
	}

	public void addChainLink(List<GameObject> newChain){
		chains.AddRange(newChain);
		chains = chains.Distinct().ToList();
	}

}
