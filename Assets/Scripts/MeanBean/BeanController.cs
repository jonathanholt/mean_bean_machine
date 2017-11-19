using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanController : MonoBehaviour {

	public GameObject beanContainer;
	public GameObject levelEdge;
	public float speed;

	// Use this for initialization
	void Start () {
		beanContainer = GameObject.Find("BeanDuo");
		//Debug.Log (beanContainer.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		beanContainer.transform.position = Vector3.MoveTowards (beanContainer.transform.position, levelEdge.transform.position, step);
	}
}
