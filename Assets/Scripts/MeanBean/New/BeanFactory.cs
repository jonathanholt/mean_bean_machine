using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanFactory : MonoBehaviour {
	
	public GameObject startPoint;

	public void createFirstBean () {
		GameObject test = Instantiate(Resources.Load("purple1")) as GameObject;
		test.name = "purple1";
		test.transform.position = startPoint.transform.position;
	}
}
