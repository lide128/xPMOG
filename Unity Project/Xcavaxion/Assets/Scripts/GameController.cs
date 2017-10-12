using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameSetupParameters parameters; 

	public MapController currentGameMap;

	public List<PlayerController> players;

	public bool gameWon;

	public bool basesPresent;
	public bool playersDistributed;

	public int boulderCount;
	public int elementBoxCount;

	// Use this for initialization
	void Start () {

		gameWon = false;
		basesPresent = false;
		playersDistributed = false;
		boulderCount = 0;
		elementBoxCount = 0;

		//Load menu screen parameters in to mapcontroller?


	}
	
	// Update is called once per frame
	void Update () {

		GetBoulderState ();
		GetElementBoxState ();

		if(boulderCount == 0 && elementBoxCount == 0){

			if(CheckPlayerInventories()){
				EndGame ();
			}
		}

		if(!basesPresent){
			if(CheckForBases()){
				basesPresent = true;
				Debug.Log ("Found bases!");
			}
		}
		if(basesPresent && !playersDistributed){
			DistributePlayers ();
			playersDistributed = true;
			Debug.Log ("Distributed players!");
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

	//checks all the in game player inventories to make sure they are empty to end the game
	public bool CheckPlayerInventories(){
		int totalVolume = 0;
		bool emptyInventories = false;
		foreach(PlayerController cont in players){
			totalVolume += cont.inventory.currentTotalElementVolume;
		}
		if(totalVolume == 0){
			emptyInventories = true;
		}
		return emptyInventories;
	}

	public bool CheckForBases(){
		Debug.Log ("checking for bases!");
		GameObject blueBase = GameObject.Find ("BlueBase(Clone)");
		GameObject greenBase = GameObject.Find ("GreenBase(Clone)");
		GameObject orangeBase = GameObject.Find ("OrangeBase(Clone)");
		GameObject redBase = GameObject.Find ("RedBase(Clone)");

		bool blueCheck = false;
		bool greenCheck = false;
		bool orangeCheck = false;
		bool redCheck = false;

		if(blueBase != null){
			Debug.Log ("blue base found");
			blueCheck = true;
		}
		if(greenBase != null){
			Debug.Log ("green base found");

			greenCheck = true;
		}
		if(orangeBase != null){
			Debug.Log ("orange base found");

			orangeCheck = true;
		}
		if(redBase != null){
			Debug.Log ("red base found");

			redCheck = true;
		}

		return blueCheck && greenCheck && orangeCheck && redCheck;
	}


	public void DistributePlayers(){

		foreach(PlayerController player in players){

			if(player.teamColor.Equals("blue")){
				GameObject blueBase = GameObject.Find ("BlueBase(Clone)");
				blueBase.GetComponent<BaseController> ().teamPlayers.Add (player);	
				Debug.Log ("player added to blue base");
			}
			if(player.teamColor.Equals("green")){
				GameObject greenBase = GameObject.Find ("GreenBase(Clone)");
				greenBase.GetComponent<BaseController> ().teamPlayers.Add (player);
				Debug.Log ("player added to green base");
			}
			if(player.teamColor.Equals("orange")){
				GameObject orangeBase = GameObject.Find ("OrangeBase(Clone)");
				orangeBase.GetComponent<BaseController> ().teamPlayers.Add (player);
				Debug.Log ("player added to orange base");
			}
			if(player.teamColor.Equals("red")){
				GameObject redBase = GameObject.Find ("RedBase(Clone)");
				redBase.GetComponent<BaseController> ().teamPlayers.Add (player);
				Debug.Log ("player added to red base");
			}
		}

	}

}
