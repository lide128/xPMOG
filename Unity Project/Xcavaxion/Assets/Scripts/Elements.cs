using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements {

	//This class represents the concept of an element
	//It is not something that can be aquired in game
	//Elements are given interactivity with ElementContainers, in which they have an amount, the volume.
	//Elements are represented in game by ElementBoxes, which have an ElementContainer within

	/*
	 *      Elements
	 *         |
	 *         |
	 *         v
	 *  ElementContainer
	 *         |
	 *         |
	 *         V
	 *     ElementBox
	 * 
	 */

	public string name;					//the name of the type of element
	public string description;			//a short text description to possibly include in the UI, with possibly useful info?
	public string symbol;
	public int extractionDifficulty;
	public int valueByWeight;
	public int prevalence;
	public int density;


	public Elements(string name, string description, string symbol, int extractionDifficulty, int valueByWeight, int prevalence, int density){
		this.name = name;
		this.description = description;
		this.symbol = symbol;
		this.extractionDifficulty = extractionDifficulty;
		this.valueByWeight = valueByWeight;
		this.prevalence = prevalence;
		this.density = density;
	}

}
