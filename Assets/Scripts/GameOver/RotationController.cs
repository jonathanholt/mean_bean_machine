using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
	
	public int targetDegree;
	public float speed;
	private int originalTargetDegree;
	
	void Start () {
		this.originalTargetDegree = targetDegree;
	}

    void Update()
    {
		if(targetDegree < 180 && (int)transform.rotation.eulerAngles.z != targetDegree){
			transform.Rotate(Vector3.forward * + speed);
		}
		else if(targetDegree > 180 && (int)transform.rotation.eulerAngles.z != targetDegree){
			transform.Rotate(Vector3.forward * - speed);
		}
		else{
			if(targetDegree == this.originalTargetDegree){
				targetDegree = 360 - targetDegree;
			}
			else{
				targetDegree = this.originalTargetDegree;
			} 
		}
    }
}
