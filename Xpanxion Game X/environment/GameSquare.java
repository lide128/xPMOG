package environment;

import java.util.ArrayList;
import java.util.List;
import java.awt.Point;

public class GameSquare {
	
	/*
	 * A square element of space that can be occupied with 0..n elements or objects
	 */
	
	List<Object> contents;
	Point coordinates;
	boolean playerOccupied;
	
	public GameSquare(Point assignedCoords) {
		
		List<Object> squareContents = new ArrayList<Object>();
		contents = squareContents;
		
		coordinates = assignedCoords;
		playerOccupied = false;
		
	}
	
	public void addToSquare(Object toAdd) {
		//check for player add
		contents.add(toAdd);
	}
	
	public void removeFromSquare(Object toRemove) {
		//check for player remove
		//find with GameElement or GameObject name
		//then remove
	}

}
