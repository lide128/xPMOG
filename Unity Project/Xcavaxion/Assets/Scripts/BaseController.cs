using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

public class BaseController : TileController {

	//A subclass of TileController to handle all of the specifics related to base operations
	//May want to make a generic "move element containers around" class since this is the third time it'll be repeated

	public string baseOwner;

	public List<ElementContainer> storedElements; //elements that are simply stored
	public List<ElementContainer> consumableElements; //elements that are set aside to be consumed
	public List<string> nameOfStoredElements;
	public List<int> volumeOfStoredElements;
	public int totalElementVolume; //combined volume of elements in both lists
	public int storedElementCount;

//	public bool elementsUpdated;
	public List<string> linkedStructures; //references to strucutes that may be linked to this base, no idea yet.

	public Inventory baseInventory;

	// Use this for initialization
	public override void Start () {
		base.Start (); //should be okay for now a lot of the TileController classes Start() functionality isn't needed
		storedElements = new List<ElementContainer>();
		consumableElements = new List<ElementContainer> ();
		nameOfStoredElements = new List<string> ();
		volumeOfStoredElements = new List<int> ();
//		elementsUpdated = false;
		storedElementCount = 0;

		baseInventory = new Inventory (false, 0);
		baseInventory.elementsUpdated = false;

	}
	
	// Update is called once per frame
	public override void Update () {
		//don't think we need any of the TileController update functionality

//		storedElementCount = storedElements.Count;

//		if(elementsUpdated){
//
//			UpdateElementNameList ();
//			UpdateElementVolumeList ();
//
//			totalElementVolume = CurrentTotalVolume ();
//			elementsUpdated = false;
//		}
		bool updated = baseInventory.elementsUpdated;

		if(updated){
			Debug.Log ("base inventory updated!");
			nameOfStoredElements = baseInventory.GetElementNames ();
			volumeOfStoredElements = baseInventory.GetElementVolumes ();

			totalElementVolume = baseInventory.currentTotalElementVolume;
			baseInventory.elementsUpdated = false;
		}
	}

	void UpdateElementNameList(){
		nameOfStoredElements.Clear ();
		foreach(ElementContainer cont in storedElements){
			nameOfStoredElements.Add (cont.contents.name);
		}
	}

	void UpdateElementVolumeList(){
		volumeOfStoredElements.Clear ();
		foreach(ElementContainer cont in storedElements){
			volumeOfStoredElements.Add (cont.volume);
		}
	}

	int CurrentTotalVolume(){
		int returnVolume = 0;
		if(storedElements.Count != 0){
			foreach(ElementContainer cont in storedElements){
				returnVolume += cont.volume;
			}
		}
		if(consumableElements.Count != 0){
			foreach (ElementContainer cont in consumableElements) {
				returnVolume += cont.volume;
			}
		}
		return returnVolume;
	}

	int CurrentTotalElementCount(){
		int returnTotal = 0;
		if(storedElements.Count != 0){
			returnTotal += storedElements.Count;
		}
		if(consumableElements.Count != 0){
			returnTotal += consumableElements.Count;
		}
		return returnTotal;
	}

	//How to handle players moving on to the base
	//Temp: just want to have the player dump all elements in to the base for the time being
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("base trigger!");
		string triggeesName = other.gameObject.name;
		Debug.Log (FirstCharToUpper(triggeesName) + " triggered " + baseOwner + "'s base.");
		if(triggeesName == "Player"){
			Debug.Log ("Base trigger player!");
//			GameObject inv = other.gameObject.GetComponent<PlayerController>().inventory; //with copy won't be modifing original
//			List<ElementContainer> copyOfInventory = inv.gameObject.GetComponent<InventoryManager> ().elementsInventory;
//			TakeAllElements (copyOfInventory); //trying out the base "taking" them
//			//this below is dumb, to clear the inventory lol
//			other.gameObject.GetComponent<PlayerController> ().inventory.gameObject.GetComponent<InventoryManager> ().ClearInventory (); //doesn't seem to do anything
//			totalElementVolume = CurrentTotalVolume ();
//			elementsUpdated = true;

//			GameObject inv = other.gameObject.GetComponent<PlayerController> ().inventory;
//			List<ElementContainer> copyOfInventory = inv.gameObject.GetComponent<InventoryManager> ().playerInventory.elementsInventory;
//			TakeAllElements (copyOfInventory);
//			other.gameObject.GetComponent<PlayerController> ().inventory.gameObject.GetComponent<InventoryManager> ().playerInventory.elementsInventory.Clear(); //doesn't seem to do anything
//
		}

	}

	public void TakeAllElements(List<ElementContainer> elementsToTake){
		foreach(ElementContainer cont in elementsToTake){
			TakeSingleElement (cont); 		//add element to base inventory, checks for duplicates below
			//elementsToTake.Remove (cont); 	//remove element from player inventory
		}
	}

	public void TakeSingleElement(ElementContainer containerToTake){
		int allVol = containerToTake.volume;
		TakeSingleElementAmount (containerToTake, allVol);
	}

	//Add single element amount passed in to base inventory
	public void TakeSingleElementAmount(ElementContainer containerToTake, int volumeToTake){
		string elementName = containerToTake.contents.name;
		if(CheckIfElementAlreadyContained(storedElements, elementName)){
			foreach(ElementContainer cont in storedElements){
				if(cont.contents.name.Contains(elementName)){
					cont.volume += volumeToTake;
					containerToTake.volume -= volumeToTake;
				}
			}
		}
		else{
			storedElements.Add (containerToTake);
			//remove element from list, higher up in TakeAllElements
		}
			
//		elementsUpdated = true;
	}

	public bool CheckIfElementAlreadyContained(List<ElementContainer> toCheck, string elementName){
		bool elementContained = false;
		foreach(ElementContainer cont in toCheck){
			if(elementName.Equals(cont.contents.name)){
				elementContained = true;
			} 
		}
		return elementContained;
	}

	public static string FirstCharToUpper(string input){
		return input.Substring (0, 1).ToUpper () + input.Substring (1);
	}

	public ElementContainer GetElementContainerByName(List<ElementContainer> listToSearch, string elementName){
		foreach(ElementContainer cont in listToSearch){
			if(cont.contents.name.Equals(elementName)){
				return cont;
			}
		}
		return null;
	}

}
