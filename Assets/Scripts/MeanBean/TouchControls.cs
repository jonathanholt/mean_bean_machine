using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchControls : MonoBehaviour
{
	public Button yourButton;
	public GameObject thePlayer;
	private string buttonName;

	void Start()
	{
		thePlayer = GameObject.Find("Player");
		Button btn = yourButton.GetComponent<Button>();
		buttonName = btn.name;
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		if (buttonName == "LeftButton") {
			thePlayer.GetComponent<PlayerController> ().leftMove ();
		}
		else if (buttonName == "RightButton") {
			thePlayer.GetComponent<PlayerController> ().rightMove ();
		}
		else if (buttonName == "AButton") {
			thePlayer.GetComponent<PlayerController> ().rotateMoveA ();
		}
		else if (buttonName == "BButton") {
			thePlayer.GetComponent<PlayerController> ().rotateMoveB ();
		}
	}
}