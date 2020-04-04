using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanFactory : MonoBehaviour {
	
	public GameObject startPoint;
	public GameObject[] startPointArray;
	public GameObject startPointUpper;
	bool readyForNext = false;
	string[] prefabs = {"purple1", "red1", "blue1", "green1", "yellow1"};
	string greyString = "grey1";
	public GameObject thePlayer;
	public GameObject beanArray;
	public bool avalancheNext;
	public bool canMove = true;
	public bool isPlayer;
	public GameObject hasBean;
	public bool isGameOver;
	

	public void createBeanPair (bool isAvalanche, int avalancheCount = 0) {
		if(isPlayer){
			//isGameOver = true;
			//gameOver();
		}
		
		if(isPlayer)
			hasBean.GetComponent<HasBeanController>().chooseAnimation();
		if(avalancheCount > 2){
			Debug.Log("HIGHER THAN 2!!!");
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
			Debug.Log("avalanche..."+thePlayer.GetComponent<AvalancheController>().avalancheToFall);
			int resetPosition = Random.Range(0, 4);
			bean1.transform.position = startPointArray[resetPosition].transform.position;
		}
		else{
			bean1.transform.position = startPoint.transform.position;
		}
		if (!isAvalanche) {
			//string randomPrefab2 = choosePrefab ();
			//GameObject bean2 = Instantiate (Resources.Load (randomPrefab2)) as GameObject;
			//bean2.GetComponent<Bean> ().SetPlayer (thePlayer);
			//bean2.GetComponent<Bean> ().SetBeanArray (beanArray);
			//bean2.name = randomPrefab2;
			//bean2.GetComponent<Bean> ().setInPlay (2);
			//bean2.transform.position = startPointUpper.transform.position;
		}
		}
	}
	
	public string choosePrefab(){
		int randomNumber = Random.Range(0, 5);
		return prefabs[0];
	}
	
	public void createNext(){
		if(!isGameOver){
		
		if(!readyForNext){
			readyForNext = true;
			if (thePlayer.GetComponent<AvalancheController>().avalancheCount > 0) {
				//Debug.Log ("Avalanche!!");
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
	
	IEnumerator LongPause(float delayTime){
		yield return new WaitForSeconds (delayTime);
		readyForNext = false;
	}
	
	void Update () {
		if(isGameOver){/**
			Vector2 aPosition1 = new Vector2(30,30);
			GameObject gameOverText = GameObject.Find("GameOverFont");
			GameObject gameOverTarget = GameObject.Find("GameOverPixel");
			gameOverText.transform.position = Vector2.MoveTowards(new Vector2(gameOverText.transform.position.x, gameOverText.transform.position.y), gameOverTarget.transform.position, 3 * Time.deltaTime);

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
			gameOverAnimator.SetBool ("gameOver", true); **/
		}
	}
		
		public void gameOver(string playername){
		if(playername == "AIPlayer"){
			EnemyController.changeAnimationLost ();
			GameObject ground = GameObject.Find("EnemyGround");
			Destroy (ground);
			GameObject playerFrameFloor = GameObject.Find("EnemyFrameFloor");
			Destroy (playerFrameFloor);
			GameObject YouWinComponents = GameObject.Find("YouWinComponents");
			YouWinComponents.transform.position = new Vector3(YouWinComponents.transform.position.x,YouWinComponents.transform.position.y + 6.75f, YouWinComponents.transform.position.z);
		}
		else{
		EnemyController.changeAnimationWinning ();
		EnemyLowerController.changeAnimationWinning ();
		GameObject ground = GameObject.Find("Ground");
		Destroy (ground);
		GameObject playerFrameFloor = GameObject.Find("PlayerFrameFloor");
		Destroy (playerFrameFloor);
		}
	}
}
