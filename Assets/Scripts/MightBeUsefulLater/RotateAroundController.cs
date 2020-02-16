using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralOrbitController : MonoBehaviour
{
    private Vector3 target;
	public GameObject targetObject;

    void Start()
	{
		this.target = gameObject.transform.position;
	}
	
    void Update()
    {
        transform.RotateAround(this.target, Vector3.up, 30 * Time.deltaTime);
    }
}
