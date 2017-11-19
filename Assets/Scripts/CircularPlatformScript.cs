using UnityEngine;
using System.Collections;
/*
    Novithian
    04/25/2016
*/
public class CircularPlatformScript : MonoBehaviour {
    public float radius = 2f;      //Distance from the center of the circle to the edge
    public float currentAngle= 0f; //Our angle, this public for debugging purposes
    private float speed = 0f;      //Rate at which we'll move around the circumference of the circle
    public float timeToCompleteCircle = 1.5f; //Time it takes to complete a full circle
	public GameObject platform;
	public bool clockwise;
 
    // Use this for initialization
    void Start () {
    }
 
    void Awake(){
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
    }
 
    // Update is called once per frame
    void Update () {
        speed = (Mathf.PI * 2) / timeToCompleteCircle; //For level design purposes
		if(clockwise)
			currentAngle -= Time.deltaTime * speed; //Changes the angle
		else
			currentAngle += Time.deltaTime * speed; //Changes the angle
		float newX = platform.transform.position.x + radius * Mathf.Cos (currentAngle) / 10;
		float newY = platform.transform.position.y + radius * Mathf.Sin (currentAngle) / 10;
        platform.transform.position = new Vector3 (newX, newY, platform.transform.position.z);
    }
}