using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game { 

	public static Game current;
	public PlayerProgress progress;

	public Game () {
		progress = new PlayerProgress();
	}

}