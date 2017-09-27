using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;

public class PlayerController : MonoBehaviour {

	public float playerSpeed;

	private Rigidbody2D playerRigidBody;
	private BoxCollider2D playerCollider2d;

	public GameObject inventory;

	public bool playerMoving;

	// Use this for initialization
	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D> ();
		playerCollider2d = GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	void Update () {

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
			ElementContainer temp = collision2d.gameObject.GetComponent<ElementBox> ().container;
			bool elementsAdded = inventory.GetComponent<InventoryManager> ().AddElementContainerToInventory (temp);
			if(elementsAdded){
				Destroy (collision2d.gameObject); //remove the ElementBox after contents absorbed
			}
			else if(!elementsAdded){
				//if we can't add all try adding part of the contain
				inventory.GetComponent<InventoryManager> ().PartialElementAdd (temp);
				//don't destroy the container
			}
			else{
				//if unable to add elements, display message
				//this should be an in game UI message
				Debug.Log("Could not add " + temp.volume + " " + temp.contents.name + " to player inventory, because amount exceeds carrying capacity.");
			}
		}

	}
		
}
