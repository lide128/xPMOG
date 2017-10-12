using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIController : PlayerController {

	public int difficultyLevel; // 1 to 5? easiest to hardest? what does it mean?

	private Rigidbody2D cpuRigidBody;
	private BoxCollider2D cpuCollider2d;

	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;

	public List<GameObject> allGameItems;
	public GameObject[] _availableBoulders;
	public string[] _availableItems;
	public GameObject[] _availableElementBoxes;
	public string _availableCodeNuggets;

	public GameObject[,] _gameMap;

	System.Random rand = new System.Random();

	// Use this for initialization
	public override void Start () {

		base.Start (); //run the PlayerController start function
		cpuRigidBody = GetComponent<Rigidbody2D> ();
		cpuCollider2d = GetComponent<BoxCollider2D> ();

		timeBetweenMoveCounter = timeBetweenMove;
		timeToMoveCounter = timeToMove;

		//start the AI player on one of the not occupied bases, handled in the BaseController
	}

	// Update is called once per frame
	public override void Update () {

		GetCurrentMapState ();

		DifficultySelector();

	}

	public void FixedUpdate (){

	}

	public void DifficultySelector(){

		switch (difficultyLevel) {

		case 0: //crazy AI
			CrazyRandomMovement ();
			break;
		case 1: //dumb AI
			//simple random movement, returns to base when elements are full
			SensibleRandomMovement ();
			break;
		case 2: //easy AI
			//seeks nearest boulder, and nearest elementbox, returns to base when full
			EasyAIMovement();
			//something
			break;
		case 3: //average AI
			//seeks nearest boulder, prioritizes certain elementboxes, returns to base when full
			//something
			break;
		case 4: //smart AI
			//seeks nearest boulder, searches for specific elementboxes, grabs items, returns to base when element needs are met
			//something
			break;
		case 5: //very smart AI
			//objective based: only chooses boulders that don't let other teams in area, searches for specific elements, and items, grabs code nuggets, returns to base when objectives met
			//something
			break;
		default:
			SensibleRandomMovement ();
			break;
		}
	}

	public bool CheckTimePassed(float timeToWait){
		float delta = 0.0f;
		while(timeToWait >= delta){
			delta += Time.deltaTime;
		}
		return true;
	} 

	//this moves at too fast a speed to the destination
	public void MoveDistance(float amountX, float amountY){

		Vector2 newPosition = new Vector2 (transform.position.x + amountX, transform.position.y + amountY);
		cpuRigidBody.MovePosition (newPosition + cpuRigidBody.velocity * Time.fixedDeltaTime);
	}

	public void MoveToPoint(Vector3 moveToPoint){

		transform.position = Vector3.MoveTowards (transform.position, moveToPoint, playerSpeed * Time.deltaTime);
		playerMoving = true;
	}

	public void EasyAIMovement(){

		GameObject[] _allStuff = new GameObject[_availableBoulders.Length + _availableElementBoxes.Length];
		Array.Copy (_availableBoulders, _allStuff, _availableBoulders.Length);
		Array.Copy (_availableElementBoxes, 0, _allStuff, _availableBoulders.Length, _availableElementBoxes.Length);

		if(_allStuff.Length != 0 && !inventory.elementsFull){
			//go for the nearest thing
			GameObject nearestThing = ChooseClosest(_allStuff);
			MoveToPoint (nearestThing.transform.position);
		}

		else if(inventory.elementsFull || _allStuff.Length == 0){
			MoveToPoint (basePosition);
			playerMoving = false;
		}

	}

	public void SensibleRandomMovement(){
		
		if(inventory.elementsFull){
			MoveToPoint (basePosition);
			playerMoving = false;
		}

		if(playerMoving){
			timeToMoveCounter -= Time.deltaTime;
			cpuRigidBody.velocity = moveDirection;

			if(timeToMoveCounter < 0f){
				playerMoving = false;
				timeBetweenMoveCounter = timeBetweenMove;
			}
		}
		else{
			timeBetweenMoveCounter -= Time.deltaTime;
			cpuRigidBody.velocity = Vector2.zero;
			if(timeBetweenMoveCounter < 0.0f){
				playerMoving = true;
				timeToMoveCounter = timeToMove;

				moveDirection = new Vector3 (UnityEngine.Random.Range (-1f, 1f) * playerSpeed, UnityEngine.Random.Range (-1f, 1f) * playerSpeed, 0f);
			}
		}
	}

	public void CrazyRandomMovement(){

		//choose a random direction
		int direction = CoinFlip(); //pick horizontal or vertical movement

		//vertical
		if(direction == 0){
			//move a small random amount in that direction, random positive or negative

			MoveDistance(0.0f, 0.1f * DirectionChange());
			playerMoving = true;
		}

		//horizontal
		if(direction == 1){
			//move a small random amount in that direction, random positive or negative
			MoveDistance(0.1f * DirectionChange(), 0.0f);
			playerMoving = true;
		}

	}

	public void GetCurrentMapState(){

		//_gameMap = GameObject.Find ("MapController").GetComponent<MapController> ()._gameMap;
		FindAllBoulders();
		FindAllElementBoxes ();

	}
		

	//TODO fix this, as of right now when there are a lot of boulders this checks every boulder for distance, VERY SLOW
	public GameObject ChooseClosest(GameObject[] arrayOfObjects){

		Vector3 currentPosition = transform.position;

		float shortestDistance = float.MaxValue;

		GameObject closest = null;

		foreach(GameObject obj in arrayOfObjects){

			float difference = Vector3.Distance(currentPosition, obj.transform.position);
			if(difference < shortestDistance){
				shortestDistance = difference;
				closest = obj;
			}

		}
		return closest;

	}

	public void FindAllGameItems(){
		
	}

	public void FindAllBoulders(){
		_availableBoulders = GameObject.FindGameObjectsWithTag("Boulder");
	}

	public void FindAllItems(){

	}

	public void FindAllElementBoxes(){
		_availableElementBoxes = GameObject.FindGameObjectsWithTag ("ElementBox");
	}

	public void FindAllCodeNuggets(){
		
	}
		
	public int CoinFlip(){
		return rand.Next (2);
	}

	public int DirectionChange(){
		int direction = CoinFlip ();
		if(direction == 0){
			return -1;
		}
		else{
			return 1;
		}
	}
}
