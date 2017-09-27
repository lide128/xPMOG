using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour {

	public int totalCurrency;

	public int elementVolumeCapacity; //the carrying capacity of elements based on volume
	public int currentTotalElementVolume;

	public List<string> items; //temporarily string until class is made
	public List<ElementContainer> elementsInventory;
	public List<string> codeNuggets; //temporarily string until class is made

	public List<string> elementNames;
	public List<int> elementVols;
	public int elementsCount;

	public bool elementsUpdated; //in order to keep track of the current inventory status


	// Use this for initialization
	void Start () {
		elementsInventory = new List<ElementContainer> ();
		elementNames = new List<string> ();
		elementVols = new List<int> ();
		elementsUpdated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(elementsUpdated){ //this updates the inspector view of the inventory at the moment, for the UI later
			elementsCount = elementsInventory.Count;

			updateNameList ();
			updateVolList ();

			elementsUpdated = false;
		}
	}

	void updateNameList(){
		elementNames.Clear();
		foreach(ElementContainer cont in elementsInventory){
			elementNames.Add(cont.contents.name);
		}
	}

	void updateVolList(){
		elementVols.Clear();
		foreach(ElementContainer cont in elementsInventory){
			elementVols.Add (cont.volume);
		}
	}

	public bool CheckIfElementAlreadyContained(string elementName){
		bool elementContained = false;
		foreach(ElementContainer cont in elementsInventory){
			if(elementName.Equals(cont.contents.name)){
				elementContained = true;
			} 
		}
		return elementContained;
	}

	//this does work, now handling element inventory max, partial amount handled in PartialElementAdd
	public bool AddElementContainerToInventory(ElementContainer elementContainerToAdd){
		bool successfullyAdded = false;
		string toAddName = elementContainerToAdd.contents.name;
		int volumeToAdd = elementContainerToAdd.volume;
		if (CheckIfElementAlreadyContained (toAddName)) { //if element container already in player inventory
			successfullyAdded = AddElementContentsTo (toAddName, volumeToAdd); //volume overflow check within this function
			Debug.Log ("Added " + volumeToAdd + " of " + toAddName + " to preexisting element container in player inventory.");
		} 
		else { //if the element container is not in the player inventory
			if(!CheckForVolumeOverflow(volumeToAdd)){
				elementsInventory.Add (elementContainerToAdd);
				currentTotalElementVolume += volumeToAdd;
				Debug.Log ("Added element container of: " + volumeToAdd + " " + toAddName + " to the player inventory elements");
				successfullyAdded = true;
			}
		}
		elementsUpdated = successfullyAdded;
		return successfullyAdded;
	}

	//assumes element container already exists in list of buried elements
	public bool AddElementContentsTo(string elementName, int volumeToAdd){
		bool successfullyAdded = false;
		foreach(ElementContainer cont in elementsInventory){
			if(elementName.Equals(cont.contents.name)){
				if(!CheckForVolumeOverflow(volumeToAdd)){ //if there is no overflow add amount to existing container
					Debug.Log("elementInventory " + elementName + " volume before add: " + cont.volume);
					cont.volume += volumeToAdd; //add the amount to preexisting element container
					currentTotalElementVolume += volumeToAdd;
					Debug.Log("elementInventory " + elementName + " volume after add: " + cont.volume);
					successfullyAdded = true;
				}
			}
		}
		return successfullyAdded;
	}

	//Add part of the volume of an element container to the players inventory if there is not enough room for the whole
	public void PartialElementAdd(ElementContainer elementToDivide){
		int difference = elementVolumeCapacity - currentTotalElementVolume;
		string elementName = elementToDivide.contents.name;
		ElementContainer whatWillFit = new ElementContainer (elementToDivide.contents, difference);
		AddElementContainerToInventory (whatWillFit); //add what will fit to the inventory
		elementToDivide.volume -= difference; //remove the amount from the element container
		Debug.Log ("Added " + difference + " of " + elementName + " to player inventory. ElementBox still contains " + elementToDivide.volume + " of " + elementName + ".");
	}


	//returns true if there is volume overflow when adding elements to inventory
	public bool CheckForVolumeOverflow(int volumeToAdd){
		bool volumeOverflow = false;
		int newVolumeTotal = currentTotalElementVolume + volumeToAdd;
		if(newVolumeTotal > elementVolumeCapacity){
			volumeOverflow = true;
		}
		return volumeOverflow;
	}
		
	//TODO this needs to be updated
	//assumes element container exists in inventory
	//also assumes volume present in the inventory
	//also cleans up element inventory by removing empty element containers
	public ElementContainer removeElementAmount(int amountToRemove, Elements elementToRemove){

		ElementContainer toRemove = new ElementContainer ();
		toRemove.contents = elementToRemove;
		toRemove.volume = amountToRemove;

		//remove amount from inventory
		foreach(ElementContainer container in elementsInventory){
			if(container.contents.Equals(elementToRemove)){
				container.volume -= amountToRemove;
				Debug.Log ("Removed: " + amountToRemove + " of " + elementToRemove.name + " from inventory.");
			}
			if(container.volume == 0){
				elementsInventory.Remove (container);
				Debug.Log ("Removed " + elementToRemove.name + " container from the inventory because it is empty.");
			}
		}			
		return toRemove;
	}


}
