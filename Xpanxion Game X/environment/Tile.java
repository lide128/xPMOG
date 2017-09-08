package environment;

import java.util.ArrayList;
import java.util.List;
import java.awt.Point;
import system.GameObject;
import system.GameElement;

/**
 * A class representing a square element of space that can be occupied with 0..n elements or objects
 * @author Alex White
 *
 */
public class Tile {
	
	List<GameObject> gameObjects;
	TileCover cover;
	Point coordinates;
	int value;
	boolean playerOccupied;
	boolean excavated;
	boolean base;
	
	public Tile(Point assignedCoords) {
		
		coordinates = assignedCoords;
		gameObjects = new ArrayList<GameObject>();
		cover = new TileCover();
		playerOccupied = false;
		excavated = false;
		base = false;
		
	}
	
	public boolean isPlayerPresent() {
		return playerOccupied;
	}
	
	public int totalElementObjectCount() {
		return gameObjects.size();
	}
	
	/**
	 * Add a GameObject to the tile's element list
	 * @param toAdd the GameObject to be added
	 */
	public void addObjectToTile(GameObject toAdd) {
		gameObjects.add(toAdd);
	}
	
	
	/**
	 * Finds and returns a copy of the GameElement in the element list of the tile
	 * @param toFindName the name of GameElement to find
	 * @returns the GameElement if found, if not returns null
	 */
	public GameObject findElement(String toFindName) {
		GameObject toFind = null;
		for(GameObject object: gameObjects) {
			if(object.name.equals(toFindName)) toFind = object;
		}
		return toFind;
	}
	
	
	/**
	 * Remove a GameElement from the tile's element list
	 * @param toRemoveName the name of the element to remove
	 * @returns true if removal was successful, false otherwise
	 */
	public boolean removeElementFromTile(String toRemoveName) {
		GameObject toRemove = findElement(toRemoveName);
		if(toRemove != null) {
			return gameObjects.remove(toRemove);
		}
		return false;
	}
	

}
