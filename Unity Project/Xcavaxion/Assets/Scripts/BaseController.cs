using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseController : TileController {

	//A subclass of TileController to handle all of the specifics related to base operations
	//May want to make a generic "move element containers around" class since this is the third time it'll be repeated

	public string baseOwner;

	public int totalElementVolume; 

	public List<string> nameOfStoredElements;
	public List<int> volumeOfStoredElements;

	public List<string> linkedStructures; //references to strucutes that may be linked to this base, no idea yet.

	public Inventory baseInventory;

	// Use this for initialization
	public override void Start () {
		base.Start (); //should be okay for now a lot of the TileController classes Start() functionality isn't needed

		nameOfStoredElements = new List<string> ();
		volumeOfStoredElements = new List<int> ();

		baseInventory = new Inventory (false, 0);
		baseInventory.elementsUpdated = false;

	}
	
	// Update is called once per frame
	public override void Update () {
		//don't think we need any of the TileController update functionality

		bool updated = baseInventory.elementsUpdated;

		if(updated){
			Debug.Log ("base inventory updated!");
			nameOfStoredElements = baseInventory.GetElementNames ();
			volumeOfStoredElements = baseInventory.GetElementVolumes ();

			totalElementVolume = baseInventory.currentTotalElementVolume;
			baseInventory.elementsUpdated = false;
		}
	}



	//How to handle players moving on to the base
	//Temp: just want to have the player dump all elements in to the base for the time being
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("base trigger!");
		string triggeesName = other.gameObject.name;
		Debug.Log (FirstCharToUpper(triggeesName) + " triggered " + baseOwner + "'s base.");
		if(triggeesName == "Player"){
			Debug.Log ("Base trigger player!");
		}

	}
		
	public static string FirstCharToUpper(string input){
		return input.Substring (0, 1).ToUpper () + input.Substring (1);
	}

	void OnMouseOver(){
		rend.material.color = new Color(0.95f, 0, 0, 0.8f);
	}

	void OnMouseExit(){
		rend.material.color = Color.white; //restores tile color
	}


}
