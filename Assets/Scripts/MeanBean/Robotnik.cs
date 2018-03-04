using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Robotnik : MonoBehaviour {
	public string colour;
	public string name;
	public List<string> matches = new List<string> ();

	void Start () {
		colour = "NA";
	}
}
