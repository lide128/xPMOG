using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using System;

public class PlayerController : MonoBehaviour {

	public float playerSpeed;

	private Rigidbody2D playerRigidBody;
	private BoxCollider2D playerCollider2d;

	public bool playerMoving;

	// Use this for initialization
	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D> ();
		playerCollider2d = GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	void Update () {

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
		Debug.Log ("collided with: " + collision2d.gameObject.name);
		if(collision2d.gameObject.layer == 8){
			Physics2D.IgnoreLayerCollision (8, 9, true);
		}
	}
		
}
