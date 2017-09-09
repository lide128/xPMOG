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

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((name == null) ? 0 : name.hashCode());
		result = prime * result + value;
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		GameObject other = (GameObject) obj;
		if (name == null) {
			if (other.name != null)
				return false;
		} else if (!name.equals(other.name))
			return false;
		if (value != other.value)
			return false;
		return true;
	}
	
}
