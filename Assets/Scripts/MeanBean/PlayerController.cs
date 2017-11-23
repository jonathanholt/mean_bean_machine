using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveVelocity;
	public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool doubleJumped;
	private Animator anim;
	public Transform firePoint;
	public GameObject ninjaStar;

	private Rigidbody2D myrigidbody2D;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	public float gravityStore;



	public bool canMove;
	private bool collidedTrue;



	//Mean Bean Variables
	private GameObject startPoint;
	private int row;
	private int[] squares = new int[] {0, 0, 0, 0, 0, 0};
	public GameObject square1;
	public GameObject square2;
	public int beanOrientation;
	public string[] positions = new string[] {"beanHolderUp", "beanHolderLeft", "beanHolderDown", "beanHolderRight"};


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		collidedTrue = false;
		startPoint = GameObject.Find("StartPoint");
		row = 3;
		beanOrientation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("down")) {
		
		}
		if (Input.GetKeyUp ("left") && row > 1) {
			transform.position += Vector3.left * 0.53f;
			row--;
		}
		if (Input.GetKeyUp ("right") && row < 6) {
			transform.position += Vector3.right * 0.53f;
			row++;
		}
		if (Input.GetKeyUp ("a")) {
			shiftAntiClockwise ();		
		}
		if (Input.GetKeyUp ("s")) {
			shiftClockwise ();		
		}
















		if (!canMove) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			//Debug.Log ("state 1");
			return;
		}

		anim.SetBool("Grounded", grounded);

		if (knockbackCount <= 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			if (knockFromRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockback, knockback);
			}
			if (!knockFromRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockback, knockback);
			}
			knockbackCount -= Time.deltaTime;
		}

		anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

	public void OnCollisionEnter2D(Collision2D other){
		updateGrid ();
		transform.position = startPoint.transform.position;
		NewBean.getNewBean ();
		row = 3;
	}

	public void updateGrid(){

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

			//Debug.Log ("1. "+squares[row-1]+ " & 2. " +squares[row]);
		}
		else{
			//beanOrientationPositive should == 0
			squares[row-1] += 2;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 2);
			square2FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
		}

		square1 = GameObject.Find(square1FindString);
		square2 = GameObject.Find(square2FindString);
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

		//updateSquares(square1FindString, NewBean.randomBean1);
	}


	public void shiftAntiClockwise(){
		beanOrientation -= 1;
		beanOrientation = beanOrientation % 4;
		GameObject beanToMove = GameObject.Find("bean2");
		GameObject moveBeanTo = new GameObject ();
		moveBeanTo = GameObject.Find(positions[Mathf.Abs(beanOrientation)]);
		beanToMove.transform.position = moveBeanTo.transform.position;
	}

	public void shiftClockwise(){
		beanOrientation += 1;
		beanOrientation = beanOrientation % 4;
		GameObject beanToMove = GameObject.Find("bean2");
		GameObject moveBeanTo = new GameObject ();
		moveBeanTo = GameObject.Find(positions[Mathf.Abs(beanOrientation)]);
		beanToMove.transform.position = moveBeanTo.transform.position;
	}

	public void updateSquares(string squareString, int colour){
		GameObject square = GameObject.Find(squareString);
		Square squaresquare = (Square)square.GetComponent (typeof(Square));
		squaresquare.setColour ("test");
		//load neighbour 1
		GameObject neighbour1 = GameObject.Find(squarestringAlteration(squareString, 1));
		//load neighbour 2
		GameObject neighbour2 = GameObject.Find(squarestringAlteration(squareString, 2));
		//load neighbour 3
		GameObject neighbour3 = GameObject.Find(squarestringAlteration(squareString, 3));
		//load neighbour 4
		GameObject neighbour4 = GameObject.Find(squarestringAlteration(squareString, 4));
	}

	public string squarestringAlteration(string original, int neighbour){
		string[] textSplit = original.Split(new string[]{"-"}, System.StringSplitOptions.None);
		int firstNumber = int.Parse (textSplit [1]);
		int secondNumber = int.Parse (textSplit [2]);
		if (neighbour == 1) {
		}
		else if(neighbour == 2){
			
		}
			else if(neighbour == 3){
				
			}
			else if(neighbour == 4){
				
			}
		return "in progress";
	}

	//Get the Square script for the square object
	//Update the colour of the square
	//Load the square to the left, check the colour...if we have a match, update the chain and directmatches appropriately
	//updating the chain means check the neighbours chain value, adding that to your own and updating your chain and all the other values
	//in the chain by one. 
}
