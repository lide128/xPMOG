package system;

/**
 * A class representing all possible in game objects except for the player
 * @author Alex White
 *
 */
public class GameObject {
	
	public String name;
	public String description; //a description of the object that will be displayed in game
	
	public int value; //how much the object is worth in the game currency
	

	public void changeName(String newName) {
		name = newName;
	}
	
	public void changeDescription(String newDescription) {
		description = newDescription;
	}
	
	public void changeValue(int newValue) {
		value = newValue;
	}
	
}
