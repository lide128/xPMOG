﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;

public class PlayerController : MonoBehaviour {

	public string playerIdentifier;

	public Team playerTeam;
	public string teamColor;

	public float playerSpeed;

	public Renderer rend;

	private Rigidbody2D playerRigidBody;
	private BoxCollider2D playerCollider2d;

	public InventoryManager inventory; //inventory cannot be InventoryManager class because it is a monobehaviour

	public bool playerMoving;

	public UIMessageHandler messages;

	// Use this for initialization
	public virtual void Start () {
		rend = GetComponent<SpriteRenderer> ();
		playerRigidBody = GetComponent<Rigidbody2D> ();
		playerCollider2d = GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	public virtual void Update () {

		//TODO move this to its own function 
		//TODO restrict player movement, to tiles? to perpedicular axis? to movement inside tiles? to a set increment of distance?
		playerMoving = false;

		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f){
			playerRigidBody.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * playerSpeed, playerRigidBody.velocity.y);
			playerMoving = true;
		}
		if(Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f){
			playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, Input.GetAxisRaw ("Vertical") * playerSpeed);
			playerMoving = true;
		}

		if(Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f){
			playerRigidBody.velocity = new Vector2 (0f, playerRigidBody.velocity.y);
		}
		if(Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f){
			playerRigidBody.velocity = new Vector2 (playerRigidBody.velocity.x, 0f);
		}
	}

	void OnCollisionEnter2D(Collision2D collision2d){
		Debug.Log ("player collided with: " + collision2d.gameObject.name);

		//this is here in order to ignore collisions with the box colliders of the ground tiles
		if(collision2d.gameObject.layer == 8){
			Physics2D.IgnoreLayerCollision (8, 22, true);
		}

		//if player collides with ElementBox, attempt to add ElementContainer to player's element inventory
		if(collision2d.gameObject.layer == 25){ 

			//trying the separate inventory class here
			ElementContainer temp = collision2d.gameObject.GetComponent<ElementBox> ().container;
			bool elementsAdded = inventory.GetComponent<InventoryManager> ().playerInventory.AddElementContainerToInventory (temp);
			if(elementsAdded){
				Destroy (collision2d.gameObject); //remove the ElementBox after contents absorbed
				UpdateElementBoxCount(-1);
			}
			else if(!elementsAdded){
				//if we can't add all try adding part of the container
				inventory.GetComponent<InventoryManager> ().playerInventory.PartialElementAdd (temp);
				//don't destroy the container
			}
			else{
				//if unable to add elements, display message
				//this should be an in game UI message
				messages.CreateMessage ("Could not add " + temp.volume + " " + temp.contents.name + " to player inventory, because amount exceeds carrying capacity.", playerIdentifier);
			}
		}
	}

	public void UpdateElementBoxCount(int amount){
		GameObject mapCont = GameObject.Find ("MapController");
		mapCont.GetComponent<MapController> ().onScreenElementBoxCount += amount;
	}

	void OnTriggerEnter2D(Collider2D other){

		//TODO limit which base a player can drop off at 

		//if players enters base collision box, give all elements to the base
		if(other.gameObject.layer == 9) { //layer 9 is the layer for bases
			
			//If it's the base, bump the character up a couple of pixels to simulate going over a bump
			Vector3 currentPos = transform.position;
			currentPos.y += 0.03f;
			transform.position = currentPos;

			string touchedBaseTeam = other.gameObject.GetComponent<BaseController> ().baseColor;

			if(touchedBaseTeam.Equals(teamColor)){
				//Handle the depositing of items, giving all of them automatically right now
				//This is the base adding all of a copy of the players elements to it's inventory
				//throws and exception here upon placement of the character at start on base, presumably because everything is empty
				other.gameObject.GetComponent<BaseController> ().baseInventory.AddListOfElementContainer (
					inventory.playerInventory.elementsInventory);
				other.gameObject.GetComponent<BaseController> ().baseInventory.elementsUpdated = true;
				inventory.playerInventory.ClearInventory ();
			}
			else{
				messages.CreateMessage ("Not your team's base.", playerIdentifier);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){

		if(other.gameObject.layer == 9){
			//Move the player back down from the bump
			Vector3 currentPos = transform.position;
			currentPos.y -= 0.03f;
			transform.position = currentPos;
		}
	}

	void OnMouseOver(){
		rend.material.color = new Color(0.95f, 0, 0, 0.8f);
	}

	void OnMouseExit(){
		rend.material.color = Color.white; //restores tile color
	}
		
}
