using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalancheController : MonoBehaviour {

	public int avalancheCount;
	
	void Start () {
		avalancheCount = 0;
	}
	
	public void incrementAvalancheCount(){
		avalancheCount += 1;
	}
	
	public void processAvalanche(){
		avalancheCount = 0;
	}
}
