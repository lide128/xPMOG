using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.Xml.Schema;
using UnityEngine.WSA;

public class MapController : MonoBehaviour {

	public List<GameObject> TileTypes;
    public int numberOfGroundTiles;
    public int numberOfBufferTiles;
	private Vector2 MapSize;
	public Vector2 PlayableMapSize;
	public Vector2 origin;
	public int screenBuffer;
	public float tileScale;

	private GameObject[,] _gameMap;
	private char boulderChar = 'X';
	private char[,] _charMap;


	// Use this for initialization
	public void Start () {
		_gameMap = CreateMap();
        //UpdateBoulders();
        DrawMap();
	}


	private GameObject[,] CreateMap(){

	
		GameObject border = FindGameObjectWithName (TileTypes, "BorderTile");
		GameObject boulder = FindGameObjectWithName (TileTypes, "BoulderTile");


		MapSize.x = PlayableMapSize.x + (screenBuffer * 2);
		MapSize.y = PlayableMapSize.y + (screenBuffer * 2);

        System.Random rand = new System.Random();

        GameObject[,] _map = new GameObject[(int) MapSize.x, (int) MapSize.y];

		for(var y = 0; y <  MapSize.y - 1; y++){
			for(var x = 0; x < MapSize.x - 1; x++){
				//check buffer tiles
				if(WithinBuffer(x, y)){
                    int randomBuffer = rand.Next(numberOfBufferTiles);
                    //if (randomBuffer == 3) rand.Next(numberOfBufferTiles);
                    GameObject toUse = FindGameObjectWithName(TileTypes, "BufferTile" + (randomBuffer + 1));
					_map [x, y] = toUse;
					continue;
				}
				//check border tiles
				if(OnBorder(x, y)){
					_map [x, y] = border;
					continue;
				}
                if (CheckBorderCorners(x, y))
                {
                    _map[x, y] = boulder; //temp
                    continue;
                }

                else
                {
                    //Pick one of the ground tiles to display randomly
                    int randomGround = rand.Next(numberOfGroundTiles);
                    Debug.Log("random choice: " + randomGround);
                    GameObject toUse = FindGameObjectWithName(TileTypes, "GroundTile" + (randomGround+1));
                    Debug.Log("tile choice: " + toUse.name);
                    _map[x, y] = toUse;
                }
			}
		}

		return _map;
	}

	private void DrawMap(){

		for (var y = 0; y < MapSize.y - 1; y++) {
			for (var x = 0; x < MapSize.x - 1; x++) {
				//move the position based on tile scale
				var transformX = x * tileScale;
				var transformY = y * tileScale;

				//change depending on origin
				transformX += origin.x;
				transformY += origin.y;
				var temp = Instantiate (_gameMap [x, y]) as GameObject;
				temp.transform.position = new Vector3 (transformX, transformY, 0f);
			}
		}
	}

	private GameObject FindGameObjectWithName(List<GameObject> listOfGameObjects, String toFindName){
		foreach(GameObject o in listOfGameObjects){
			String currentName = o.name;
			if (currentName.Equals (toFindName))
				return o;
		}
		return null;
	}

	private bool WithinBuffer(int xCoord, int yCoord){ 
		return (xCoord < screenBuffer - 1 || xCoord > (screenBuffer - 1) + PlayableMapSize.x) ||
		(yCoord < screenBuffer - 1 || yCoord > (screenBuffer - 1) + PlayableMapSize.y);
	}

	private bool OnBorder(int xCoord, int yCoord){
		return (xCoord == (screenBuffer - 1) || xCoord == (screenBuffer - 1 + PlayableMapSize.x)) && (yCoord > (screenBuffer - 1) && (yCoord < (screenBuffer - 1) + PlayableMapSize.y)) ||
		(yCoord == (screenBuffer - 1) || yCoord == (screenBuffer - 1 + PlayableMapSize.y)) && (xCoord > (screenBuffer - 1) && (xCoord < (screenBuffer - 1) + PlayableMapSize.x));
	}

	private bool CheckBorderCorners(int xCoord, int yCoord){
		return (xCoord == screenBuffer - 1 && yCoord == screenBuffer - 1) ||
		(xCoord == (PlayableMapSize.x + screenBuffer)-1 && yCoord == (PlayableMapSize.y + screenBuffer)-1) ||
		(xCoord == screenBuffer - 1 && yCoord == (PlayableMapSize.y + screenBuffer)-1) ||
		(xCoord == (PlayableMapSize.x + screenBuffer)-1 && yCoord == screenBuffer - 1);
	}

	private void UpdateBoulders(){

		GameObject boulder = FindGameObjectWithName (TileTypes, "Boulder");
		//update _gameMap state
		for(var y = 0; y <  PlayableMapSize.y - 1; y++){
			for (var x = 0; x < PlayableMapSize.x - 1; x++) {
				if(x == 3 && y == 3){
					_gameMap [x + screenBuffer, y + screenBuffer] = boulder;
				}
			}
		}

		//draw boulders

	}

	// Update is called once per frame
	void Update () {
//		UpdateBoulders (_charMap);
	}
}
