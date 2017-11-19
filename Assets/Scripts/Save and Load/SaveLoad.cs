using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad {

	public static Game savedGame;

	//it's static so we can call it from anywhere
	public static void Save(Game toSave) {
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGame.dat"); //you can call it anything you want
		bf.Serialize(file, toSave);
		Debug.Log ("Before save = " + toSave.progress.score);
		file.Close();
		Load ();

	}

	public static void Load() {
		if (File.Exists (Application.persistentDataPath + "/savedGame.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedGame.dat", FileMode.Open);
			savedGame = (Game)bf.Deserialize (file);
			//Debug.Log ("After load = " + savedGame.progress.score);
			file.Close ();
		} else {
			savedGame = new Game();
		}
	}

	public static bool FileExists(){
		Debug.Log (Application.persistentDataPath + "/savedGames.dat");
		Debug.Log (File.Exists ("C:/Users/jholt_000/AppData/LocalLow/DefaultCompany/Test Platform/savedGames.dat"));
		return(File.Exists (Application.persistentDataPath + "/savedGames.dat"));
	}
}