using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSetupParameters : MonoBehaviour {

	public String terrainType;
	public int mapSizeX;
	public int mapSizeY;

	public int numberOfTeams;
	public int numberOfPlayersPerTeam;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);

//		GameObject lengthInput;
//		lengthInput = GameObject.Find ("LengthInput");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
