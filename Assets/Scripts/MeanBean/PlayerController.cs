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
		if(beanOrientationPositive == 1){
			squares[row-1] += 1;
			squares[row-2] += 1;
			square1FindString = "Grid-" + (row - 1) + "-" + (squares [row - 1] - 1);
				square2FindString = "Grid-"+(row-2)+"-"+(squares[row-1] - 1);
		}
		else if(beanOrientationPositive == 2){
			squares[row-1] += 2;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
				square2FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 2);
		}
		else if (beanOrientationPositive == 3){
			squares[row-1] += 1;
			squares[row] += 1;
			square1FindString = "Grid-"+(row-1)+"-"+(squares[row-1] - 1);
				square2FindString = "Grid-"+(row)+"-"+(squares[row-1] - 1);
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

		findMatches (square1FindString, square2FindString);
	}

	public void findMatches(string bean1InGrid, string bean2InGrid){
		GameObject squareAbove;
		//SECOND DIGIT PLUS ONE

		GameObject squareLeft;
		// FIRST DIGIT PLUS ONE

		GameObject squareRight;
		// FIRST DIGIT MINUS ONE

		GameObject squareBelow;
		//SECOND DIGIT MINUS ONE
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
}
