using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Robotnik : MonoBehaviour {
	public string colour = "NULLVOID";
	public string name;
	public List<string> matches = new List<string> ();
	public List<string>directions = new List<string>();

	void Start () {
		colour = "NULLVOID";
	}
}
