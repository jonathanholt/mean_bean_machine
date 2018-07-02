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
	public static bool needToRedoGrid = false;

	// The values of colours in sprite sheets
	//5 == yellow
	//4 == red
	//3 == purple
	//2 == green
	//1 == blue

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
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){
				if (grid [j, i].colour != "NULLVOID") {
					if (grid [j, i].name == robotnikString) {
						correctj = j;
						correcti = i;
						grid [j, i].matches.Add (grid [j, i].name);
						List<string> removeDupes = grid [j, i].matches.Distinct ().ToList ();
						grid [j, i].matches = removeDupes;
						if (j + 1 < rows) {
							if (grid [j + 1, i].colour == grid [j, i].colour) {
								grid [j, i].directions.Add (grid [j + 1, i].name);
								grid [j + 1, i].matches.Add (grid [j + 1, i].name);

								grid [j + 1, i].matches.Add (grid [j + 1, i].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j + 1, i].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j + 1, i].matches = combinedMatches;
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

						if (j - 1 >= 0) {
							if (grid [j - 1, i].colour == grid [j, i].colour) {
								grid [j - 1, i].matches.Add (grid [j - 1, i].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j - 1, i].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j - 1, i].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;
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
							if (grid [j, i - 1].colour == grid [j, i].colour) {
								grid [j, i - 1].matches.Add (grid [j, i - 1].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j, i - 1].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j, i - 1].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;
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
							if (grid [j, i + 1].colour == grid [j, i].colour) {
								grid [j, i + 1].matches.Add (grid [j, i + 1].name);
								List<string> combinedMatches = grid [j, i].matches;
								combinedMatches.AddRange (grid [j, i + 1].matches);
								combinedMatches = combinedMatches.Distinct ().ToList ();
								grid [j, i + 1].matches = combinedMatches;
								grid [j, i].matches = combinedMatches;
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
	}

	public static void deleteRobotnikMatches(){
		if (deletei1 != 100 && deletej1 != 100) {

			List<string> toDelete = grid [deletej1, deletei1].matches;
			if (deleteTime) {
				needToRedoGrid = true;
				for(int j = 0; j < rows; j++){
					for(int i = 0; i < columns; i++){
						if (toDelete.Contains("Grid-" + j + "-" + i)) {
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
				needToRedoGrid = true;
				for(int j = 0; j < rows; j++){
					for(int i = 0; i < columns; i++){

						if (toDelete.Contains("Grid-" + j + "-" + i)) {
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
						}
					}			
				}
			}
		}
	}

	public static void finalCheckFunction(){
		for(int j = 0; j < rows; j++){
			string debuggingstring = "[";
			for(int i = 0; i < columns; i++){
				grid [j, i].matches = new List<string>();
				debuggingstring += (grid [j, i].colour+",");
			}
			debuggingstring += "]";
		}
			

		for(int m = 0; m < 4; m++){
		for(int j = 0; j < rows; j++){
			for(int i = 0; i < columns; i++){
				grid [j, i].matches = new List<string>();
				checkRobotnikMatches("Grid-" + j + "-" + i, 2);
				deleteRobotnikMatches ();
			}
		}
		dropFunction ();
	}
}

	public static Robotnik[,] returnGrid(){
		needToRedoGrid = false;
		return grid;
	}
								
}
