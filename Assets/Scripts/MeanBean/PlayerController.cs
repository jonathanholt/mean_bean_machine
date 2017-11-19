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
		squares[row-1] += 2;
		int square1RowtoAdd = 0;
		int square2RowtoAdd = 0;
		int square2ColumntoAdd = 0;
		//0 is normal
		//-1 is normal is square 2 on the left of square 1
		//-2 is square 1 is above square2
		// -3 is normal is square 2 on the right of square 1
		if (beanOrientation == -2) {
			square1RowtoAdd = +1;
			square2RowtoAdd = -1;
		} else if (beanOrientation == -1) {
			square2ColumntoAdd = -1;
			square2RowtoAdd = -1;
			if(((squares[row-1] - 2)+square1RowtoAdd) != 0){
				square1RowtoAdd = -1;
				square2RowtoAdd -= 1;
			}
		} else if (beanOrientation == -3) {
			square2ColumntoAdd = +1;
			square2RowtoAdd = -1;
			if(((squares[row-1] - 2)+square1RowtoAdd) != 0){
				square1RowtoAdd = -1;
				square2RowtoAdd -= 1;
			}
		}
		square1 = GameObject.Find("Grid-"+(row-1)+"-"+((squares[row-1] - 2)+square1RowtoAdd));
		square2 = GameObject.Find("Grid-"+((row-1)+square2ColumntoAdd)+"-"+((squares[row-1] - 1)+square2RowtoAdd));
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
