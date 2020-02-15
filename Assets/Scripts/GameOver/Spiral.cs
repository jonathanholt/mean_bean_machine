using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour 
 {
 
     public float RotateSpeed = 7f;
     private float Radius = 6f;
 
     private Vector2 _centre;
     private float _angle;
	 public GameObject targetGameobject;
 
     private void Start()
     {
         _centre = targetGameobject.transform.position;
     }
 
     private void Update()
     {
		this.Radius -= 0.04f;
		if(this.Radius >= 0f){
			_angle += RotateSpeed * Time.deltaTime;
 
			var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
			transform.position = _centre + offset;
		}
	 }
  
  
 
 }
