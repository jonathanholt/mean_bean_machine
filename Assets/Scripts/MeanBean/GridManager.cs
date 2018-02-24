using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	private Robotnik[,] grid;
	private int rows = 6;
	private int columns = 12;

	//3D array, examples
	//row 1 [0,0,0,0,0],
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,0,0,0]
	//		[0,0,{TILENAME P},0,0]
	//		[0,0,{TILENAME P},0,0]

	//update colour...

	//int matches...set matches to 0 for square that has a bean just landed in it. Then, check the 4 squares nearby for matches
	// update the matches property respectively, in the above case, it'd be 1. We need to simultaenously update both of these properties 
	// at the same time.
    //WHAT IS WE GET 4 matches?
	// GO THROUGH THE GRID AND COLLECT THE NAMES OF ANY WITH A MATCH PROPERTY OF 3, pass back to playercontroller for changes, and then update grid here

	//DROP FUNCTION
	//START WITH BOTTOM, CHECK IF BEAN, IF NOT, MOVE ON, IF BEAN && BELOW SQUARES IS EMPTY, SWITCH VALUES, CHECK THE BELOW AGAIN AND PERFORM MATCH CHECK
	//WE COULD JUST RUN MATCH FUNCTION AFTER ALL DROPS, BUT THAT REQUIRES RESETTING ALL MATCHES TO 0 AGAIN



	//CHAINS...
	//CHAINS NEED TO BE MERGED LISTS OF MATCHES. AN ARRAY. SO, TWO SQUARES MATCH AND THE MATCHING FUNCTION HAPPENS OK FINE,
	// EACH SQUARE ALSO NEEDS A LIST, IT WILL, IF IT'S NEIGHBOUR IS THE SAME, MERGE IT'S CHAINS TOGETHER
	//BEFORE DOING THIS, WE NEED TO WORK OUT THE DIFFERENCE! IF WE COMBINE THESE TWO CHAINS, IS THERE A NEW VALUE BEING ADDED THAT ISN@T
	// EITHER OF THE SQUARES, iF SO ADD IT TO A TEMPT LIST AND THEN MERGE IT TOO
	// WE CAN CHECK THE LENGTH OF LISTS< ANY WITH 4 NEED TO BE DELETED

	// Use this for initialization
	void Start () {
		grid = new Robotnik[columns, rows];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
