using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickerTapeController : MonoBehaviour
{
    private float speed = 5.0f;
    private Vector3 target;
    private Vector3 position;
	private Vector3 originalPosition;
	public GameObject gameObjectInOriginalPosition;

    void Start()
    {
        target = new Vector3(-20.0f, 2.5f, 0f);
        position = gameObject.transform.position;
		originalPosition = gameObjectInOriginalPosition.transform.position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
		if(transform.position == target){
			transform.position = originalPosition;
		}
    }
}
