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
	public int squaresOccupied;


	public string[] gamecolours = new string[]{"blue","green","purple","red","yellow"};
	//5 == yellow
	//4 == red
	//3 == purple
	//2 == green
	//1 == blue


	/*
	 * 	Called at the start of the level once. Sets some basic variable values
	 */ 
	void Start () {
		squaresOccupied = 0;
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

		if (Input.GetKeyUp ("z")) {
			PauseController.togglePause ();
		}


		if (!gameover && Time.timeScale != 0f) {
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

	/*in
	 * 	Detect Player collision with the bottom of the grid and call functions
	 */ 
	public void OnCollisionEnter2D(Collision2D other){
		squaresOccupied += 2;
		objectCollidedWith = other.collider.gameObject.name;
		if (objectCollidedWith != "Ground") {
			string[] textSplit = objectCollidedWith.Split (new string[]{ "-" }, System.StringSplitOptions.None);
			int firstNumber = int.Parse (textSplit [1]);
			int secondNumber = int.Parse (textSplit [2]);
			if (isMoveAllowed (firstNumber, secondNumber)) {
				updateGrid ();
				reinitGame ();
			} else {
				Debug.Log ("else");
				transform.position = oldPosition;
				if (lastPressed == "r") {
					row--;
				}
				if (lastPressed == "l") {
					row++;
				}
			}
		} else {
			if (squaresOccupied >= 29) {
				EnemyController.changeAnimationWinning ();
				EnemyLowerController.changeAnimationWinning ();
			}
			Debug.Log ("nope");
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
		string[] colours = new string[] {"NULLVOID", "B", "GR", "PUR", "REDD", "YELLO"};
		interactWithGridManager(square1FindString, square2FindString,  colours[NewBean.randomBean1], colours[NewBean.randomBean2]);
	}

	public void interactWithGridManager(string squareToDo, string squareToDo2, string randomBeancolour, string randomBeanColour2){
		GridManager.changeRobotnikColour (squareToDo, randomBeancolour);
		GridManager.changeRobotnikColour (squareToDo2, randomBeanColour2);
		GridManager.checkRobotnikMatches (squareToDo, 1);
		GridManager.checkRobotnikMatches (squareToDo2, 100);
		if ((GridManager.deletei1 != 100 && GridManager.deletej1 != 100) || (GridManager.deletei2 != 100 && GridManager.deletej2 != 100)) {
			GridManager.deleteRobotnikMatches ();
			for (int n = 0; n < 50; n++) {
				GridManager.dropFunction ();
			}
			GridManager.finalCheckFunction ();
		}
		//iterate over returnGrid


		if (GridManager.needToRedoGrid) {
			Robotnik[,] returnedGrid = GridManager.returnGrid ();
			for (int j = 0; j < GridManager.rows; j++) {
				for (int i = 0; i < GridManager.columns; i++) {
					string nameOfSquare = returnedGrid [j, i].name;
					GameObject squareToFind = GameObject.Find (nameOfSquare);
					if (returnedGrid [j, i].colour == "NULLVOID") {
						restoreSquareToBlank (squareToFind);
					} else {
						addColorToSquare (squareToFind, returnedGrid[j, i].colour);
					}
				}
			}
		}



	}

	public void restoreSquareToBlank(GameObject squareToAlter){
		Destroy(squareToAlter.GetComponent<BoxCollider2D> ());		
		Object [] sprites;
		sprites = Resources.LoadAll<Sprite> ("GPJ_2D_Platformer_Sprites");

		squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [0];
	}

	public void addColorToSquare(GameObject squareToAlter, string colour){
		/*
		 * ONLY ADD COLLIDER IF WE DON;T HAVE ONE */
		if (squareToAlter.GetComponent<BoxCollider2D> () == null) {
			squareToAlter.AddComponent<BoxCollider2D> ();
		}

		//5 == yellow
		//4 == red
		//3 == purple
		//2 == green
		//1 == blue
		Object [] sprites;
		sprites = Resources.LoadAll<Sprite> ("beans");
		switch(colour)
		{
		case "B":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [0];
			Debug.Log ("BLUE");
			break;
		case "GR":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [1];
			Debug.Log ("GREEN");
			break;
		case "PUR":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [2];
			Debug.Log ("PURPLE");
			break;
		case "REDD":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [3];
			Debug.Log ("RED");
			break;
		case "YELLO":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [4];
			Debug.Log ("YELLOW");
			break;
		default:
			Debug.Log ("ERROR");
			break;
		}

	}



	public void addColourToSquare(GameObject squareToAlter){
	}

	public void gameOver(){
		EnemyController.changeAnimationWinning ();
		EnemyLowerController.changeAnimationWinning ();
		GameObject beanDuo = GameObject.Find("BeanDuo");
		GameObject beanHolders = GameObject.Find("BeanHolders");
		Destroy (beanDuo);
		Destroy (beanHolders);
		gameover = true;
		//play sound
		//disappear middle section of frame
		GameObject ground = GameObject.Find("Ground");
		Destroy (ground);
		GameObject playerFrameFloor = GameObject.Find("PlayerFrameFloor");
		//Add animation before destory
		Destroy (playerFrameFloor);
		StartCoroutine(MyFunction(0.1f));
	}

	IEnumerator MyFunction(float delayTime){
		int[] columns = new int[] {2, 3, 1, 4, 0, 5};
		for (int i = 0; i < 6; i++) {
			yield return new WaitForSeconds (delayTime);
			string column = "GridColumn" + columns[i];
			GameObject gridcolumn = GameObject.Find(column);
			gridcolumn.AddComponent<Rigidbody2D>();
			gridcolumn.GetComponent<Rigidbody2D> ().gravityScale = 0.1f;	
		}
	}
		
}