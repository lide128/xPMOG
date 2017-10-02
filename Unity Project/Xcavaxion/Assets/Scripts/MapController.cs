using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

	public List<GameObject> TileTypes;

    public int numberOfGroundTiles;
    public int numberOfBufferTiles;
	public int numberOfBoulderTiles;
	public int numberOfBorderTiles;

	private Vector2 MapSize; //full tile map size, including border
	public Vector2 PlayableMapSize; //just the playable tiles size
	public Vector2 origin;
	public int screenBuffer;
	public float tileScale;

	public int chanceOfBoulderTile;

	private string borderTile = "BorderTile";
	private string bufferTile = "BufferTile";
	private string boulderTile = "BoulderTile";
	private string groundTile = "GroundTile";
	private string genericBase = "GenericBase";

	private string northAxis = "northAxis";
	private string southAxis = "southAxis";
	private string westAxis = "westAxis";
	private string eastAxis = "eastAxis";

	private string NWCorner = "NWCorner";
	private string NECorner = "NECorner";
	private string SWCorner = "SWCorner";
	private string SECorner = "SECorner";

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
		
		_gameMap = CreateMap();
        
//		CreateBoulders ();
		CreateOnGroundObjects();

        DrawMap();

	}


	private GameObject[,] CreateMap(){

		GameObject border = FindGameObjectWithName (TileTypes, borderTile);
		GameObject boulder = FindGameObjectWithName (TileTypes, boulderTile);

		MapSize.x = PlayableMapSize.x + (screenBuffer * 2);
		MapSize.y = PlayableMapSize.y + (screenBuffer * 2);

        //System.Random rand = new System.Random();

        GameObject[,] _map = new GameObject[(int) MapSize.x, (int) MapSize.y];


		for(var y = 0; y <  MapSize.y - 1; y++){
			for(var x = 0; x < MapSize.x - 1; x++){
				
				//check buffer tiles
				if(WithinBuffer(x, y)){
                    int randomBuffer = rand.Next(numberOfBufferTiles);
                    GameObject toUse = FindGameObjectWithName(TileTypes, bufferTile + (randomBuffer + 1));
					_map [x, y] = toUse;
					continue;
				}
				//check border tiles
				if(OnBorder(x, y)){
					_map [x, y] = border;
					continue;
				}
				//check border corners
                if (CheckBorderCorners(x, y) != ""){
                    _map [x, y] = boulder; //temp
                    continue;
                }


                else
                {
                    //Pick one of the ground tiles to display randomly
                    int randomGround = rand.Next(numberOfGroundTiles);
                    GameObject toUse = FindGameObjectWithName(TileTypes, groundTile + (randomGround+1));
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
				temp.transform.position = new Vector3 (transformX, transformY, 0f);

				//give the tile information about it's position on the screen
				tileCont = temp.GetComponent<TileController> ();
				tileCont.SetCoordinates (tileCoords);
				tileCont.elementFrequency = elementFrequency;
			}
		}
	}

	//Get the original 2d array map indicies for the tile, map does include buffer
	private Vector2 ScreenCoordsToMapCoords(TileController tile){
		Vector2 mapCoords;
		mapCoords.x = (tile.tileCoordinates.x - origin.x) / tileScale;
		mapCoords.y = (tile.tileCoordinates.y - origin.y) / tileScale;
		return mapCoords;
	}

	private GameObject FindGameObjectWithName(List<GameObject> listOfGameObjects, String toFindName){
		foreach(GameObject o in listOfGameObjects){
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
		if((xCoord == screenBuffer) && (yCoord == PlayableMapSize.y + screenBuffer - 2)){
			returnBase = NWBase;
		}
		//check NE base
		if((xCoord == PlayableMapSize.x + screenBuffer - 2) && (yCoord == PlayableMapSize.y + screenBuffer - 2)){
			returnBase = NEBase;
		}
		//check SW base
		if((xCoord == screenBuffer) && (yCoord == screenBuffer)){
			returnBase = SWBase;
		}
		//check SE base
		if((xCoord == PlayableMapSize.x + screenBuffer - 2) && (yCoord == screenBuffer)){
			returnBase = SEBase;
		}
		return returnBase;
	}

	//detemines if a given position in the map array is within the buffer
	private bool WithinBuffer(int xCoord, int yCoord){ 
		return (xCoord < screenBuffer - 1 || xCoord > (screenBuffer - 1) + PlayableMapSize.x) ||
		(yCoord < screenBuffer - 1 || yCoord > (screenBuffer - 1) + PlayableMapSize.y);
	}
		
	//returns a string corresponding to which axis the tile may be in order to correctly place border tiles
	private string DetermineBufferTileAxis(int xCoord, int yCoord){
		string returnAxis = ""; //if no match return empty string
		//check north axis
		if(((xCoord >= screenBuffer) || (xCoord < (MapSize.x - screenBuffer))) && (yCoord == MapSize.y - screenBuffer)){
			returnAxis = northAxis;
		}
		//check south axis
		if(((xCoord >= screenBuffer) || (xCoord < (MapSize.x - screenBuffer))) && (yCoord == screenBuffer - 1)){
			returnAxis = southAxis;
		}
		//check west axis
		if((xCoord == screenBuffer - 1) && ((yCoord >= screenBuffer) || (yCoord < (MapSize.y - screenBuffer)))){
			returnAxis = westAxis;
		}
		//check east axis
		if((xCoord == MapSize.x - screenBuffer) && ((yCoord >= screenBuffer) || (yCoord < (MapSize.y - screenBuffer)))){
			returnAxis = eastAxis;
		}

		return returnAxis;
	}

	//determines if a position in the map array requires a border tile, if it's within the border
	private bool OnBorder(int xCoord, int yCoord){
		return (xCoord == (screenBuffer - 1) || xCoord == (screenBuffer - 1 + PlayableMapSize.x)) && (yCoord > (screenBuffer - 1) && (yCoord < (screenBuffer - 1) + PlayableMapSize.y)) ||
		(yCoord == (screenBuffer - 1) || yCoord == (screenBuffer - 1 + PlayableMapSize.y)) && (xCoord > (screenBuffer - 1) && (xCoord < (screenBuffer - 1) + PlayableMapSize.x));
	}

//	//determines if an array postion is going to be one of the maps corner tiles
//	private bool CheckBorderCorners(int xCoord, int yCoord){
//		return (xCoord == screenBuffer - 1 && yCoord == screenBuffer - 1) ||
//		(xCoord == (PlayableMapSize.x + screenBuffer)-1 && yCoord == (PlayableMapSize.y + screenBuffer)-1) ||
//		(xCoord == screenBuffer - 1 && yCoord == (PlayableMapSize.y + screenBuffer)-1) ||
//		(xCoord == (PlayableMapSize.x + screenBuffer)-1 && yCoord == screenBuffer - 1);
//	}

	//determines the array position of border corners and which corner they are
	private string CheckBorderCorners(int xCoord, int yCoord){
		string returnCorner = ""; //if no match return empty string
		//check NW corner
		if((xCoord == screenBuffer - 1) && (yCoord == MapSize.y - screenBuffer - 1)){
			returnCorner = NWCorner;
		}
		//check NE corner
		if((xCoord == MapSize.x - screenBuffer - 1) && (yCoord == MapSize.y - screenBuffer - 1)){
			returnCorner = NECorner;
		}
		//check SW corner
		if((xCoord == screenBuffer -1) && (yCoord == screenBuffer - 1)){
			returnCorner = SWCorner;
		}
		//check SE corner
		if((xCoord == MapSize.x - screenBuffer - 1) && (yCoord == screenBuffer - 1)){
			returnCorner = SECorner;
		}
		return returnCorner;
	}

	//after the the full map is created in memory, with buffer, borders, and ground
	//this is where we add the stuff that is on the ground
	//like boulders, bases, or anyother type of structures
	//double for loop on playable map size
	private void CreateOnGroundObjects(){
		GameObject baseTile = FindGameObjectWithName (TileTypes, genericBase);
		GameObject boulder = FindGameObjectWithName (TileTypes, boulderTile);

		for(var y = 0; y <  MapSize.y - 1; y++){
			for (var x = 0; x < MapSize.x - 1; x++) {
				if(x == 3 && y == 3){
					_gameMap [x + screenBuffer, y + screenBuffer] = boulder; //temp, make a boulder at 3, 3
				}
				if(x == 4 && y == 5){
					_gameMap [x + screenBuffer, y + screenBuffer] = boulder; //temp, make a boulder at 4, 5
				}

				//check for bases, placed at corners with generic base for now
				if(CheckBaseCorners(x, y) != ""){
					_gameMap [x, y] = baseTile; //temp
				}
			}
		}
	}

//	private void CreateBoulders(){
//		GameObject boulder = FindGameObjectWithName (TileTypes, boulderTile);
//		for(var y = 0; y <  PlayableMapSize.y - 1; y++){
//			for (var x = 0; x < PlayableMapSize.x - 1; x++) {
//				if(x == 3 && y == 3){
//					_gameMap [x + screenBuffer, y + screenBuffer] = boulder;
//				}
//			}
//		}
//	}
		
	// Update is called once per frame
	void Update () {

	}
}
