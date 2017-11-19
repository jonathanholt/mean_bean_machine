using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic; 

[System.Serializable] 
public class PlayerProgress {

	public float score;
	public float time;
	public List<string> levels = new List<string>();
	public List<int> completed = new List<int>();

	public PlayerProgress () {
		this.score = 0;
		this.time = 0;
		levels.Add ("level1");
		levels.Add ("level2");
		levels.Add ("level3");
		levels.Add ("level_center");
		for(int i = 0; i < levels.Count; i++){
			if (levels [i] == "level_center")
				completed.Add (1);
			else
				completed.Add(0);
		}

	}
}
