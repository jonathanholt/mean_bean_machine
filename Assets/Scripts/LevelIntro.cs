using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelIntro : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;
	public bool isPaused;
	public GameObject pauseMenuCanvas;
	public string titleText;
	public Text titleTextArea;
	public AudioSource audioSource;
	public AudioSource introTheme;
	public GameObject NPCfaceImage;
	public Sprite faceImage;
	public Sprite faceImage2;
	public bool stopped;

	void Start(){
		introTheme.transform.position = transform.position;
		introTheme.Pause ();
	}
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			if(!stopped){
			introTheme.UnPause ();
			//titleTextArea.text = titleText;
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0.01f;
			audioSource.transform.position = transform.position;
			audioSource.Pause ();
			NPCfaceImage.GetComponent<Image> ().sprite = faceImage;
			StartCoroutine(MyCoroutine());
			//Debug.Log (Time.fixedDeltaTime);
			}
		} else {
			introTheme.Stop ();
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
			audioSource.transform.position = transform.position;
			audioSource.UnPause ();
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			isPaused = !isPaused;
		}

	}

	private IEnumerator MyCoroutine() {
		stopped = true;
		Debug.Log ("waiting");
		yield return new WaitForSeconds (0.07f);
		Debug.Log ("still waiting");
		NPCfaceImage.GetComponent<Image> ().sprite = faceImage2;
		yield return new WaitForSeconds (1f);
		}


	public void Resume(){
		isPaused = false;
	}

	public void LevelSelect(){
		Application.LoadLevel (levelSelect);
	}

	public void Quit(){
		Application.LoadLevel (mainMenu);
	}
}
