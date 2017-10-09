using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public String terrainChoice;
	public int mapSizeX;
	public int mapSizeY;



	//int level is the index of the level in the build settings
	public void LoadScene(){
        //SceneManager.LoadScene (terrainChoice);
        SceneManager.LoadScene(1);
    }

	public void chooseMapLength(){
        GameObject inputFieldGo = GameObject.Find("LengthInput");
        InputField lengthInput = inputFieldGo.GetComponent<InputField>();

        this.mapSizeX = int.Parse (lengthInput.text);

		Debug.Log ("mapSizeX: " + mapSizeX);
	}

	public void chooseMapWidth(){
        GameObject inputFieldGo = GameObject.Find("LengthInput");
        InputField widthInput = inputFieldGo.GetComponent<InputField>();

        this.mapSizeY = int.Parse (widthInput.text);

		Debug.Log ("mapSizeY: " + mapSizeY);
	}

	public void chooseTerrain(int terrain){
		switch (terrain){
		case 0:
			terrainChoice = "Lunar";
			break;
		case 1:
			terrainChoice = "Martian";
			Debug.Log ("terrainChoice: " + terrainChoice);
			break;
		case 2:
			terrainChoice = "Titan";
			Debug.Log ("terrainChoice: " + terrainChoice);
			break;
		case 3:
			terrainChoice = "Omicron Persei 8";
			Debug.Log ("terrainChoice: " + terrainChoice);
			break;
		}
	}
}

