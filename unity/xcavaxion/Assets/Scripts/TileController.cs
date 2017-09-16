using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;

public class TileController : MonoBehaviour {

	public Renderer rend;
	public bool minedBoulder;

	private String objectName;

	private String boulder = "BoulderTile";
    private String buffer = "BufferTile";
    private String border = "BorderTile";
    private String ground = "GroundTile";

	// Use this for initialization
	void Start () {
        
		rend = GetComponent<SpriteRenderer> ();
		objectName = gameObject.name;
		minedBoulder = false;
        
	}

	void Update() {
		if(objectName.Contains(boulder) && minedBoulder){
			Destroy (gameObject);
		}
	}

	void OnCollision2DEnter(Collision2D collision){
		if(objectName.Contains(boulder) && !minedBoulder && collision.gameObject.name.Equals("Player")){
			Debug.Log("collided with: " + collision.gameObject.name);
			minedBoulder = true;
		}
	}
	
	void OnMouseEnter(){
		//Debug.Log ("Mouse enter");
		rend.material.color = Color.red;
	}

	void OnMouseOver(){
		Debug.Log ("Mouse over");
//		rend.material.color = Color.blue;
//		rend.material.color -= new Color (0.1F, 0, 0) * Time.deltaTime;
		rend.material.color = new Color(0.95f, 0, 0, 0.8f);
	}

	void OnMouseExit(){
		//Debug.Log ("Mouse exit");
		rend.material.color = Color.white;
	}


}
