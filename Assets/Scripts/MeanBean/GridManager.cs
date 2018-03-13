using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour {

	public static Robotnik[,] grid;
	public static int columns = 12;
	public static int rows = 6;
	public static bool deleteTime = false;
	public static int deletei1 = 100;
	public static int deletej1 = 100;
	public static int deletei2 = 100;
	public static int deletej2 = 100;
	public static List<string> blankMatches = new List<string> ();

	//5 == yellow
	//4 == red
	//3 == purple
	//2 == green
	//1 == blue

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

	// Use this for initialization
	void Start () {
		grid = new Robotnik[rows, columns];
		for(int j = 0; j < rows; j++){
		for(int i = 0; i < columns; i++){
				Robotnik newRobotnik = new Robotnik ();
				grid[j, i] = newRobotnik;
				grid [j, i].name = "Grid-" + j + "-" + i;
			}
		}
	}

	public static void changeRobotnikColour(string robotnikString, string colourString){
		//1. locate the member of the array whose name is the same as the parameter string
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){
				if (grid [j, i].name == robotnikString) {
					//2. change that members colour attribute to the colour parameter
					grid [j, i].colour = colourString;
					return;
				}
			}
		}
	}
		
	public static void checkRobotnikMatches(string robotnikString, int isFinal){
		int correcti = 1000;
		int correctj = 1000;
		//1. get the 3D array coordinates of our square based on parameter string
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){

				if (grid [j, i].colour != "NULLVOID") {


					if (grid [j, i].name == robotnikString) {
						correctj = j;
						correcti = i;
						//2. set that square's matches to 1
						Debug.Log ("Before any adds the Count for..."+"Grid-" + j + "-" + i+"="+grid [j, i].matches.Count);
						grid [j, i].matches.Add (grid [j, i].name);
						List<string> removeDupes = grid [j, i].matches.Distinct ().ToList ();
						grid [j, i].matches = removeDupes;
						Debug.Log ("Count for..."+"Grid-" + j + "-" + i+"="+grid [j, i].matches.Count);
						//3. Load each of the neighbouring squares (if not on edge)
						if (j + 1 < rows) {
							//3.1 Check the colour attribute of that square
							if (grid [j + 1, i].colour == grid [j, i].colour) {
								Debug.Log ("Grid-" + j + "-" + i+" is the same colour as "+"Grid-" + (j + 1) + "-" + i+" which is j+1..."+grid [j + 1, i].colour +" vs "+ grid [j, i].colour);
								//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
								grid [j + 1, i].matches.Add (grid [j + 1, i].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j + 1, i].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j + 1, i].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;
								Debug.Log ("Count for..."+"Grid-" + j + "-" + i+"NOW ="+grid [j, i].matches.Count);

								//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
								if (combinedMatches.Count >= 4) {
									Debug.Log ("GREAT THAN 4!");
									deleteTime = true;
									if (isFinal == 0) {
										deletei1 = i;
										deletej1 = j;
									} else {
										deletei2 = i;
										deletej2 = j;
									}
								}
							}	
						}

						if (j - 1 >= 0) {
							//3.1 Check the colour attribute of that square
							if (grid [j - 1, i].colour == grid [j, i].colour) {
								Debug.Log ("Grid-" + j + "-" + i+" is the same colour as "+"Grid-" + (j - 1) + "-" + i+" which is j-1..."+grid [j - 1, i].colour +" vs "+ grid [j, i].colour);
								//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
								grid [j - 1, i].matches.Add (grid [j - 1, i].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j - 1, i].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j - 1, i].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;

								//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
								if (combinedMatches.Count >= 4) {
									deleteTime = true;
									if (isFinal == 0) {
										deletei1 = i;
										deletej1 = j;
									} else {
										deletei2 = i;
										deletej2 = j;
									}
								}
							}	
						}

						if (i - 1 >= 0) {
							//3.1 Check the colour attribute of that square
							if (grid [j, i - 1].colour == grid [j, i].colour) {
								Debug.Log ("Grid-" + j + "-" + i + " is the same colour as " + "Grid-" + j + "-" + (i - 1) + " which is i-1 ..." + grid [j, i - 1].colour + " vs " + grid [j, i].colour);

								//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
								grid [j, i - 1].matches.Add (grid [j, i - 1].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j, i - 1].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j, i - 1].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;

								//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
								if (combinedMatches.Count >= 4) {
									deleteTime = true;
									if (isFinal == 0) {
										deletei1 = i;
										deletej1 = j;
									} else {
										deletei2 = i;
										deletej2 = j;
									}
								}
							}	
						}

						if (i + 1 < columns) {
										//3.1 Check the colour attribute of that square
							if (grid [j, i + 1].colour == grid [j, i].colour) {
								Debug.Log ("Grid-" + j + "-" + i+" is the same colour as "+"Grid-" + j + "-" + (i+1) +" which is i+1 ..."+grid [j, i+1].colour +" vs "+ grid [j, i].colour);

								//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
								grid [j, i + 1].matches.Add (grid [j, i + 1].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j, i + 1].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j, i + 1].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;

								//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
								if (combinedMatches.Count >= 4) {
									deleteTime = true;
									if (isFinal == 0) {
										deletei1 = i;
										deletej1 = j;
									} else {
										deletei2 = i;
										deletej2 = j;
									}
								}
							}	
						}


					}

				}




			}
		}	
		//3.2.1.1. Then we should have a definitive list of which squares to delete. We need to remove duplicates from this list.
		//3.2.1.2. We can cycle through the above list and reset all of the attributes for said list in HERE
	}

	public static void deleteRobotnikMatches(){
		if (deletei1 != 100 && deletej1 != 100) {
			List<string> toDelete = grid [deletej1, deletei1].matches;
			if (deleteTime) {
				//3.2. If we have a flag set, we need to redo the square checking. So we take our original square and make a note of it.
				//EVERYTHING I WROTE HERE SHOULD BE INVALIDATED NOW BECAUSE ALL ROBOTNIK'S HAVE A REFERENCE TO THEIR OWN 
				//toDelete.Add(grid[correctj, correcti].name);
				//3.2.1. Check the left square, lower square and right square, if the squres are the same colour, make a note. AND if it is, cycle through IT'S adjecent squares too
				//3.2.1. Check the left square, lower square and right square, if the squres are the same colour, make a note. AND if it is, cycle through IT'S adjecent squares too
				//3.2.1.1. We then need to cycle through the left, bottom and right squares adjecent squares TOO, and make a note of any with the same colour
				//3.2.1.1. This then needs to happen ONE FINAL TIME with outer squarres.
				for(int j = 0; j < rows; j++){
					for(int i = 0; i < columns; i++){

						if (toDelete.Contains("Grid-" + j + "-" + i)) {
							Debug.Log ("Deleted..." + "Grid-" + j + "-" + i);
							grid[j, i].colour = "NULLVOID";
							grid [j, i].matches = new List<string>();
						}
					}
				}

				deletej1 = 100;
				deletei1 = 100;
				deleteTime = false;
			}
		}

		if (deletei2 != 100 && deletej2 != 100) {
			List<string> toDelete = grid [deletej2, deletei2].matches;
			if (deleteTime) {
				//3.2. If we have a flag set, we need to redo the square checking. So we take our original square and make a note of it.
				//EVERYTHING I WROTE HERE SHOULD BE INVALIDATED NOW BECAUSE ALL ROBOTNIK'S HAVE A REFERENCE TO THEIR OWN 
				//toDelete.Add(grid[correctj, correcti].name);
				//3.2.1. Check the left square, lower square and right square, if the squres are the same colour, make a note. AND if it is, cycle through IT'S adjecent squares too
				//3.2.1. Check the left square, lower square and right square, if the squres are the same colour, make a note. AND if it is, cycle through IT'S adjecent squares too
				//3.2.1.1. We then need to cycle through the left, bottom and right squares adjecent squares TOO, and make a note of any with the same colour
				//3.2.1.1. This then needs to happen ONE FINAL TIME with outer squarres.
				for(int j = 0; j < rows; j++){
					for(int i = 0; i < columns; i++){

						if (toDelete.Contains("Grid-" + j + "-" + i)) {
							Debug.Log ("Deleted..." + "Grid-" + j + "-" + i);
							grid[j, i].colour = "NULLVOID";
							grid [j, i].matches = new List<string>();
						}
					}
				}


			}

			deletej2 = 100;
			deletei2 = 100;
			deleteTime = false;
		}
	}

	public static void dropFunction(){
		for (int j = 0; j < rows; j++) {
			for (int i = 0; i < columns; i++) {
				if (grid [j, i].colour != "NULLVOID") {
					if (i != 0) {
						if (grid [j, i - 1].colour == "NULLVOID") {
							string holderColour1 = grid [j, i].colour;
							string holderColour2 = grid [j, i - 1].colour;
							List<string> holderMatches1 = grid [j, i].matches;
							List<string> holderMatches2 = grid [j, i - 1].matches;

							grid [j, i].colour = holderColour2;
							grid [j, i - 1].colour = holderColour1;
							grid [j, i].matches = holderMatches2;
							grid [j, i - 1].matches = holderMatches1;
								
							Debug.Log("Dropping Grid-"+j+"-"+i+" down to Grid-"+j+"-"+(i-1)+" because "+grid [j, i].colour);
						}
					}			
				}
			}
		}
		//DROP FUNCTION
		//START WITH BOTTOM, CHECK IF BEAN, IF NOT, MOVE ON, IF BEAN && BELOW SQUARES IS EMPTY, SWITCH VALUES, CHECK THE BELOW AGAIN ETC
		//WHEN WE HAVE DONE ALL DROPS THEN WE CAN CYCLE THE GRID AND SET ALL OF THE MATCHES TO 0
		//WE THEN RUN A CHECK ON EVERY SQUARE WITH THE MATCH FUNCTION TOO 
	}

	public static void finalCheckFunction(){
		Debug.Log ("FINAL CHECK");
		for(int j = 0; j < rows; j++){
			string debuggingstring = "[";
			for(int i = 0; i < columns; i++){
				grid [j, i].matches = new List<string>();
				debuggingstring += (grid [j, i].colour+",");
				//Debug.Log("Grid-" + j + "-" + i+" matches ="+grid [j, i].matches.Count);
				//	deleteRobotnikMatches ();
			}
			debuggingstring += "]";
			Debug.Log(debuggingstring);
		}

		Debug.Log ("Everything is blank");

		for(int m = 0; m < 4; m++){
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){
				Debug.Log ("About to check..."+"Grid-" + j + "-" + i);
				grid [j, i].matches = new List<string>();
				Debug.Log ("Matches after just applying blank matches = "+grid [j, i].matches.Count);
				Debug.Log("Grid-" + j + "-" + i+" matches ="+grid [j, i].matches.Count+" AND COLOUR = "+grid [j, i].colour);

				for (int n = 0; n < grid [j, i].matches.Count; n++) {
					Debug.Log("YALL WANNA KNOW "+grid [j, i].matches.ElementAt (n));
				}
				checkRobotnikMatches("Grid-" + j + "-" + i, 2);
				deleteRobotnikMatches ();
			}
		}
		dropFunction ();
		//aaaaand do this 11 times
	}
	}

	public static Robotnik[,] returnGrid(){
		//returns the grid to the player controller to cycle through and do it's UI changes with
		return grid;
	}
								
}
