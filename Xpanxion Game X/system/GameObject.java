package system;

/**
 * A class representing all possible in game objects except for the player
 * @author Alex White
 *
 */
public abstract class GameObject {
	
	public String name;
	
	public GameObject(String objectName) {
		name = objectName;
	}
	
	public String getName() { return name; }
	
	/** @return weight of this object in grams */
	public abstract int getWeight();
	
	
	/** @return volume of this object in cm^3 */
	public abstract int getVolume();
	
	/** @return the value of this object in credits */
	public abstract int getValue();
	
}
