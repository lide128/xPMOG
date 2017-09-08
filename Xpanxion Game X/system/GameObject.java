package system;

/**
 * A class representing all possible in game objects except for the player
 * @author Alex White
 *
 */
public class GameObject {
	
	public String name;
	public int value; //how much the object is worth in the game currency
	
	public GameObject(String objectName, int objectValue) {
		name = objectName;
		value = objectValue;
	}
	
	public void changeName(String newName) {
		name = newName;
	}
	
	public void changeValue(int newValue) {
		value = newValue;
	}
	
	public boolean equals(Object gameObject) {
		GameObject compare = (GameElement) gameObject;
		return compare.name.equals(name) &&
				compare.value == value;
	}
	
}
