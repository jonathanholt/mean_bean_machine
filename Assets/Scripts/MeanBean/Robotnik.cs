using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Robotnik : MonoBehaviour {
	public string colour;
	public int matches;
	public string name;

	void Start () {
		colour = "0";
		matches = 0;
	}
}
