using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour {

	//Mean Bean Variables
	private GameObject startPoint;
	public int row;
	private int[] squares = new int[] {0, 0, 0, 0, 0, 0};
	public GameObject square1;
	public GameObject square2;
	public int beanOrientation;
	public string[] positions = new string[] {"beanHolderUp", "beanHolderLeft", "beanHolderDown", "beanHolderRight"};
	public List<GameObject> matches;
	public string objectCollidedWith;
	public string lastPressed;
	public Vector3 oldPosition;
	public Rigidbody2D rigid2D;
	public int runCount;
	public bool gameover;


	/*
	 * 	Called at the start of the level once. Sets some basic variable values
	 */ 
	void Start () {
		startPoint = GameObject.Find("StartPoint");
		row = 3;
		beanOrientation = 0;
		rigid2D = GetComponent<Rigidbody2D>();
		rigid2D.velocity = new Vector3(0, -2, 0);
		GameObject rightBeanHolder = GameObject.Find("beanHolderRight");
		GameObject leftBeanHolder = GameObject.Find("beanHolderLeft");
		rightBeanHolder.GetComponent<Collider2D>().enabled = false;
		leftBeanHolder.GetComponent<Collider2D>().enabled = false;
	}
	
	/*
	 *	Update is called once per frame. Used to detect key up events
	 */ 
	void Update () {
		if (!gameover) {
			if (Input.GetKeyDown ("down")) {
				rigid2D.velocity = new Vector3 (0, -4, 0);
			}
			if (Input.GetKeyUp ("down")) {
				rigid2D.velocity = new Vector3 (0, -2, 0);
			}
			if (Input.GetKeyUp ("left")) {
				leftMove ();
			}
			if (Input.GetKeyUp ("right")) {
				rightMove ();
			}
			if (Input.GetKeyUp ("a")) {
				rotateMoveA ();
				//anticlockwise 
			}
			if (Input.GetKeyUp ("s")) {
				rotateMoveB ();
				//anticlockwise
			}
			if (Input.GetKeyUp ("z")) {
				rigid2D.velocity = new Vector3 (0, 0, 0);
			}
			if (Input.GetKeyUp ("x")) {
				Time.timeScale = 1;
			}
		}
    }

	public void rotateMoveA(){
		if ((beanOrientation % 4 == 2 && row == 1) || (row == 6 && beanOrientation % 4 == 0)) {
		}else {
			beanOrientation += 3;
			rotate ();
		}
	}

	public void rotateMoveB(){
		if ((beanOrientation%4 == 0 && row == 1) || (row == 6 && beanOrientation%4 == 2)) {
		} else {
			//clockwise
			beanOrientation += 1;
			rotate ();
		}
	}

	public void leftMove(){
		if(row > 1 && !(beanOrientation == 1 && row == 2)) {
			oldPosition = transform.position;
			lastPressed = "l";
			transform.position += Vector3.left * 0.53f;
			row--;
		}
	}

	public void rightMove(){
		if(row < 6 && !(beanOrientation == 3 && row == 5)) {
			oldPosition = transform.position;
			lastPressed = "r";
			transform.position += Vector3.right * 0.53f;
			row++;
		}
	}

    /*
	 *	Rotate bean pair 90 degress, if possible
	 */ 
	public void rotate(){
		beanOrientation = beanOrientation % 4;
		GameObject beanToMove = GameObject.Find("bean2");
		beanToMove.transform.position = GameObject.Find(positions[Mathf.Abs(beanOrientation)]).transform.position;
		if (beanOrientation == 3) {
			GameObject rightBeanHolder = GameObject.Find("beanHolderRight");
			GameObject leftBeanHolder = GameObject.Find("beanHolderLeft");
			rightBeanHolder.GetComponent<Collider2D>().enabled = true;
			leftBeanHolder.GetComponent<Collider2D> ().enabled = false;
		}
		else if(beanOrientation == 1){
			GameObject rightBeanHolder = GameObject.Find("beanHolderRight");
			GameObject leftBeanHolder = GameObject.Find("beanHolderLeft");
			leftBeanHolder.GetComponent<Collider2D>().enabled = true;
			rightBeanHolder.GetComponent<Collider2D>().enabled = false;
		}
		else{
			GameObject rightBeanHolder = GameObject.Find("beanHolderRight");
			GameObject leftBeanHolder = GameObject.Find("beanHolderLeft");
			rightBeanHolder.GetComponent<Collider2D>().enabled = false;
			leftBeanHolder.GetComponent<Collider2D>().enabled = false;
		}
	}

	/*
	 * 	Detect Player collision with the bottom of the grid and call functions
	 */ 
	public void OnCollisionEnter2D(Collision2D other){
		objectCollidedWith = other.collider.gameObject.name;
		if (objectCollidedWith != "Ground") {
			string[] textSplit = objectCollidedWith.Split (new string[]{ "-" }, System.StringSplitOptions.None);
			int firstNumber = int.Parse (textSplit [1]);
			int secondNumber = int.Parse (textSplit [2]);
			if (isMoveAllowed (firstNumber, secondNumber)) {
				updateGrid ();
				reinitGame ();
			} else {
				transform.position = oldPosition;
				if (lastPressed == "r") {
					row--;
				}
				if (lastPressed == "l") {
					row++;
				}
			}
		} else {
			updateGrid ();
			reinitGame ();
		}
	}

    /*
	 *	Check if moving bean into a certain square is allowed
	 */ 
	public bool isMoveAllowed(int firstNumber, int secondNumber){
		if ((secondNumber + 1) < squares [firstNumber]) {
			return false;
		} else {
			return true;
		}
	}

	public void childCollision(){
	}

	/*
	 * 	Start player off at top of screen with new bean pair
	 */ 
	public void reinitGame(){
		if (!gameover) {
			transform.position = startPoint.transform.position;
			NewBean.createNewBeanPair ();
			row = 3;
			rigid2D.velocity = new Vector3 (0, -2, 0);
			beanOrientation = 0;
			rotate ();
		}
	}

    /*
	 *	Entry point for all of the grid update classes and methods
	 */ 
	public void updateGrid(){
		getSquaresToUpdate ();
	}

	/*
	 * 	Work out which squares the beans have landed in and build the string of these squares
	 */ 
	public void getSquaresToUpdate(){
		int beanOrientationPositive = Mathf.Abs(beanOrientation);
		string square1FindString = null;
		string square2FindString = null;
		int square1DropNumber = 0;
		int square2DropNumber = 0;
		if(beanOrientationPositive == 1){
			if (objectCollidedWith != "Ground") {
				string[] textSplit = objectCollidedWith.Split (new string[]{ "-" }, System.StringSplitOptions.None);
				int firstNumber = int.Parse (textSplit [1]);
				int secondNumber = int.Parse (textSplit [2]);
				square1FindString = "Grid-" + (row - 1) + "-" + ((squares [row - 1]));
				square2FindString = "Grid-"+(row-2)+"-"+((squares[row-2]));
			}

			squares[row-1] += 1;
			squares[row-2] += 1;
			if (squares [row - 1] > squares [row - 2]) {
				square2DropNumber = squares [row - 1] - squares [row - 2];
			}
			if (squares [row - 1] < squares [row - 2]) {
				square1DropNumber = squares [row - 2] - squares [row - 1];
			}
			if (square1FindString == null) {
				square1FindString = "Grid-" + (row - 1) + "-" + ((squares [row - 1] - 1) - square1DropNumber);
			}
			if (square2FindString == null) {
				square2FindString = "Grid-" + (row - 2) + "-" + ((squares [row - 1] - 1) - square2DropNumber);
			}
		}
		else if(beanOrientationPositive == 2){
			squares[row-1] += 2;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
				square2FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 2);
		}
		else if (beanOrientationPositive == 3){
			//if the row from the number above doesn't equal the equation below, we need to do something different
			//so we need to break down this string
			square1FindString = null;
			square2FindString = null;
			if (objectCollidedWith != "Ground") {
				string[] textSplit = objectCollidedWith.Split (new string[]{ "-" }, System.StringSplitOptions.None);
				int firstNumber = int.Parse (textSplit [1]);
				int secondNumber = int.Parse (textSplit [2]);
					if (firstNumber != row - 1) {
						square2FindString = "Grid-"+(row)+"-"+((squares[row]));
						square1FindString = "Grid-" + (row - 1)+ "-"+((squares[row-1]));
					}
			}
			squares[row-1] += 1;
			squares[row] += 1;
			if (squares [row] > squares [row - 1]) {
				square1DropNumber = squares [row] - squares [row - 1];
			}
			if (squares [row] < squares [row - 1]) {
				square2DropNumber = squares[row-1] - squares[row];
			}
			if(square1FindString == null){
			square1FindString = "Grid-" + (row - 1);
			square1FindString = square1FindString + "-";
			square1FindString = square1FindString + ((squares[row-1] - 1) - square1DropNumber);
			}
			if(square2FindString == null){
				square2FindString = "Grid-"+(row)+"-"+((squares[row-1] - 1) - square2DropNumber);
			}

		}
		else{
			squares[row-1] += 2;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 2);
			square2FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
		}
			
		if ((square1FindString.Contains ("12") || square1FindString.Contains ("13")) || (square2FindString.Contains ("12")) || square2FindString.Contains ("13")) {
			gameOver ();
			Debug.Log ("Exited");
		} else {
			square1 = GameObject.Find (square1FindString);
			square2 = GameObject.Find (square2FindString);
			updateSquareProperties (square1, square2, square1FindString, square2FindString);
		}
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
			
			// step 1. Add square 2 to square1s chain
			squaresquare.addChain(newSquareOb);
			// step 2. Add square1 to square2s chain
			newsquaresquare.addChain(square1);
			// step 3. Merge square1 and square2s chain into a master chain
			List<GameObject> masterChain = squaresquare.getChainList();
			List<GameObject> masterChain2 = newsquaresquare.getChainList();
			masterChain.AddRange(masterChain2);
			masterChain = masterChain.Distinct().ToList();
			// step 4. Loop through this master chain and assign the master chain to the chain value for each square! Complicated but 
			foreach(GameObject masterChainLink in masterChain){
				Square masterChainLinkSquare = masterChainLink.GetComponent<Square>();
				masterChainLinkSquare.setChainLinks (masterChain);
			}
			// should work
			// step 5. Check if master chain count if equal or greater than 5
			// step 6. Proceed with 'get deleting' for the masterchain
			if(masterChain.Count >= 4){
				getDeleting(masterChain, null);
			}
			//step 7. Ensure chain values are all cleared in the deletion function just like they are for matchs
		

			newsquaresquare.setDirectmatches(newsquaresquare.getDirectmatches()+1);
			newsquaresquare.addMatch (square1);
			squaresquare.setDirectmatches(squaresquare.getDirectmatches()+1);
			squaresquare.addMatch (newSquareOb);

			if(newsquaresquare.getDirectmatches() >= 3){
				getDeleting(newsquaresquare.getMatchList(), square1);
			}
			if(squaresquare.getDirectmatches() >= 3){
				getDeleting(squaresquare.getMatchList(), newSquareOb);
			}
		}
	}


	/*
	 * Delete properties of squares who had appropriate number of matches
	 */ 
	public void getDeleting(List<GameObject> culprits, GameObject finalDeletion = null){

		ScoreManager.AddPoints (10);
		ScoreManager.AlterGUI ();
		HasBeanController.chooseAnimation ();
		List<GameObject> affectedSquares = new List<GameObject>();
		matches = culprits;
		foreach(GameObject newgameobject in culprits){
			affectedSquares.Add (newgameobject);
			// Cleaning up square
			Object [] sprites;
			sprites = Resources.LoadAll<Sprite> ("GPJ_2D_Platformer_Sprites");
			newgameobject.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites [0];
			Destroy(newgameobject.GetComponent<BoxCollider2D> ());
			Square squaresquare = newgameobject.GetComponent<Square>();
			squaresquare.setColour ("");
			squaresquare.setDirectMatches (0);
			squaresquare.setChain (0);
			List<GameObject> fresh = new List<GameObject>();
			squaresquare.clearMatches (fresh);
			squaresquare.setChainLinks (fresh);
		}
		if (finalDeletion) {
			affectedSquares.Add (finalDeletion);
			Object[] sprites2;
			sprites2 = Resources.LoadAll<Sprite> ("GPJ_2D_Platformer_Sprites");
			finalDeletion.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites2 [0];
			Square squaresquaresquare = finalDeletion.GetComponent<Square> ();
			squaresquaresquare.setColour ("");
			squaresquaresquare.setDirectMatches (0);
			squaresquaresquare.setChain (0);
			List<GameObject> squaresquaresquarefreshMatches = new List<GameObject> ();
			squaresquaresquare.clearMatches (squaresquaresquarefreshMatches);
			squaresquaresquare.setChainLinks (squaresquaresquarefreshMatches);
		}

		// I don't think this aspect is working
		foreach (GameObject affectedSquare in affectedSquares) {
			string name = affectedSquare.transform.name;
			string[] textSplit = name.Split (new string[]{ "-" }, System.StringSplitOptions.None);
			int firstNumber = int.Parse (textSplit [1]);
			int secondNumber = int.Parse (textSplit [2]);
			squares [firstNumber] -= 1;
			}
		gridRearrange ();
		}

	/*
	 * 	Post-deletion reorganisation of grid. Includes the 'drop function'
	 */ 
	public void gridRearrange(){
		for(int wrapper = 0; wrapper < 11; wrapper ++){
		for(int newintorig = 0; newintorig < 6; newintorig ++){
			int[] squareValues = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
			for(int newint = 0; newint < 12; newint ++){
				GameObject newSquareOb = GameObject.Find("Grid-"+newintorig+"-"+newint);
				Square newsquaresquare = newSquareOb.GetComponent<Square>();
				if(newSquareOb.GetComponent<SpriteRenderer> ().sprite.name != "Ground" && newSquareOb.GetComponent<SpriteRenderer> ().sprite.name != "GPJ_2D_Platformer_Sprites_0"){
					squareValues [newint] = 1;
				}
			}

			bool hitAZero = false;
			int firstZeroPosition = 0;
			int counter = 0;
			foreach(int squareValue in squareValues){
				if(squareValue == 0 && !hitAZero){
					hitAZero = true;
					firstZeroPosition = counter;
				}
				if(squareValue == 1 && hitAZero){
					//do all of the swapping here!
					GameObject newSquareOb = GameObject.Find("Grid-"+newintorig+"-"+counter);
					Square newsquaresquare = newSquareOb.GetComponent<Square>();

					GameObject newSquareOb2 = GameObject.Find("Grid-"+newintorig+"-"+firstZeroPosition);
					Square newsquaresquare2 = newSquareOb.GetComponent<Square>();

					Sprite placeHolderSprite = newSquareOb.GetComponent<SpriteRenderer> ().sprite;
					Sprite placeHolderSprite2 = newSquareOb2.GetComponent<SpriteRenderer> ().sprite;
					newSquareOb2.GetComponent<SpriteRenderer>().sprite = placeHolderSprite;
					newSquareOb.GetComponent<SpriteRenderer>().sprite = placeHolderSprite2;
					newsquaresquare2.setColour (newSquareOb2.GetComponent<SpriteRenderer> ().sprite.name);
					newsquaresquare.setColour (newSquareOb.GetComponent<SpriteRenderer> ().sprite.name);
					// rerun the function completely
					Debug.Log("Do all the swapping!"+"Grid-"+newintorig+"-"+counter+"   "+"Grid-"+newintorig+"-"+firstZeroPosition);
					break;
				}
				counter ++;
		}
	}
		}
		//reFigureMatches ();
		redoGrid();
	}

    /*
	 *	Re-check the entire grid for matches etc
	 */ 
	public void redoGrid(){
		//step 0. iterate through grid. clear matches, chains and all that shit, only set colour!
		for (int newintorig = 0; newintorig < 6; newintorig++) {
			for (int newint = 0; newint < 12; newint++) {
				GameObject newSquareOb = GameObject.Find ("Grid-" + newintorig + "-" + newint);
				Square newsquaresquare = newSquareOb.GetComponent<Square> ();
				List<GameObject> fresh = new List<GameObject>();
				newsquaresquare.setChainLinks (fresh);
				newsquaresquare.setChain (0);
				newsquaresquare.setDirectMatches (0);
				newsquaresquare.clearMatches (fresh);
				if (newSquareOb.GetComponent<SpriteRenderer> ().sprite.name != "Ground" && newSquareOb.GetComponent<SpriteRenderer> ().sprite.name != "GPJ_2D_Platformer_Sprites_0") {
					newsquaresquare.setColour (newSquareOb.GetComponent<SpriteRenderer> ().sprite.name);
				} else {
					newsquaresquare.setColour ("");
				}
			}
		}
		for (int newintorig = 0; newintorig < 6; newintorig++) {
			for (int newint = 0; newint < 12; newint++) {
				GameObject newSquareOb = GameObject.Find ("Grid-" + newintorig + "-" + newint);
				Square newsquaresquare = newSquareOb.GetComponent<Square> ();
				string  currentSquare = "Grid-" + newintorig + "-" + newint;
				string[] textSplit = currentSquare.Split (new string[]{ "-" }, System.StringSplitOptions.None);
				int firstNumber = int.Parse (textSplit [1]);
				int secondNumber = int.Parse (textSplit [2]);
				for (int i = 0; i < 4; i++) {
					string newString;
					if (i == 0 && (firstNumber - 1) > -1) {
						newString = "Grid-"+(firstNumber-1)+"-"+secondNumber;
						redoCheckMatches (newString, newSquareOb, newsquaresquare);
					} else if (i == 1 && (secondNumber - 1) > -1) {
						newString = "Grid-"+firstNumber+"-"+(secondNumber-1);
						redoCheckMatches (newString, newSquareOb, newsquaresquare);
					} else if (i == 2 && (firstNumber + 1) < 6) {
						newString = "Grid-"+(firstNumber+1)+"-"+secondNumber;
						redoCheckMatches (newString, newSquareOb, newsquaresquare);
					} else if (i == 3 && (secondNumber + 1) < 12) {
						newString = "Grid-"+firstNumber+"-"+(secondNumber+1);
						redoCheckMatches (newString, newSquareOb, newsquaresquare);
					}
				}
				//step 1. get each game object and square
				//step 2. set the colour based on the sprite -> this needs to be done FIRST. Two loops
				//step 3. iterate through adjacent squares. Check for matches

				// step 3.1. if we have a match, increase match count by one and add new square to match list
				// step 3.1.2 also, add self to chain, neighbour to chain and nighbours chain to chain via the masterchain system
				// step 3.2. if our square now has 4 matches, we need to run the deletion event. For now, let's do a log with name
				//step 3.2.1. if our square has 4 chains, we need to run deletion event on the masterchain. let's do a log with name for now

			}
		}
	}

    /*
	 *	Modification of the checkMatches function called when running through the grid rearrange
	 */ 
	public void redoCheckMatches(string newString, GameObject newSquareOb, Square newsquaresquare){
		GameObject newSquareOb2 = GameObject.Find(newString);
		Square squaresquare2 = newSquareOb2.GetComponent<Square>();
		if (newsquaresquare.getColour () != "" && squaresquare2.getColour () != "" && newsquaresquare.getColour () == squaresquare2.getColour ()) {
			newsquaresquare.setDirectmatches (newsquaresquare.getDirectmatches () + 1);
			newsquaresquare.addMatch (newSquareOb);
			squaresquare2.setDirectmatches (squaresquare2.getDirectmatches () + 1);
			squaresquare2.addMatch (newSquareOb2);
			// step 1. Add square 2 to square1s chain
			squaresquare2.addChain (newSquareOb2);
			// step 2. Add square1 to square2s chain
			newsquaresquare.addChain (newSquareOb);
			// step 3. Merge square1 and square2s chain into a master chain
			List<GameObject> masterChain = squaresquare2.getChainList ();
			List<GameObject> masterChain2 = newsquaresquare.getChainList ();
			masterChain.AddRange (masterChain2);
			masterChain = masterChain.Distinct ().ToList ();
			foreach(GameObject masterChainLink in masterChain){
				Square masterChainLinkSquare = masterChainLink.GetComponent<Square>();
				masterChainLinkSquare.setChainLinks (masterChain);
			}



			if (masterChain.Count () >= 4) {
				getDeleting(masterChain, null);
			}

			if(newsquaresquare.getDirectmatches() >= 4){
				getDeleting(newsquaresquare.getMatchList(), newSquareOb);
			}
			if(squaresquare2.getDirectmatches() >= 4){
				getDeleting(squaresquare2.getMatchList(), square1);
			}
		}
	}

	public void gameOver(){
		gameover = true;
		//play sound
		Debug.Log ("Play game over sound");
		//disappear middle section of frame

		GameObject ground = GameObject.Find("Ground");
		Destroy (ground);
		GameObject playerFrameFloor = GameObject.Find("PlayerFrameFloor");
		//Add animation before destory
		Destroy (playerFrameFloor);
		// drop columns, starting with the middle one
		GameObject gridrow0 = GameObject.Find("GridRow0");
		gridrow0.AddComponent<Rigidbody2D>();
		gridrow0.GetComponent<Rigidbody2D> ().gravityScale = 0.1f;




		//GameObject falltarget = GameObject.Find("FallTarget");
		//Transform target = falltarget.transform;
		//float speed = 10;
		//gridrow0.transform.position = Vector3.MoveTowards (gridrow0.transform.position, target.position, speed * Time.deltaTime);
		// have 'Game Over' text move up in screen
		// show 'Game Over' dead beans
	}
		
}