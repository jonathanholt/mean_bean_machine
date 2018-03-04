using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	public static Robotnik[,] grid;
	public static int columns = 12;
	public static int rows = 6;

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
				Debug.Log ("Debug manager..." + grid [j, i].name);
			}
		}
	}

	public static void changeRobotnikColour(string robotnikString, int colourString){
		//1. locate the member of the array whose name is the same as the parameter string
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){
				if (grid [j, i].name == robotnikString) {
					//2. change that members colour attribute to the colour parameter
					grid [j, i].colour = colourString.ToString ();
				} else {
					Debug.Log ("No match found!");
				}
			}
		}
	}
		
	public static void checkRobotnikMatches(string robotnikString){
		bool deleteTime = false;
		int correcti = 1000;
		int correctj = 1000;
		//1. get the 3D array coordinates of our square based on parameter string
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){
				if (grid [j, i].name == robotnikString) {
					correctj = j;
					correcti = i;
					//2. set that square's matches to 1
					grid [j, i].matches = 1;
					//3. Load each of the neighbouring squares (if not on edge)
					if(j+1 < rows){
						//3.1 Check the colour attribute of that square
						if (grid [j + 1, i].colour == grid [j, i].colour) {
							//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
							int combinedMatchesCount = grid [j + 1, i].matches + grid [j, i].matches; 
							grid [j + 1, i].matches = combinedMatchesCount;
							grid [j, i].matches = combinedMatchesCount;

							//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
							if(combinedMatchesCount >= 4){
								deleteTime = true;
							}
						}	
					}

					if(j-1 >= 0){
						//3.1 Check the colour attribute of that square
						if (grid [j - 1, i].colour == grid [j, i].colour) {
							//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
							int combinedMatchesCount = grid [j - 1, i].matches + grid [j, i].matches; 
							grid [j - 1, i].matches = combinedMatchesCount;
							grid [j, i].matches = combinedMatchesCount;

							//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
							if(combinedMatchesCount >= 4){
								deleteTime = true;
							}
						}	
					}

					if(i-1 >= 0){
						//3.1 Check the colour attribute of that square
						if (grid [j, i - 1].colour == grid [j, i].colour) {
							//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
							int combinedMatchesCount = grid [j, i - 1].matches + grid [j, i].matches; 
							grid [j, i - 1].matches = combinedMatchesCount;
							grid [j, i].matches = combinedMatchesCount;

							//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
							if(combinedMatchesCount >= 4){
								deleteTime = true;
							}
						}	
					}

					if(i+1 < columns){
						//3.1 Check the colour attribute of that square
						if (grid [j, i + 1].colour == grid [j, i].colour) {
							//3.1.1. If the colour attribute is the same, then extract the match value of both squares, combine it and reassign to the squares
							int combinedMatchesCount = grid [j, i + 1].matches + grid [j, i].matches; 
							grid [j, i + 1].matches = combinedMatchesCount;
							grid [j, i].matches = combinedMatchesCount;

							//3.1.2. If the match attribute reaches 4, then set a flag because deleting needs to happen
							if(combinedMatchesCount >= 4){
								deleteTime = true;
							}
						}	
					}


				}
			}
		}

		List<string> toDelete = new List<string> ();
		if (deleteTime) {
			//3.2. If we have a flag set, we need to redo the square checking. So we take our original square and make a note of it.
			toDelete.Add(grid[correctj, correcti].name);
			//3.2.1. Check the left square, lower square and right square, if the squres are the same colour, make a note. AND if it is, cycle through IT'S adjecent squares too
			//3.2.1. Check the left square, lower square and right square, if the squres are the same colour, make a note. AND if it is, cycle through IT'S adjecent squares too
			//3.2.1.1. We then need to cycle through the left, bottom and right squares adjecent squares TOO, and make a note of any with the same colour
			//3.2.1.1. This then needs to happen ONE FINAL TIME with outer squarres.
			if(correctj+1 < rows){
				if (grid [correctj + 1, correcti].colour == grid [correctj, correcti].colour) {
					toDelete.Add(grid [correctj + 1, correcti].name);
					//squares to check
					//1. correctj+2, i
					//2. correctj+1, correcti +1
					//3. correctj+1, correcti-1
				}
			}

			if(correctj-1 >= 0){
				if (grid [correctj - 1, correcti].colour == grid [correctj, correcti].colour) {
					toDelete.Add(grid [correctj - 1, correcti].name);
				}	
			}

			if(correcti-1 >= 0){
				if (grid [correctj, correcti - 1].colour == grid [correctj, correcti].colour) {
					toDelete.Add(grid [correctj, correcti - 1].name);
				}
			}

			if(correcti+1 < columns){
				if (grid [correctj + 1, correcti].colour == grid [correctj, correcti].colour) {
					toDelete.Add(grid [correctj + 1, correcti].name);
				}
			}

			foreach(string toDeleteNode in toDelete){
				Debug.Log(toDeleteNode);
			}
		}
			
		//3.2.1.1. Then we should have a definitive list of which squares to delete. We need to remove duplicates from this list.
		//3.2.1.2. We can cycle through the above list and reset all of the attributes for said list in HERE



	}

	public void dropFunction(){
		//DROP FUNCTION
		//START WITH BOTTOM, CHECK IF BEAN, IF NOT, MOVE ON, IF BEAN && BELOW SQUARES IS EMPTY, SWITCH VALUES, CHECK THE BELOW AGAIN ETC
		//WHEN WE HAVE DONE ALL DROPS THEN WE CAN CYCLE THE GRID AND SET ALL OF THE MATCHES TO 0
		//WE THEN RUN A CHECK ON EVERY SQUARE WITH THE MATCH FUNCTION TOO 
	}

	public Robotnik[,] returnGrid(){
		//returns the grid to the player controller to cycle through and do it's UI changes with
		return grid;
	}
}
