using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameSetupParameters parameters; 

	public MapController currentGameMap;

	public bool gameWon;

	public int boulderCount;
	public int elementBoxCount;

	// Use this for initialization
	void Start () {

		gameWon = false;
		boulderCount = 0;
		elementBoxCount = 0;

		//Load menu screen parameters in to mapcontroller?


	}
	
	// Update is called once per frame
	void Update () {

		GetBoulderState ();
		GetElementBoxState ();

		if(boulderCount == 0 && elementBoxCount == 0){
			EndGame ();
		}
	}

	public void GetBoulderState(){
		boulderCount = currentGameMap.onScreenBoulderCount;
	}

	public void GetElementBoxState(){
		elementBoxCount = currentGameMap.onScreenElementBoxCount;
	}

	public void EndGame(){
		SceneManager.LoadScene(0);
	}

}
