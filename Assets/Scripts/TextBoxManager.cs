using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public TextAsset textFile;
	public string[] textLines;
	public GameObject textBox;
	public Text theText;
	public int currentLine;
	public int endAtLine;
	public PlayerController player;
	public bool isActive;
	public GameObject NPCfaceImage;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		if (textFile) {
			textLines = textFile.text.Split ('\n');
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		if (isActive) {
			EnableTextBox ();
		} else {
			DisableTextBox ();
		}
	}

	void Update(){

		if (!isActive) {
			return;
		}

			if (textBox.activeSelf) {
			if(textLines [currentLine] == textLines[textLines.Length - 1])
				theText.text = '"' + textLines [currentLine] + '"';
			else
				theText.text = '"' + textLines [currentLine] + '"' + " ►";

				if (Input.GetKeyDown (KeyCode.Return)) {
					currentLine++;
				}

				if (currentLine > endAtLine) {
					DisableTextBox ();
				}

			}
		}

	public void EnableTextBox(){
		textBox.SetActive (true);
	}

	public void DisableTextBox(){
		textBox.SetActive (false);
	}

	public void ReloadScript(TextAsset theText){
		if (theText != null) {
			textLines = new string[1];
			textLines = theText.text.Split ('\n');
		}
	}

	public void ChangeImage(Sprite faceImage){
		if (faceImage != null) {
			NPCfaceImage.GetComponent<Image> ().sprite = faceImage;
		}
	}
}
