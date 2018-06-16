using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNewBean : MonoBehaviour {
	
	public static GameObject bean1;
	public static GameObject bean2;
	public static GameObject nextBean1;
	public static GameObject nextBean2;
	public static int nextRandomBean1 = 0;
	public static int nextRandomBean2 = 0;
	public static int randomBean1 = 0;
	public static int randomBean2 = 0;
	public static Object [] sprites;
	public static EnemyAIController ai; 
	public static int currentInstructionsCount = 0;
	public static Instruction currentInstruction;

	void Start () {
		bean1 = GameObject.Find("enemybean1");
		bean2 = GameObject.Find("enemybean2");
		nextBean1 = GameObject.Find("enemyNextBean1");
		nextBean2 = GameObject.Find("enemyNextBean2");
		sprites = Resources.LoadAll ("beans");
		nextRandomBean1 = Random.Range (1, 6);
		nextRandomBean2 = Random.Range (1, 6);
		ai = new EnemyAIController();
		createNewBeanPair ();
	}

	public static void createNewBeanPair(){
		if (currentInstructionsCount == 0) {
			List<Instruction> instructions = ai.getRandom ();
			currentInstructionsCount = instructions.Count;
			currentInstruction = instructions[currentInstructionsCount - 1];
		}
		randomBean1 = currentInstruction.colour1;
		randomBean2 = currentInstruction.colour2;
		bean1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [randomBean2];
		bean2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [randomBean1];
		createNextBeanPair ();
		currentInstructionsCount--;
	}

	public static void createNextBeanPair(){
		nextRandomBean1 = Random.Range(1, 6);
		nextRandomBean2 = Random.Range(1, 6);
		nextBean1.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [nextRandomBean1];
		nextBean2.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites [nextRandomBean2];
	}

	private int getRandomBean1(){
		return randomBean1;
	}

	private int getRandomBean2(){
		return randomBean2;
	}

}
