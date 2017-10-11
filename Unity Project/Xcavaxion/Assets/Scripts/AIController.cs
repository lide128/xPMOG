using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PlayerController {

	public int difficultyLevel; // 1 to 5? easiest to hardest? what does it mean?

	private Rigidbody2D cpuRigidBody;
	private BoxCollider2D cpuCollider2d;

	System.Random rand = new System.Random();

	// Use this for initialization
	public override void Start () {

		base.Start (); //run the PlayerController start function
		cpuRigidBody = GetComponent<Rigidbody2D> ();
		cpuCollider2d = GetComponent<BoxCollider2D> ();
		//start the AI player on one of the not occupied bases
	}
	
	// Update is called once per frame
	public override void Update () {

//		RandomMapMovement ();
	}

	public void FixedUpdate (){

		RandomMapMovement ();
	}

	public bool CheckTimePassed(float timeToWait){
		float delta = 0.0f;
		while(timeToWait >= delta){
			delta += Time.deltaTime;
		}
		return true;
	} 

	public void MoveDistance(float amountX, float amountY){

		Vector2 newPosition = new Vector2 (transform.position.x + amountX, transform.position.y + amountY);
		cpuRigidBody.MovePosition (newPosition + cpuRigidBody.velocity * Time.fixedDeltaTime);
	}

	public void RandomMapMovement(){

		//choose a random direction
		int direction = CoinFlip(); //pick horizontal or vertical movement

		//vertical
		if(direction == 0){
			//move a small random amount in that direction, random positive or negative

//			cpuRigidBody.velocity = new Vector2(DirectionChange() * playerSpeed + 0.5f, cpuRigidBody.velocity.y);
			MoveDistance(0.0f, 0.1f * DirectionChange());
			playerMoving = true;
		}

		//horizontal
		if(direction == 1){
			//move a small random amount in that direction, random positive or negative
//			cpuRigidBody.velocity = new Vector2(DirectionChange() * playerSpeed + 0.5f, cpuRigidBody.velocity.x);
			MoveDistance(0.1f * DirectionChange(), 0.0f);
			playerMoving = true;
		}

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
