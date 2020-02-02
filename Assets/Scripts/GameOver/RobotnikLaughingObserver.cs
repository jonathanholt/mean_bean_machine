using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotnikLaughingObserver : MonoBehaviour
{
	
	public bool countdownReachedOne;
	public GameObject robotnik;
	Animator anim;
	
    void Start()
    {
        countdownReachedOne = false;
		anim = robotnik.GetComponent<Animator>();
    }

    
    void Update()
    {
		if(countdownReachedOne){
			anim.SetTrigger("RobotnikLaughing");
			countdownReachedOne = false;
		}
	}
}
