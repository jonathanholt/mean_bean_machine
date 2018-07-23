using UnityEngine;
using System.Collections;

public class ScrollingMenuBackground : MonoBehaviour 
{

	private BoxCollider2D groundCollider;       //This stores a reference to the collider attached to the Ground.
	private float groundHorizontalLength;       //A float to store the x-axis length of the collider2D attached to the Ground GameObject.
	private Vector2 originalPoint;
	public float speed;

	private void Start(){
		originalPoint = transform.position;
	}

	//Update runs once per frame
	private void Update()
	{
		if (transform.position.x < 0) {
			RepositionBackground ();
		} else {
			transform.position = (Vector2) originalPoint;
		}
	}

	//Moves the object this script is attached to right in order to create our looping background effect.
	private void RepositionBackground()
	{
		//This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
		Vector2 groundOffSet = new Vector2(speed, 0);

		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		transform.position = (Vector2) transform.position + groundOffSet;
	}
}