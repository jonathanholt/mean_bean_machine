using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {

	public List<Instruction> getRandom(){
		List<Instruction> instructions = new List<Instruction> () {
			new Instruction { colour1 = EnemyNewBean.nextRandomBean2, colour2 = EnemyNewBean.nextRandomBean1, rotation = Random.Range (1, 4), column = Random.Range (-1, 5) }
		};
		return instructions;
	}
}
