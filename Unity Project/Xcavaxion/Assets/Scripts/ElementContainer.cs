using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementContainer {

	public Elements contents; //what element is in the container
	public int volume; //the amount of the element in the container

	public ElementContainer(){
		Debug.Log ("Element container created");
		contents = GameItemManager.listOfElements[0]; //default give it the empty element upon creation
		Debug.Log ("new element container contains!: " + contents.name);
		volume = 0; //and volume of zero
		Debug.Log ("new element container volume: " + volume);
	}

	//TODO I don't think this is used anywhere, may need to be removed
	public void AddElementToContainer(Elements toAdd, int amountToAdd){
		contents = toAdd;
		volume = amountToAdd;
		Debug.Log ("Added " + amountToAdd + ", " + toAdd.name + " element container.");
	}

}
