package environment;

import java.awt.Point;
import system.GameObjectList;

/**
 * A class representing a square element of space that can be occupied with 0..n elements or objects
 * @author Alex White
 *
 */
public class Tile {
	
	char symbol = '-';
	GameObjectList gameObjects;
	TileCover cover;
	Point coordinates;
	int value;
	boolean playerOccupied;
	boolean excavated;
	boolean base;
	
	public Tile(Point assignedCoords) {
		
		gameObjects = new GameObjectList();
		coordinates = assignedCoords;
		cover = new TileCover();
		playerOccupied = false;
		excavated = false;
		base = false;
		
	}
	
	/**
	 * Removes the cover of the tile, flips excavated to true, makes the reference to the Tile Cover null
	 * @return the list of GameObjects contained in the Tile Cover
	 */
	public GameObjectList removeCover() {
		excavated = true;
		cover = null;
		return cover.gameObjects;
	}
	
	public boolean isExcavated() {
		return excavated == true && cover == null;
	}
	
	public char getSymbol() {
		if(isExcavated()) return symbol;
		return cover.symbol;
	}
	

}
