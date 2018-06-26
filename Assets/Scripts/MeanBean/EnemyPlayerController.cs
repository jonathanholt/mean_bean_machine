using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyPlayerController : MonoBehaviour {

	//Mean Bean Variables
	private GameObject startPoint;
	private GameObject alternateStartPoint1;
	private GameObject alternateStartPoint2;
	private GameObject alternateStartPoint3;
	private GameObject alternateStartPoint4;
	private GameObject alternateStartPoint5;
	public int row;
	private int[] squares = new int[] {0, 0, 0, 0, 0, 0};
	public GameObject square1;
	public GameObject square2;
	public int beanOrientation;
	public string[] enemyPositions = new string[] {"enemyBeanHolderUp", "enemyBeanHolderLeft", "enemyBeanHolderDown", "enemyBeanHolderRight"};
	public List<GameObject> matches;
	public string objectCollidedWith;
	public string lastPressed;
	public Vector3 oldPosition;
	public Rigidbody2D rigid2D;
	public int runCount;
	public bool gameover;
	public int enemySquaresOccupied;
	public int randomNuisance;


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
		enemySquaresOccupied = 0;
		startPoint = GameObject.Find("EnemyStartPoint");
		alternateStartPoint1 = GameObject.Find("StartPoint1");
		alternateStartPoint2 = GameObject.Find("StartPoint2");
		alternateStartPoint3 = GameObject.Find("StartPoint3");
		alternateStartPoint4 = GameObject.Find("StartPoint4");
		alternateStartPoint5 = GameObject.Find("StartPoint5");

		row = 3;
		beanOrientation = 0;
		rigid2D = GetComponent<Rigidbody2D>();
		rigid2D.velocity = new Vector3(0, -2, 0);
		GameObject rightBeanHolder = GameObject.Find("enemyBeanHolderRight");
		GameObject leftBeanHolder = GameObject.Find("enemyBeanHolderLeft");
		rightBeanHolder.GetComponent<Collider2D>().enabled = false;
		leftBeanHolder.GetComponent<Collider2D>().enabled = false;
	}
	
	/*
	 *	Update is called once per frame. Used to detect key up events
	 */ 
	void Update () {
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
		GameObject beanToMove = GameObject.Find("enemybean2");
		beanToMove.transform.position = GameObject.Find(enemyPositions[Mathf.Abs(beanOrientation)]).transform.position;
		Debug.Log ("LISTEN HERE...");
		Debug.Log (enemyPositions[Mathf.Abs(beanOrientation)]);
		if (beanOrientation == 3) {
			GameObject rightBeanHolder = GameObject.Find("enemyBeanHolderRight");
			GameObject leftBeanHolder = GameObject.Find("enemyBeanHolderLeft");
			rightBeanHolder.GetComponent<Collider2D>().enabled = true;
			leftBeanHolder.GetComponent<Collider2D> ().enabled = false;
		}
		else if(beanOrientation == 1){
			GameObject rightBeanHolder = GameObject.Find("enemyBeanHolderRight");
			GameObject leftBeanHolder = GameObject.Find("enemyBeanHolderLeft");
			leftBeanHolder.GetComponent<Collider2D>().enabled = true;
			rightBeanHolder.GetComponent<Collider2D>().enabled = false;
		}
		else{
			GameObject rightBeanHolder = GameObject.Find("enemyBeanHolderRight");
			GameObject leftBeanHolder = GameObject.Find("enemyBeanHolderLeft");
			rightBeanHolder.GetComponent<Collider2D>().enabled = false;
			leftBeanHolder.GetComponent<Collider2D>().enabled = false;
		}
	}

	/*in
	 * 	Detect Player collision with the bottom of the grid and call functions
	 */ 
	public void OnCollisionEnter2D(Collision2D other){
		Debug.Log("Collision!");
		if (NuisanceController.nuisanceState != 100) {
			Debug.Log("Enemy collision in here");
			enemySquaresOccupied += 2;
			objectCollidedWith = other.collider.gameObject.name;
			Debug.Log(objectCollidedWith);
			if (objectCollidedWith != "EnemyGround") {
				Debug.Log ("Enemy hit ground");
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
				Debug.Log ("Enemy didn't hit ground");
				if (enemySquaresOccupied >= 29) {
					EnemyController.changeAnimationLosing ();
				}
				updateGrid ();
				reinitGame ();
			}
		} else {
			Debug.Log ("NUISANCE NUISANCE NUISANCE");
			enemySquaresOccupied += 1;
			nuisanceUpdateGrid ();
			reinitGame ();
		}
	}

    /*
	 *	Check if moving bean into a certain square is allowed
	 */ 
	public bool isMoveAllowed(int firstNumber, int secondNumber){
		if (((secondNumber + 1) < squares [firstNumber])) {
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
			Debug.Log ("Not gameover");
			if (NuisanceController.nuisanceState > 0 && NuisanceController.nuisanceState != 100) {
				Debug.Log ("Nuisance stuff");
				/**
				randomNuisance = Random.Range (0, 6);


				switch (randomNuisance)
				{
				case 1:
					transform.position = alternateStartPoint1.transform.position;
					break;
				case 2:
					transform.position = alternateStartPoint2.transform.position;
					break;
				case 3:
					transform.position = alternateStartPoint3.transform.position;
					break;
				case 4:
					transform.position = alternateStartPoint4.transform.position;
					break;
				case 5:
					transform.position = alternateStartPoint5.transform.position;
					break;
				default:
					transform.position = startPoint.transform.position;
					break;
				}
				*/

				//NuisanceController.createNewNuisancePair ();
				rigid2D.velocity = new Vector3 (0, -2, 0);
				beanOrientation = 0;
				rotate ();
				//NuisanceController.nuisanceState = 100;
			} else {
				//NuisanceController.nuisanceState = 0;
				transform.position = startPoint.transform.position;
				EnemyNewBean.createNewBeanPair ();
				row = 3;
				rigid2D.velocity = new Vector3 (0, -2, 0);
				beanOrientation = 0;
				int movement = 1;
				if (EnemyNewBean.currentInstruction.column < 3) {
					movement = 0;
				} 
				for (int i = 0; i < 4; i++) {
					if (beanOrientation != EnemyNewBean.currentInstruction.rotation) {
						rotateMoveB();
					}
					if (row > 2 && movement == 0) {
						leftMove ();
						Debug.Log ("Row "+row);
					}
					else if(row < 6 && movement == 1){
						rightMove ();
						Debug.Log ("Row "+row);
					}
				}
				//rotate ();
			}
		}
	}

    /*
	 *	Entry point for all of the grid update classes and methods
	 */ 
	public void updateGrid(){
		getSquaresToUpdate ();
	}

	public void nuisanceUpdateGrid(){
		//getSquaresToUpdateNuisance ();
	}

	public void getSquaresToUpdateNuisance(){
		string square1FindString = null;

		switch (randomNuisance)
		{
		case 1:
			squares[2] += 1;
			square1FindString = "EnemyGrid-"+(2)+"-"+((squares[2])-1);
			break;
		case 2:
			squares[3] += 1;
			square1FindString = "EnemyGrid-"+(3)+"-"+((squares[3])-1);
			break;
		case 3:
			squares[4] += 1;
			square1FindString = "EnemyGrid-"+(4)+"-"+((squares[4])-1);
			break;
		case 4:
			squares[1] += 1;
			square1FindString = "EnemyGrid-"+(1)+"-"+((squares[1])-1);
			break;
		case 5:
			squares[0] += 1;
			square1FindString = "EnemyGrid-"+(0)+"-"+((squares[0])-1);
			break;
		default:
			squares[2] += 1;
			square1FindString = "EnemyGrid-"+(2)+"-"+((squares[2])-1);
			break;
		}

		square1 = GameObject.Find (square1FindString);
		Object [] sprites;
		Debug.Log ("Important "+square1FindString);
		sprites = Resources.LoadAll<Sprite> ("nuisance");
		square1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [1];
		if (square1.GetComponent<BoxCollider2D> () == null) {
			square1.AddComponent<BoxCollider2D> ();
			float width = GetComponent<SpriteRenderer> ().bounds.size.x;
			float height = GetComponent<SpriteRenderer> ().bounds.size.y;
			square1.GetComponent<BoxCollider2D> ().size = new Vector3(height, width, width);
		}
		square1.tag = "EnemyGround";
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
			int rowplus = 0;
			if (row == 6) {
				Debug.Log ("Row change..."+row);
				rowplus = 1;
			}
			if (objectCollidedWith != "EnemyGround") {
				if (row == 2) {
					rowplus = 1;
				}
				string[] textSplit = objectCollidedWith.Split (new string[]{ "-" }, System.StringSplitOptions.None);
				int firstNumber = int.Parse (textSplit [1]);
				int secondNumber = int.Parse (textSplit [2]);
				Debug.Log ("Error may have been thrown?");
				square1FindString = "EnemyGrid-" + (row - rowplus) + "-" + ((squares [row - rowplus]));
				square2FindString = "EnemyGrid-" + (row - 1 - rowplus) + "-" + ((squares [row - 1 - rowplus]));
			}
			Debug.Log ("Error may have been thrown");
			Debug.Log ("Row = "+ row);


				squares [row - rowplus] += 1;
				squares [row - 1 - rowplus] += 1;


			if (squares [row - rowplus] > squares [row - 1 - rowplus]) {
				square2DropNumber = squares [row - rowplus] - squares [row - 1 - rowplus];
			}
			if (squares [row - rowplus] < squares [row - 1 - rowplus]) {
				square1DropNumber = squares [row - 1 - rowplus] - squares [row - rowplus];
			}
			if (square1FindString == null) {
				Debug.Log ("setting find string 1");
				if ((row - rowplus) < 0) {
					Debug.Log ("Resetting rowplus ");
					rowplus = 0;
				}
				if (row == 2) {
					rowplus = 1;
				}
				Debug.Log ("Useful debugging string");
				square1FindString = "EnemyGrid-" + (row - rowplus) + "-" + ((squares [row - 1 - rowplus] - 1) - square1DropNumber);
				Debug.Log ("LOOK! "+square1FindString);
			}
			if (square2FindString == null) {
				square2FindString = "EnemyGrid-" + (row - 1 - rowplus) + "-" + ((squares [row - 1 - rowplus] - 1) - square2DropNumber);
				Debug.Log ("LOOK AGAIN! "+square2FindString);
			}
		}
		else if(beanOrientationPositive == 2){
			Debug.Log ("2");
			Debug.Log ("Is this out of index??"+(row-1));
			squares[row-1] += 2;
			Debug.Log ("setting find string 2");
			square1FindString = "EnemyGrid-"+(row-1)+"-"+(squares[row-1] - 1);
				square2FindString = "EnemyGrid-"+(row-1)+"-"+(squares[row-1] - 2);
		}
		else if (beanOrientationPositive == 3){
			Debug.Log ("3");
			Debug.Log ("Is this out of index??"+(row-1));
			//if the row from the number above doesn't equal the equation below, we need to do something different
			//so we need to break down this string
			square1FindString = null;
			square2FindString = null;
			if (objectCollidedWith != "EnemyGround") {
				string[] textSplit = objectCollidedWith.Split (new string[]{ "-" }, System.StringSplitOptions.None);
				int firstNumber = int.Parse (textSplit [1]);
				int secondNumber = int.Parse (textSplit [2]);
					if (firstNumber != row - 1) {
						square2FindString = "EnemyGrid-"+(row)+"-"+((squares[row]));
					Debug.Log ("setting find string 3");
						square1FindString = "EnemyGrid-" + (row - 1)+ "-"+((squares[row-1]));
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
				Debug.Log ("setting find string 4");
			square1FindString = "EnemyGrid-" + (row - 1);
			square1FindString = square1FindString + "-";
			square1FindString = square1FindString + ((squares[row-1] - 1) - square1DropNumber);
			}
			if(square2FindString == null){
				square2FindString = "EnemyGrid-"+(row)+"-"+((squares[row-1] - 1) - square2DropNumber);
			}

		}
		else{
			Debug.Log ("Testing 4");
			Debug.Log ("Is this out of index??"+(row-1));
			squares[row-1] += 2;
			Debug.Log ("setting find string 5");
			square1FindString = "EnemyGrid-"+(row-1)+"-"+(squares[row-1] - 2);
			square2FindString = "EnemyGrid-"+(row-1)+"-"+(squares[row-1] - 1);
		}
			Debug.Log (square1FindString);
			Debug.Log (square2FindString);
			square1 = GameObject.Find (square1FindString);
			square2 = GameObject.Find (square2FindString);
			updateSquareProperties (square2, square1, square1FindString, square2FindString);
	}

	/*
	 *	Pass in two squares and add a BoxCollider
	 */ 
	public void updateSquareProperties(GameObject square1, GameObject square2, string square1FindString, string square2FindString){
		Object [] sprites;
		sprites = Resources.LoadAll ("beans");
		if (EnemyNewBean.round == 1) {
			square1.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites [EnemyNewBean.randomBean2];
			square2.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites [EnemyNewBean.randomBean1];
		} else {
			square1.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites [EnemyNewBean.randomBean1];
			square2.GetComponent<SpriteRenderer> ().sprite = (Sprite)sprites [EnemyNewBean.randomBean2];
		}
			if (square1.GetComponent<BoxCollider2D> () == null) {
			//Debug.Log ("NULL MET");
				square1.AddComponent<BoxCollider2D> ();
				float width = GetComponent<SpriteRenderer> ().bounds.size.x;
				float height = GetComponent<SpriteRenderer> ().bounds.size.y;
				square1.GetComponent<BoxCollider2D> ().size = new Vector3(height, width, width);
		}
		if (square2.GetComponent<BoxCollider2D> () == null) {
			Debug.Log ("ESTABLISHING BOX COLLIDER 2");
			square2.AddComponent<BoxCollider2D> ();
			float width = GetComponent<SpriteRenderer> ().bounds.size.x;
			float height = GetComponent<SpriteRenderer> ().bounds.size.y;
			square2.GetComponent<BoxCollider2D> ().size = new Vector3(height, width, width);
		}
		square1.tag = "EnemyGround";
		square2.tag = "EnemyGround";
		string[] colours = new string[] {"NULLVOID", "B", "GR", "PUR", "REDD", "YELLO"};
		interactWithGridManager(square1FindString, square2FindString,  colours[EnemyNewBean.randomBean2], colours[EnemyNewBean.randomBean1]);
	}

	public void interactWithGridManager(string squareToDo, string squareToDo2, string randomBeancolour, string randomBeanColour2){
		GridManager.changeRobotnikColour (squareToDo, randomBeanColour2);
		GridManager.changeRobotnikColour (squareToDo2, randomBeancolour);
		GridManager.checkRobotnikMatches (squareToDo, 1);
		GridManager.checkRobotnikMatches (squareToDo2, 100);
		bool goneDeleting = false;
		if ((GridManager.deletei1 != 100 && GridManager.deletej1 != 100) || (GridManager.deletei2 != 100 && GridManager.deletej2 != 100)) {
			GridManager.deleteRobotnikMatches ();
			goneDeleting = true;
			for (int n = 0; n < 50; n++) {
				GridManager.dropFunction ();
			}
			GridManager.finalCheckFunction ();
		}
		//iterate over returnGrid


		if (GridManager.needToRedoGrid) {
			squares = new int[] {0, 0, 0, 0, 0, 0};
			Robotnik[,] returnedGrid = GridManager.returnGrid ();
			for (int j = 0; j < GridManager.rows; j++) {
				for (int i = 0; i < GridManager.columns; i++) {
					string nameOfSquare = returnedGrid [j, i].name;
					GameObject squareToFind = GameObject.Find (nameOfSquare);
					if (returnedGrid [j, i].colour == "NULLVOID") {
						restoreSquareToBlank (squareToFind);
					} else {
						squares [j] += 1;
						addColorToSquare (squareToFind, returnedGrid[j, i].colour);
					}
				}
			}
			foreach(int testsquare in squares){
			}
		}
		if (goneDeleting) {
			NuisanceController.nuisanceState++;
			//NuisanceController.initNuisance ();
		}
		if ((squareToDo.Contains ("12") || squareToDo.Contains ("13")) || (squareToDo2.Contains ("12")) || squareToDo2.Contains ("13")) {
			gameOver ();
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
			break;
		case "GR":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [1];
			break;
		case "PUR":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [2];
			break;
		case "REDD":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [3];
			break;
		case "YELLO":
			squareToAlter.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [4];
			break;
		default:
			break;
		}

	}



	public void addColourToSquare(GameObject squareToAlter){
	}

	public void gameOver(){
		EnemyController.changeAnimationLost ();
		GameObject beanDuo = GameObject.Find("BeanDuo");
		GameObject player = GameObject.Find("Player");
		GameObject enemybeanDuo = GameObject.Find("EnemyBeanDuo");
		GameObject enemybeanHolders = GameObject.Find("EnemyBeanHolders");
		Destroy (beanDuo);
		Destroy (player);
		Destroy (enemybeanDuo);
		Destroy (enemybeanHolders);
		gameover = true;
		//play sound
		//disappear middle section of frame
		GameObject ground = GameObject.Find("EnemyGround");
		Destroy (ground);
		GameObject enemyFrameFloor = GameObject.Find("EnemyFrameFloor");
		//Add animation before destory
		Destroy (enemyFrameFloor);
		StartCoroutine(MyFunction(0.1f));
	}

	IEnumerator MyFunction(float delayTime){
		int[] columns = new int[] {2, 3, 1, 4, 0, 5};
		for (int i = 0; i < 6; i++) {
			yield return new WaitForSeconds (delayTime);
			string column = "EnemyGridColumn" + columns[i];
			GameObject gridcolumn = GameObject.Find(column);
			gridcolumn.AddComponent<Rigidbody2D>();
			gridcolumn.GetComponent<Rigidbody2D> ().gravityScale = 0.1f;	
		}
	}
		
}