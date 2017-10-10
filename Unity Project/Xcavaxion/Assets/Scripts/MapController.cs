using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapController : MonoBehaviour {

	public List<GameObject> TileTypes; //the set of tile prefabs to be used to make the map, must be added to the map controller game object in the unity inspector

    public int numberOfGroundTiles; 	//currently 5 different playable area ground tiles
    public int numberOfBufferTiles; 	//currently 6 different buffer ground tiles
	public int numberOfBoulderTiles; 	//currently 2 different boulders
	public int numberOfEdgeTiles; 		//currently 3 different tiles for each edge

	private Vector2 MapSize; //full tile map size, including border
	public Vector2 PlayableMapSize; //just the playable tiles size
	public Vector2 origin;
	public int screenBuffer;
	public float tileScale;

	public int chanceOfBoulderTile; // 0 to 100 percent

	public int onScreenBoulderCount;
	public int onScreenElementBoxCount;

	private string borderTile = "BorderTile";
	private string bufferTile = "BufferTile";
	private string boulderTile = "BoulderTile";
	private string groundTile = "GroundTile";
	private string genericBase = "GenericBase";

	private string northEdge = "northEdgeTile";
	private string southEdge = "southEdgeTile";
	private string westEdge = "westEdgeTile";
	private string eastEdge = "eastEdgeTile";

	private string NWCorner = "NWCornerTile";
	private string NECorner = "NECornerTile";
	private string SWCorner = "SWCornerTile";
	private string SECorner = "SECornerTile";

	private string NWBase = "NWBase";
	private string NEBase = "NEBase";
	private string SWBase = "SWBase";
	private string SEBase = "SEBase";

	public int elementFrequency;

	private GameObject[,] _gameMap;
	private char[,] _charMap;

	private TileController tileCont;

	System.Random rand = new System.Random();

	// Use this for initialization
	public void Start () {
		onScreenBoulderCount = 0;
		onScreenElementBoxCount = 0;

		_gameMap = CreateMap();
        
		CreateOnGroundObjects();

        DrawMap();
	}
		
	private GameObject[,] CreateMap(){

		GameObject border = FindGameObjectWithName (borderTile);
		GameObject boulder = FindGameObjectWithName (boulderTile + "1");

		MapSize.x = PlayableMapSize.x + (screenBuffer * 2);
		MapSize.y = PlayableMapSize.y + (screenBuffer * 2);

        GameObject[,] _map = new GameObject[(int) MapSize.x, (int) MapSize.y];

		for(var y = 0; y <  MapSize.y - 1; y++){
			for(var x = 0; x < MapSize.x - 1; x++){
				
				//check if we are on a buffer tile
				if(WithinBuffer(x, y)){
                    int randomBuffer = rand.Next(numberOfBufferTiles);
                    GameObject toUse = FindGameObjectWithName(bufferTile + (randomBuffer + 1));
					_map [x, y] = toUse;
					continue;
				}
				//check if we are on an edge tile
				if(OnBorder(x, y)){
					string axis = DetermineBufferTileAxis (x, y); //check which border axis we are on
					GameObject toUse = FindGameObjectWithName (GeneratePrefabName (axis, numberOfEdgeTiles)); //then get the appropriate tile
					_map [x, y] = toUse; //assign to the 2d array
					continue;
				}
				//check if we are on a corner tile
				string cornerIdentity = CheckBorderCorners(x, y);
				if (cornerIdentity != ""){
					GameObject toUse = FindGameObjectWithName(cornerIdentity);
                    _map [x, y] = toUse;
                    continue;
                }
					
                else
                {
                    //Pick one of the ground tiles to display randomly for the playable game area
					GameObject toUse = FindGameObjectWithName(GeneratePrefabName(groundTile, numberOfBoulderTiles));
                    _map[x, y] = toUse;
                }
			}
		}
		return _map;
	}

	private void DrawMap(){
		Vector2 tileCoords;
		for (var y = 0; y < MapSize.y - 1; y++) {
			for (var x = 0; x < MapSize.x - 1; x++) {
				//move the position based on tile scale
				var transformX = x * tileScale;
				var transformY = y * tileScale;

				//change depending on origin
				transformX += origin.x;
				transformY += origin.y;

				//set tile coords to send to tile
				tileCoords.x = transformX;
				tileCoords.y = transformY;

				var temp = Instantiate (_gameMap [x, y]) as GameObject;
				temp.transform.position = new Vector3 (transformX, transformY, 0.0f);

				//give the tile information about it's position on the screen
				tileCont = temp.GetComponent<TileController> ();
				tileCont.SetCoordinates (tileCoords);
				tileCont.elementFrequency = elementFrequency;
			}
		}
	}

	public string GeneratePrefabName(string tileName, int numTiles){
		int randomTileNum = rand.Next (numTiles);
		string nameToReturn = tileName + (randomTileNum + 1);
		return nameToReturn;
	}

	//Get the original 2d array map indicies for the tile, map does include buffer
	private Vector2 ScreenCoordsToMapCoords(TileController tile){
		Vector2 mapCoords;
		mapCoords.x = (tile.tileCoordinates.x - origin.x) / tileScale;
		mapCoords.y = (tile.tileCoordinates.y - origin.y) / tileScale;
		return mapCoords;
	}

	private GameObject FindGameObjectWithName(string toFindName){
		foreach(GameObject o in TileTypes){
			String currentName = o.name;
			if (currentName.Equals (toFindName))
				return o;
		}
		return null;
	}

	//this will determine the corner base locations on the playable map
	private string CheckBaseCorners(int xCoord, int yCoord){
		string returnBase = "";
		//check NW base
		if((xCoord == screenBuffer) && (yCoord == PlayableMapSize.y + (screenBuffer - 1))){
			returnBase = NWBase;
			Debug.Log ("northwest base identified");
		}
		//check NE base
		if((xCoord == PlayableMapSize.x + (screenBuffer - 1)) && (yCoord == PlayableMapSize.y + (screenBuffer - 1))){
			returnBase = NEBase;
			Debug.Log ("northeast base identified");
		}
		//check SW base
		if((xCoord == screenBuffer) && (yCoord == screenBuffer)){
			returnBase = SWBase;
			Debug.Log ("southwest base identified");
		}
		//check SE base
		if((xCoord == PlayableMapSize.x + (screenBuffer - 1)) && (yCoord == screenBuffer)){
			returnBase = SEBase;
			Debug.Log ("southeast base identified");
		}
		return returnBase;
	}

	//detemines if a given position in the map array is within the buffer
	private bool WithinBuffer(int xCoord, int yCoord){ 
		return (xCoord < screenBuffer - 1 || xCoord > (screenBuffer) + PlayableMapSize.x) ||
		(yCoord < screenBuffer - 1 || yCoord > (screenBuffer) + PlayableMapSize.y);
	}
		
	//returns a string corresponding to which axis the tile may be in order to correctly place border tiles
	private string DetermineBufferTileAxis(int xCoord, int yCoord){
		string returnAxis = ""; //if no match return empty string
		//check north axis
		if(((xCoord >= screenBuffer) || (xCoord < (PlayableMapSize.x + screenBuffer))) && (yCoord == PlayableMapSize.y + screenBuffer)){
			returnAxis = northEdge;
		}
		//check south axis
		if(((xCoord >= screenBuffer) || (xCoord < (PlayableMapSize.x + screenBuffer))) && (yCoord == screenBuffer - 1)){
			returnAxis = southEdge;
		}
		//check west axis
		if((xCoord == screenBuffer - 1) && ((yCoord >= screenBuffer) || (yCoord < (PlayableMapSize.y + screenBuffer)))){
			returnAxis = westEdge;
		}
		//check east axis
		if((xCoord == PlayableMapSize.x + screenBuffer) && ((yCoord >= screenBuffer) || (yCoord < (PlayableMapSize.y + screenBuffer)))){
			returnAxis = eastEdge;
		}

		return returnAxis;
	}

	//determines if a position in the map array requires a border tile, if it's within the border
	private bool OnBorder(int xCoord, int yCoord){
		return (xCoord == (screenBuffer - 1) || xCoord == (screenBuffer + PlayableMapSize.x)) && (yCoord > (screenBuffer - 1) && (yCoord < (screenBuffer + PlayableMapSize.y))) ||
			(yCoord == (screenBuffer - 1) || yCoord == (screenBuffer + PlayableMapSize.y)) && (xCoord > (screenBuffer - 1) && (xCoord < (screenBuffer + PlayableMapSize.x)));
	}

	//determines the array position of border corners and which corner they are
	private string CheckBorderCorners(int xCoord, int yCoord){
		string returnCorner = ""; //if no match return empty string
		//check NW corner
		if((xCoord == screenBuffer - 1) && (yCoord == MapSize.y - screenBuffer)){
			returnCorner = NWCorner;
		}
		//check NE corner
		if((xCoord == MapSize.x - screenBuffer) && (yCoord == MapSize.y - screenBuffer)){
			returnCorner = NECorner;
		}
		//check SW corner
		if((xCoord == screenBuffer -1) && (yCoord == screenBuffer - 1)){
			returnCorner = SWCorner;
		}
		//check SE corner
		if((xCoord == MapSize.x - screenBuffer) && (yCoord == screenBuffer - 1)){
			returnCorner = SECorner;
		}
		return returnCorner;
	}

	//after the the full map is created in memory, with buffer, borders, and ground
	//this is where we add the stuff that is on the ground
	//like boulders, bases, or anyother type of structures
	//double for loop on playable map size
	private void CreateOnGroundObjects(){
		GameObject baseTile = FindGameObjectWithName (genericBase);
		bool alreadyCovered = false;
		System.Random rand = new System.Random ();

		for(var y = screenBuffer; y <  PlayableMapSize.y + screenBuffer; y++){
			for (var x = screenBuffer; x < PlayableMapSize.x + screenBuffer; x++) {
				int newNum = rand.Next (1, 101);

				//check for bases, placed at corners with generic base for now
				if(CheckBaseCorners(x, y) != ""){
					_gameMap [x, y] = baseTile; //temp
					alreadyCovered = true;
				}
				//fill map with boulders based on chance of boulder percent
				else if(BoulderChance(newNum)){
					GameObject boulder = FindGameObjectWithName (GeneratePrefabName(boulderTile, numberOfBoulderTiles));
					_gameMap [x, y] = boulder;
					onScreenBoulderCount++;
				}
				alreadyCovered = false;
			}
		}
	}

	public bool BoulderChance(int randoNum){
		return randoNum <= chanceOfBoulderTile;
	}
				
	// Update is called once per frame
	void Update () {

	}


}
