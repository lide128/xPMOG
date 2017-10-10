using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TileController : MonoBehaviour {

	public Renderer rend;

	public bool minedBoulder;
	public bool hasBuriedItems;
	public bool hasBuriedElements;
	public bool hasBuriedCodeNuggets;

	public int elementFrequency; //scale of 1 to 100, 100 being alot of elements?
	private int groundTilePrefabCount = 5; //the number of different ground tile prefabs available

	private string tileName;
	private string boulder = "BoulderTile";
    private string buffer = "BufferTile";
    private string border = "BorderTile";
    private string ground = "GroundTile";
	private string baseTile = "Base";
	private string prefabsPath = "Prefabs/";

	private List<Vector2> itemSpawnPoints;

	public TileItemManager tileItems;

	public Vector2 tileCoordinates; //this is the position data of the game screen

	public UIMessageHandler messages;

	// Use this for initialization
	public virtual void Start () {
       
		rend = GetComponent<SpriteRenderer> ();
		tileName = gameObject.name;
		minedBoulder = false;
		hasBuriedItems = false;
		hasBuriedElements = false;
		hasBuriedCodeNuggets = false;

		if(tileName.Contains(boulder)){
			hasBuriedElements = true;
		}

		if(hasBuriedItems || hasBuriedElements || hasBuriedCodeNuggets){

			//because this tile has items, elements, or code nuggets, create the tileItem object
			tileItems = new TileItemManager();
			//and get the item spawn point list
			itemSpawnPoints = tileItems.tileItemSpawnPoints;

			if(hasBuriedItems){
				Debug.Log ("Added items to tile");
			}
			if (hasBuriedElements) {
				AddElementsToTile ();
			}
			if(hasBuriedCodeNuggets){
				Debug.Log ("Added code nuggets to tile");
			}
		}
	}

	public virtual void Update() {

		if(tileName.Contains(boulder) && minedBoulder){ //this handles the self destruction of a boulder tile and creation of a tile to replace it
			List<ElementContainer> temp = new List<ElementContainer> (); //buffer to copy the tile element contents into
			temp = tileItems.buriedElements;

			Destroy (gameObject); //destroy the boulder tile
			Debug.Log ("Destroyed the boulder tile!");

			CreateRandomGroundTile();

			if(hasBuriedElements){

				ReleaseBoulderElements (temp, itemSpawnPoints);

			}
		}
	}
		
	public void SetCoordinates(Vector2 newCoords){
		tileCoordinates = newCoords;
	}

	void OnDestroy(){
		//this leaves things in the game scene and is not cleaned upon end
	}

	public virtual void OnCollisionEnter2D(Collision2D collision){
		Debug.Log ("tile collision occurred");

		//TODO this needs to be changed, mining a boulder will be more of a process than just running in to it
		//checks if the player collided with a boulder tile and if so sets the mined boulder conidtion to true, which will destroy the boulder
		if(tileName.Contains(boulder) && !minedBoulder && collision.gameObject.name.Equals("Player")){
			Debug.Log("tile collided with: " + collision.gameObject.name);
			minedBoulder = true;
		}
	}

	void OnMouseEnter(){

	}

	void OnMouseOver(){
//		Debug.Log ("Mouse over");
		if(tileName.Contains(ground) || tileName.Contains(boulder)){ //only highlight ground or boulder tiles
			rend.material.color = new Color(0.95f, 0, 0, 0.8f);

		}
	}

	void OnMouseExit(){
		rend.material.color = Color.white; //restores tile color
	}

	//This adds a random number of random elements of random quantities to the tile... randomly.
	public void AddElementsToTile(){
		//find the gameobject containing all the in game items
		GameObject inGameItems = GameObject.FindWithTag ("All Game Items");
		int i;
		for(i = 0; i < elementFrequency; i++){
			tileItems.AddElementContainerToTile (inGameItems.GetComponent<GameItems>().allGameItems.getRandomVolumeOfDispersedElement());
		}
		Debug.Log ("Added " + i + " elements to the tile.");
	}
		
	//Randomly chooses one of the ground tile prefabs to create
	void CreateRandomGroundTile(){
		System.Random rand = new System.Random ();
		int nextRandom = rand.Next (groundTilePrefabCount) + 1; //add one because rand.Next is from 0 to (x) exclusive
		string toLoad = ground + nextRandom;
		GameObject newTile = (GameObject)Instantiate (Resources.Load (prefabsPath + toLoad), 
			new Vector3(transform.position.x, transform.position.y, 0.0f), transform.rotation);//instantiate new ground tile
	}

	//Remove all elements from boulder tile and into ElementBoxes created on the ground.
	//Does not duplicate items anymore
	void ReleaseBoulderElements(List<ElementContainer> boulderContents, List<Vector2> spawnPoints){
		int contentsCount = boulderContents.Count;
		System.Random rand = new System.Random ();
		List<Vector2> chosenPoints = tileItems.ItemDispersement (contentsCount, spawnPoints); //list of randomly chosen spawn points

		foreach(ElementContainer ele in boulderContents){
			string nameOfBoxPrefab = prefabsPath + ele.contents.name + "Box";
			GameObject newBox = (GameObject)Instantiate(Resources.Load(nameOfBoxPrefab));

			Transform tilePos = gameObject.transform; //the position in coordinate space of the current tile

			int removeIndex = rand.Next(chosenPoints.Count); //randomly choose spawn point
			Vector2 newItemPos = chosenPoints[removeIndex];
			chosenPoints.RemoveAt (removeIndex); //remove used point so no overlap

			float newX = tilePos.position.x + newItemPos.x;
			float newY = tilePos.position.y + newItemPos.y;
			newBox.transform.Translate (new Vector3 (newX, newY, 0.0f)); //move the box to the item spawn point
			newBox.transform.localScale = new Vector3(0.4f, 0.4f, 0.0f); //scale down the element boxes by a 4th

			newBox.GetComponent<ElementBox>().container = ele; //transfer tile single tile element container to new box

		}
	}

}
