using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {

	//Mean Bean Variables
	private GameObject startPoint;
	private int row;
	private int[] squares = new int[] {0, 0, 0, 0, 0, 0};
	public GameObject square1;
	public GameObject square2;
	public int beanOrientation;
	public string[] positions = new string[] {"beanHolderUp", "beanHolderLeft", "beanHolderDown", "beanHolderRight"};
	public List<GameObject> matches;


	/*
	 * 	Called at the start of the level once. Sets some basic variable values
	 */ 
	void Start () {
		startPoint = GameObject.Find("StartPoint");
		row = 3;
		beanOrientation = 0;
	}
	
	/*
	 *	Update is called once per frame. Used to detect key up events
	 */ 
	void Update () {
		if (Input.GetKeyUp ("down")) {
		
		}
		if (Input.GetKeyUp ("left") && row > 1 && !(beanOrientation == 1 && row == 2)) {
			transform.position += Vector3.left * 0.53f;
			row--;
		}
		if (Input.GetKeyUp ("right") && row < 6 && !(beanOrientation == 3 && row == 5)) {
			Debug.Log (beanOrientation + " " + row);
			transform.position += Vector3.right * 0.53f;
			row++;
		}
		if (Input.GetKeyUp ("a")) {
			//anticlockwise 
			if ((beanOrientation % 4 == 2 && row == 1) || (row == 6 && beanOrientation % 4 == 0)) {
				Debug.Log ("Not OKAY!");
			}else {
				beanOrientation += 3;
				rotate ();
			}
		}
		if (Input.GetKeyUp ("s")) {
			//anticlockwise
			if ((beanOrientation%4 == 0 && row == 1) || (row == 6 && beanOrientation%4 == 2)) {
				Debug.Log ("Not OKAY!");
			} else {
				//clockwise
				beanOrientation += 1;
				rotate ();
			}
		}
    }

	public void rotate(){
		beanOrientation = beanOrientation % 4;
		//Debug.Log (beanOrientation);
		GameObject beanToMove = GameObject.Find("bean2");
		beanToMove.transform.position = GameObject.Find(positions[Mathf.Abs(beanOrientation)]).transform.position;
	}

	/*
	 * 	Detect Player collision with the bottom of the grid and call functions
	 */ 
	public void OnCollisionEnter2D(Collision2D other){
		updateGrid ();
		reinitGame ();
	}

	/*
	 * 	Start player off at top of screen with new bean pair
	 */ 
	public void reinitGame(){
		transform.position = startPoint.transform.position;
		NewBean.createNewBeanPair ();
		row = 3;
	}

	public void updateGrid(){
		getSquaresToUpdate ();
	}

	/*
	 * 	Work out which squares the beans have landed in and build the string of these squares
	 */ 
	public void getSquaresToUpdate(){
		int beanOrientationPositive = Mathf.Abs(beanOrientation);
		string square1FindString;
		string square2FindString;
		int square1DropNumber = 0;
		int square2DropNumber = 0;
		if(beanOrientationPositive == 1){
			squares[row-1] += 1;
			squares[row-2] += 1;
			if (squares [row - 1] > squares [row - 2]) {
				square2DropNumber = squares [row - 1] - squares [row - 2];
			}
			if (squares [row - 1] < squares [row - 2]) {
				square1DropNumber = squares [row - 2] - squares [row - 1];
			}
			square1FindString = "Grid-" + (row - 1) + "-" + ((squares [row - 1] - 1) - square1DropNumber);
			square2FindString = "Grid-"+(row-2)+"-"+((squares[row-1] - 1) - square2DropNumber);
		}
		else if(beanOrientationPositive == 2){
			squares[row-1] += 2;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
				square2FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 2);
		}
		else if (beanOrientationPositive == 3){
			squares[row-1] += 1;
			squares[row] += 1;
			if (squares [row] > squares [row - 1]) {
				square1DropNumber = squares [row] - squares [row - 1];
			}
			if (squares [row] < squares [row - 1]) {
				square2DropNumber = squares[row-1] - squares[row];
			}

			square1FindString = "Grid-"+(row-1)+"-"+((squares[row-1] - 1) - square1DropNumber);
			square2FindString = "Grid-"+(row)+"-"+((squares[row-1] - 1) - square2DropNumber);
		}
		else{
			squares[row-1] += 2;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 2);
			square2FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
		}
			
		square1 = GameObject.Find(square1FindString);
		square2 = GameObject.Find(square2FindString);
		updateSquareProperties (square1, square2, square1FindString, square2FindString);
	}

	/*
	 *	Pass in two squares and add a BoxCollider
	 */ 
	public void updateSquareProperties(GameObject square1, GameObject square2, string square1FindString, string square2FindString){
		Object [] sprites;
		sprites = Resources.LoadAll ("beans");
		square1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [NewBean.randomBean1];
		square2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [NewBean.randomBean2];
		if (square1.GetComponent<BoxCollider2D> () == null) {
			square1.AddComponent<BoxCollider2D> ();
			float width = GetComponent<SpriteRenderer> ().bounds.size.x;
			float height = GetComponent<SpriteRenderer> ().bounds.size.y;
			square1.GetComponent<BoxCollider2D> ().size = new Vector3(height, width, width);
		}
		if (square2.GetComponent<BoxCollider2D> () == null) {
			square2.AddComponent<BoxCollider2D> ();
			float width = GetComponent<SpriteRenderer> ().bounds.size.x;
			float height = GetComponent<SpriteRenderer> ().bounds.size.y;
			square2.GetComponent<BoxCollider2D> ().size = new Vector3(height, width, width);
		}
		square1.tag = "Ground";
		square2.tag = "Ground";
		findAdjacentSquares(square1FindString, square1, NewBean.randomBean1);
		findAdjacentSquares(square2FindString, square2, NewBean.randomBean2);
	}

	/*
	 *	Finds adjacent squares and passes along with original square to another function
	 */ 
	public void findAdjacentSquares(string currentSquare, GameObject square1, int colour){
		Square squaresquare = square1.GetComponent<Square>();
		string[] colours = {"blue","green","purple","red","yellow"};
		squaresquare.setColour (colours[colour-1]);
		string[] textSplit = currentSquare.Split (new string[]{ "-" }, System.StringSplitOptions.None);
		int firstNumber = int.Parse (textSplit [1]);
		int secondNumber = int.Parse (textSplit [2]);
		for (int i = 0; i < 4; i++) {
			string newString;
			if (i == 0 && (firstNumber - 1) > -1) {
				newString = "Grid-"+(firstNumber-1)+"-"+secondNumber;
				checkingMatches (newString, square1);
			} else if (i == 1 && (secondNumber - 1) > -1) {
				newString = "Grid-"+firstNumber+"-"+(secondNumber-1);
				checkingMatches (newString, square1);
			} else if (i == 2 && (firstNumber + 1) < 6) {
				newString = "Grid-"+(firstNumber+1)+"-"+secondNumber;
				checkingMatches (newString, square1);
			} else if (i == 3 && (secondNumber + 1) < 12) {
				newString = "Grid-"+firstNumber+"-"+(secondNumber+1);
				checkingMatches (newString, square1);
			}
		}
	}

	/*
	 * 	Updates square and adjacent square matches and calls delete function if needed
	 */ 
	public void checkingMatches(string newString, GameObject square1){
		GameObject newSquareOb = GameObject.Find(newString);
		Square squaresquare = square1.GetComponent<Square>();
		Square newsquaresquare = newSquareOb.GetComponent<Square>();
		if(newsquaresquare.getColour() != "" && squaresquare.getColour() != "" && newsquaresquare.getColour() == squaresquare.getColour()){
			newsquaresquare.setDirectmatches(newsquaresquare.getDirectmatches()+1);
			newsquaresquare.addMatch (square1);
			if(newsquaresquare.getDirectmatches() == 4){
				getDeleting(newsquaresquare.getMatchList(), newSquareOb);
			}
			squaresquare.setDirectmatches(squaresquare.getDirectmatches()+1);
			squaresquare.addMatch (newSquareOb);
			if(squaresquare.getDirectmatches() == 4){
				getDeleting(squaresquare.getMatchList(), square1);
			}
				
			if (newsquaresquare.getChain () == 0 && squaresquare.getChain () == 0) {
				newsquaresquare.setChain (newsquaresquare.getChain () + 1 + squaresquare.getChain () + 1);
				newsquaresquare.addChain (square1);
				newsquaresquare.addChain (newSquareOb);
				squaresquare.addChain (newSquareOb);
				squaresquare.addChain (square1);
			} else {
				newsquaresquare.setChain (newsquaresquare.getChain () + squaresquare.getChain () + 1);
				newsquaresquare.addChainLink (squaresquare.getChainList());
			
			}
			if(newsquaresquare.getChain() == 4){
				getDeleting(newsquaresquare.getChainList(), newSquareOb);
				getDeleting(squaresquare.getMatchList(), square1);
			}
			squaresquare.setChain(squaresquare.getChain() + newsquaresquare.getChain());
			squaresquare.addChainLink (newsquaresquare.getChainList ());
			if(squaresquare.getChain() == 4){
				getDeleting(squaresquare.getChainList(), square1);
				getDeleting(squaresquare.getMatchList(), square1);
			}
		}
	}


	/*
	 * Delete properties of squares who had appropriate number of matches
	 */ 
	public void getDeleting(List<GameObject> culprits, GameObject finalDeletion){
		List<GameObject> affectedSquares = new List<GameObject>();
		matches = culprits;
		foreach(GameObject newgameobject in culprits){
			affectedSquares.Add (newgameobject);
			newgameobject.GetComponent<SpriteRenderer> ().sprite = null;
			Destroy(newgameobject.GetComponent<BoxCollider2D> ());
			Square squaresquare = newgameobject.GetComponent<Square>();
			squaresquare.setColour ("");
			List<GameObject> matchesmatches = squaresquare.getMatchList ();
			foreach(GameObject newgameobjectinner in matchesmatches){
				newgameobject.GetComponent<SpriteRenderer> ().sprite = null;
				Destroy(newgameobject.GetComponent<BoxCollider2D> ());
				affectedSquares.Add (newgameobjectinner);
			}
			List<GameObject> matchesmatches2 = squaresquare.getChainList ();
			foreach(GameObject newgameobjectinner in matchesmatches2){
				newgameobject.GetComponent<SpriteRenderer> ().sprite = null;
				affectedSquares.Add (newgameobjectinner);
			}
		}
		finalDeletion.GetComponent<SpriteRenderer> ().sprite = null;
		Square squaresquaresquare = finalDeletion.GetComponent<Square>();
		squaresquaresquare.setColour ("");

		List<GameObject> noDupes = affectedSquares.Distinct().ToList();
		foreach (GameObject noDupe in noDupes) {
			string name = noDupe.transform.name;
			string[] textSplit = name.Split (new string[]{ "-" }, System.StringSplitOptions.None);
			int firstNumber = int.Parse (textSplit [1]);
			int secondNumber = int.Parse (textSplit [2]);
			squares [secondNumber] -= 1;
			}
		gridRearrange ();
		}

	/*
	 * 	Post-deletion reorganisation of grid.
	 */ 
	public void gridRearrange(){
		for(int newintorig = 0; newintorig < 6; newintorig ++){
			int[] squareValues = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
			for(int newint = 0; newint < 12; newint ++){
				GameObject newSquareOb = GameObject.Find("Grid-"+newintorig+"-"+newint);
				Square newsquaresquare = newSquareOb.GetComponent<Square>();
				if(newsquaresquare.getColour() != "" ){
					squareValues [newint] = 1;
				}
			}

			bool hitAZero = false;
			int firstZeroPosition = 0;
			int counter = 0;
			foreach(int squareValue in squareValues){
				//Debug.Log ("Grid-"+newintorig+"-"+counter+" = "+squareValue);
				if(squareValue == 0 && !hitAZero){
					hitAZero = true;
					firstZeroPosition = counter;
				}
				if(squareValue == 1 && hitAZero){
					//Debug.Log ("First zero position = " + firstZeroPosition);
					//Debug.Log ("This is square..." + "Grid-" + newintorig + "-" + firstZeroPosition);
					//do all of the swapping here!
					GameObject newSquareOb = GameObject.Find("Grid-"+newintorig+"-"+counter);
					Square newsquaresquare = newSquareOb.GetComponent<Square>();
					GameObject newSquareOb2 = GameObject.Find("Grid-"+newintorig+"-"+firstZeroPosition);
					Square newsquaresquare2 = newSquareOb.GetComponent<Square>();

					Sprite placeHolderSprite = newsquaresquare.GetComponent<SpriteRenderer> ().sprite;
					Sprite placeHolderSprite2 = newsquaresquare2.GetComponent<SpriteRenderer> ().sprite;

					Object [] sprites;
					sprites = Resources.LoadAll ("beans");
					newsquaresquare.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [NewBean.randomBean1];
					newsquaresquare2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [NewBean.randomBean2];
					Debug.Log ("Swapping happening");
					Debug.Log ("Grid-"+newintorig+"-"+counter);
					Debug.Log ("Grid-"+newintorig+"-"+firstZeroPosition);
					return;
				}
				counter ++;
		}
	}
	}
}