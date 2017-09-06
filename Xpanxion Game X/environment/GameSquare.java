package environment;

import java.util.ArrayList;
import java.util.List;
import java.awt.Point;

/**
 * A class representing a square element of space that can be occupied with 0..n elements or objects
 * @author Alex White
 *
 */
public class GameSquare {
	
	List<GameElement> elements;
	List<Object> objects;
	Point coordinates;
	boolean playerOccupied;
	
	public GameSquare(Point assignedCoords) {
		
		coordinates = assignedCoords;
		
		List<GameElement> squareElements = new ArrayList<GameElement>();
		elements = squareElements;
		List<Object> squareObjects = new ArrayList<Object>();
		objects = squareObjects;
		playerOccupied = false;
		
	}
	
	public boolean isPlayerPresent() {
		return playerOccupied;
	}
	
	public int totalElementObjectCount() {
		return elements.size() + objects.size();
	}
	
	/**
	 * Add a GameElement to the GameSquare's element list
	 * @param toAdd the GameElement to be added
	 */
	public void addElementToSquare(GameElement toAdd) {
		elements.add(toAdd);
	}
	
	//TODO
	public void addObjectToSquare() {
		
	}
	
	/**
	 * Finds and returns a copy of the GameElement in the element list of the GameSquare
	 * @param toFindName the name of GameElement to find
	 * @returns the GameElement if found, if not returns null
	 */
	public GameElement findElement(String toFindName) {
		GameElement toFind = null;
		for(GameElement element: elements) {
			if(element.name.equals(toFindName)) toFind = element;
		}
		return toFind;
	}
	
	//TODO
	public void findGameObject() {
		
	}
	
	/**
	 * Remove a GameElement from the GameSquare's element list
	 * @param toRemoveName the name of the element to remove
	 * @returns true if removal was successful, false otherwise
	 */
	public boolean removeElementFromSquare(String toRemoveName) {
		GameElement toRemove = findElement(toRemoveName);
		if(toRemove != null) {
			return elements.remove(toRemove);
		}
		return false;
	}
	
	//TODO
	public void removeObjectFromSquare() {
		
	}

}
