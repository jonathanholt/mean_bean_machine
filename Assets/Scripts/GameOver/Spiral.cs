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
	 public float newStep;
 
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
				step = newStep * Time.deltaTime;
			}
			if(VerticalPositionCheck(8f) || moveUp == 2){
				if(step != newStep * Time.deltaTime){
					step = newStep * Time.deltaTime;	
				}
				else{
					step = 4f * Time.deltaTime;
				}
				moveUp = 2;
				VerticalMotion(3f);
			}
			if(VerticalPositionCheck(3f) || moveUp == 3){
				step = 4f * Time.deltaTime;
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
