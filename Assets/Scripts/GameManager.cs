using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playButton;
	public GameObject playerShip;
	public GameObject enemySpawner;
	public GameObject GameOver;
	public GameObject GameTitle; 

	public enum GameManagerState{

		Opening,
		Gameplay,
		GameOver,

	}

	GameManagerState GMState;


	// Use this for initialization
	void Start () {
	
		GMState = GameManagerState.Opening;

	}


	void UpdateGameManagerState () {
		switch (GMState) {
		case GameManagerState.Opening:
			//Hide "game over"
			GameOver.SetActive (false);

			GameTitle.SetActive (true);

			playButton.SetActive(true);
			break;
		case GameManagerState.Gameplay:

			//hide button while playing
			playButton.SetActive (false);

			GameTitle.SetActive (false);

			//and shp visible
			playerShip.GetComponent<PlayerControl>().Init();

			//Start enemy spawner
			enemySpawner.GetComponent<EnemySpawner>().SchedulEnemySpawner();

			break;

		case GameManagerState.GameOver:

			//Stop enemies
			enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
			//Display "game over"

			GameOver.SetActive(true);

			//Change game manager to opening
			Invoke("ChangeToOpeningState", 5f);

			break;
		}
	}

	public void SetGameManagerState(GameManagerState state){

		GMState = state;
		UpdateGameManagerState ();

	}

	public void StartGamePlay(){

		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState (); 

	}
	//Func to chsnge gm state to opening
	public void ChangeToOpeningState(){

		SetGameManagerState (GameManagerState.Opening);

	}

}
