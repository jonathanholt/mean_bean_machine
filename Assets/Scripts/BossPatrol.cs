using UnityEngine;
using System.Collections;

public class BossPatrol : MonoBehaviour {

	public float moveSpeed;
	public bool moveRight;
	public float moveY;

	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;

	private bool notAtEdge;
	public Transform edgeCheck;
	private float ySize;

	// Use this for initialization
	void Start () {
		ySize = transform.localScale.y;
	}

	// Update is called once per frame
	void Update () {

		hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

		notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall || !notAtEdge) {
			moveRight = !moveRight;
		}

		moveY = GetComponent<Rigidbody2D> ().velocity.y;
		if (moveRight) {
			transform.localScale = new Vector3 (-ySize, transform.localScale.y, transform.localScale.z);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, 0);
		} else {
			transform.localScale = new Vector3 (ySize, transform.localScale.y, transform.localScale.z);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, 0);
		}

	}
}
