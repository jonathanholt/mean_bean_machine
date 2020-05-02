using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeanFactory : MonoBehaviour {
	
	public GameObject startPoint;
	public GameObject[] startPointArray;
	public GameObject startPointUpper;
	bool readyForNext = false;
	string[] prefabs = {"purple1", "red1", "blue1", "green1", "yellow1", "grey1"};
	string greyString = "grey1";
	public GameObject thePlayer;
	public GameObject beanArray;
	public bool avalancheNext;
	public bool canMove = true;
	public bool isPlayer;
	public GameObject hasBean;
	public bool isGameOver;
	public GameObject panel;
	
	public UnityEvent newPair;
	

	public void createBeanPair (bool isAvalanche, int avalancheCount = 0) {
		if(isPlayer){
			//isGameOver = true;
			//gameOver();
		}
		
		if(isPlayer)
			hasBean.GetComponent<HasBeanController>().chooseAnimation();
		if(avalancheCount > 2){
			//Debug.Log("HIGHER THAN 2!!!");
				thePlayer.GetComponent<MotionController>().resetCurrentPosition(2);
				string randomPrefab1 = greyString;
				for(int i = 0; i < 5; i ++){
					GameObject bean1 = Instantiate(Resources.Load(randomPrefab1)) as GameObject;
					bean1.GetComponent<Bean>().SetPlayer(thePlayer);
					bean1.GetComponent<Bean>().SetBeanArray(beanArray);
					bean1.name = randomPrefab1;
					bean1.GetComponent<Bean> ().setInPlay (1);
					bean1.transform.position = startPointArray[i].transform.position;
				}
		}
		else{
		thePlayer.GetComponent<MotionController>().resetCurrentPosition(2);
		string randomPrefab1;
		if(isAvalanche)
			randomPrefab1 = greyString;
		else
			randomPrefab1 = choosePrefab();
		
		GameObject bean1 = Instantiate(Resources.Load(randomPrefab1)) as GameObject;
		bean1.GetComponent<Bean>().SetPlayer(thePlayer);
		bean1.GetComponent<Bean>().SetBeanArray(beanArray);
		bean1.name = randomPrefab1;
		bean1.GetComponent<Bean> ().setInPlay (1);
		if(isAvalanche){
			//Debug.Log("avalanche..."+thePlayer.GetComponent<AvalancheController>().avalancheToFall);
			int resetPosition = Random.Range(0, 4);
			bean1.transform.position = startPointArray[resetPosition].transform.position;
		}
		else{
			bean1.transform.position = startPoint.transform.position;
		}
		if (!isAvalanche) {
			string randomPrefab2 = choosePrefab ();
			GameObject bean2 = Instantiate (Resources.Load (randomPrefab2)) as GameObject;
			bean2.GetComponent<Bean> ().SetPlayer (thePlayer);
			bean2.GetComponent<Bean> ().SetBeanArray (beanArray);
			bean2.name = randomPrefab2;
			bean2.GetComponent<Bean> ().setInPlay (2);
			bean2.transform.position = startPointUpper.transform.position;
			
			if(!isPlayer){
				StartCoroutine (InvokePause (1.5f));
			}
		}
		}
	}
	
	public string choosePrefab(){
		int randomNumber = Random.Range(0, 5);
		return prefabs[randomNumber];
	}
	
	public void createNext(){
		if(!isGameOver){
		
		if(!readyForNext){
			readyForNext = true;
			if (thePlayer.GetComponent<AvalancheController>().avalancheCount > 0) {
				////Debug.Log ("Avalanche!!");
				thePlayer.GetComponent<AvalancheController> ().queueAvalanche ();
			}

			if(avalancheNext){
				canMove = false;
				thePlayer.GetComponent<AvalancheController> ().processAvalanche ();
				avalancheNext = false;
				StartCoroutine (LongPause (2f));
			}
			else {
				canMove = true;
				createBeanPair (false);
				StartCoroutine (LongPause (2f));
			}
		}
	}
	}
	
	IEnumerator InvokePause(float delayTime){
		yield return new WaitForSeconds (delayTime);
		newPair.Invoke();
	}
	
	IEnumerator LongPause(float delayTime){
		yield return new WaitForSeconds (delayTime);
		readyForNext = false;
	}
	
	void Update () {
		if (Input.GetKeyUp ("p")) {
			gameOver("AIPlayer");
		}
		
		if (Input.GetKeyUp ("o")) {
			gameOver("HumanPlayer");
		}
		
		
		if(isGameOver){
			Vector2 aPosition1 = new Vector2(30,30);
			GameObject gameOverText = GameObject.Find("GameOverFont");
			GameObject gameOverTarget = GameObject.Find("GameOverPixel");
			gameOverText.transform.position = Vector2.MoveTowards(new Vector2(gameOverText.transform.position.x, gameOverText.transform.position.y), gameOverTarget.transform.position, Time.deltaTime);

			Animator gameOverAnimator;
			gameOverAnimator = GameObject.Find("BlueGameOver").GetComponent<Animator> ();
			gameOverAnimator.SetBool ("gameOver", true);

			gameOverAnimator = GameObject.Find("RedGameOver").GetComponent<Animator> ();
			gameOverAnimator.SetBool ("gameOver", true);

			gameOverAnimator = GameObject.Find("YellowGameOver").GetComponent<Animator> ();
			gameOverAnimator.SetBool ("gameOver", true);

			gameOverAnimator = GameObject.Find("GreenGameOver").GetComponent<Animator> ();
			gameOverAnimator.SetBool ("gameOver", true);

			gameOverAnimator = GameObject.Find("PurpleGameOver").GetComponent<Animator> ();
			gameOverAnimator.SetBool ("gameOver", true);
		}
	}
		
		public void gameOver(string playername){
			isGameOver = true;
			Debug.Log("Game Over");
		if(playername == "AIPlayer"){
			Debug.Log("For AI player");
			EnemyController.changeAnimationLost ();
			GameObject ground = GameObject.Find("EnemyGround");
			Destroy (ground);
			GameObject playerFrameFloor = GameObject.Find("EnemyFrameFloor");
			Destroy (playerFrameFloor);
			GameObject YouWinComponents = GameObject.Find("YouWinComponents");
			YouWinComponents.transform.position = new Vector3(YouWinComponents.transform.position.x,YouWinComponents.transform.position.y + 6.75f, YouWinComponents.transform.position.z);
		}
		else{
			Debug.Log("For Human Player");
		EnemyController.changeAnimationWinning ();
		EnemyLowerController.changeAnimationWinning ();
		GameObject ground = GameObject.Find("Ground");
		Destroy (ground);
		GameObject playerFrameFloor = GameObject.Find("PlayerFrameFloor");
		Destroy (playerFrameFloor);
		GameObject AIPlayer = GameObject.Find("AIPlayer");
		Destroy (AIPlayer);
		// fade to black and go to game over screen after a certain amount of time
		StartCoroutine(Transition(3f));
		}
	}
	
		IEnumerator Transition(float delayTime){
		yield return new WaitForSeconds (delayTime);
		Debug.Log("Fade to black");
		CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
		while(canvasGroup.alpha < 1){
			canvasGroup.alpha += Time.deltaTime;
			yield return null;
		}
		canvasGroup.interactable = false;
		yield return null;
	}
}
