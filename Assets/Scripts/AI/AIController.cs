using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIController : MonoBehaviour
{
   public UnityEvent moveAILeftEvent;
   public UnityEvent moveAIRightEvent;
   public UnityEvent rotateAIClockwiseEvent;
   public UnityEvent rotateAIAntiClockwiseEvent;
   
	public void randomMoves(){
		var number = Random.Range(0,5);
		if(number > 0){
			for (int i = 0; i < number; i++) 
			{
				StartCoroutine (MovePause (0.5f));
			}
		}
	}
	
	IEnumerator MovePause(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		generateMove();
	}
	
	public void generateMove(){
		var moveOrRotate = Random.Range(0,2);
		if(moveOrRotate > 0){
			var clockwiseOrAnticlockwise = Random.Range(0,2);
			if(clockwiseOrAnticlockwise > 0)
				rotateClockwise(Random.Range(1,4));
			else
				rotateAntiClockwise(Random.Range(1,4));
		}
		else{
			var leftOrRight = Random.Range(0,2);
			if(leftOrRight > 0)
				moveRight(Random.Range(1,5));
			else
				moveLeft(Random.Range(1,5));
		}
	}
	
	public void rotateClockwise(int rotations){
		Debug.Log("Rotating Clockwise "+rotations+" times");
		rotateAIClockwiseEvent.Invoke();
	}
	
	public void rotateAntiClockwise(int rotations){
		Debug.Log("Rotating Anti-Clockwise "+rotations+" times");
		rotateAIAntiClockwiseEvent.Invoke();
	}
	
	public void moveRight(int steps){
		Debug.Log("Moving right "+steps+" times");
		moveAIRightEvent.Invoke();
	}
	
	public void moveLeft(int steps){
		Debug.Log("Moving left "+steps+" times");
		moveAILeftEvent.Invoke();
	}
}
