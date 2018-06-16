using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour {

	//Return array format = (how many turns it's valid for, array(turn 1 : array(colour 1, colour 2, rotation, ending column), turn 2 : array()));

	// Chooses a random column & orientation for the enemy bean pair
	public List<Instruction> getRandom(){
		List<Instruction> instructions = new List<Instruction> () {
			new Instruction { colour1 = 1, colour2 = 1, rotation = 1, column = 1 }
		};
		return instructions;
	}

	// Finds a matching colour so the enemy beans to land on
	public void getMagenetic(){
		
	}

	// Return a sequence of moves for the enemy to do
	public void getSequence(){
		// two of the same colour, landing horizontally in the center
		// one more of that colour with a different colour on top
		// two of this new colour
		// One more of this new colour with a third colour on top
	}

	// Return a more complex sequence of moves for the enemy to do
	public void getSequenceComplex(){
	
	}
}
