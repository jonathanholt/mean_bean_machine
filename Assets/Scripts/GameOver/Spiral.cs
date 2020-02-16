using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour 
 {
 
     public float RotateSpeed = 7f;
     private float Radius = 8f;
	 public float xOffset = 0f;
     private Vector2 _centre;
     private float _angle;
	 public GameObject targetGameobject;
	 public int moveUp;
	 public float step;
 
     private void Start()
     {
         _centre = targetGameobject.transform.position;
		 moveUp = 1;
		 step = 10f * Time.deltaTime;
	 }
 
     private void Update()
     {
		this.Radius -= 0.04f;
		if(this.Radius >= 0f){
			SpiralMotion();
		}
		else{
			if(moveUp == 1){
				VerticalMotion(8f);
			}
			if(VerticalPositionCheck(8f)){
				step = 6f * Time.deltaTime;
			}
			if(VerticalPositionCheck(8f) || moveUp == 2){
				step = 3f * Time.deltaTime;
				moveUp = 2;
				VerticalMotion(3f);
			}
			if(VerticalPositionCheck(3f) || moveUp == 3){
				moveUp = 3;
				VerticalMotion(5f);
			}
			if(VerticalPositionCheck(5f)){
				moveUp = 2;
			}
		}
	 }
	 
	 public void SpiralMotion(){
		 _angle += RotateSpeed * Time.deltaTime;
		var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
		transform.position = _centre + offset;
	 }
	 
	 public void VerticalMotion(float motionOffset){
		 transform.position = Vector2.MoveTowards(transform.position, new Vector2(_centre.x+ this.xOffset, _centre.y+motionOffset), step);
	 }
	 
	 public bool VerticalPositionCheck(float motionOffset){
		 return transform.position == new Vector3(_centre.x+ this.xOffset, _centre.y+motionOffset, transform.position.z);
	 }
 }
