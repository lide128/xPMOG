package system;

import java.util.ArrayList;
import java.util.Optional;
import java.util.function.BinaryOperator;

public class GameObjectList extends ArrayList<GameObject>{

	/**
	 * Returns true if the list contains the GameObject specified by name
	 * @param name of the object to be searched
	 * @return true if the list contains the GameObject
	 */
	public boolean containsByName(String name) {
		boolean gameObjectFound = false;
		for(GameObject object: this) {
			if(object.name.equals(name)) gameObjectFound = true;
		}
		return gameObjectFound;
	}
	
	/**
	 * Provides the index of the first GameObject in the list matching the provided name
	 * @param name of the GameObject to find the index of
	 * @return the index of the GameObject searched for in the list, -1 if not found
	 */
	public int indexOfByName(String name) {
		for(int i = 0; i < this.size(); i++) {
			if(this.get(i).name.equals(name)) return i; 
		}
		return -1;
	}
	
	/**
	 * Returns the GameObject in the list specified by the name. Object remains in the list.
	 * @param name of the GameObject to get
	 * @return the GameObject, if not found null
	 */
	public GameObject getByName(String name) {
		GameObject toGet = null;
		for(GameObject object: this) {
			if(object.name.equals(name)) toGet = object;
		}
		return toGet;
	}
	
	/**
	 * Removes the first occurrence of the GameObject from the list matching the provided name.
	 * @param name of the GameObject to remove
	 * @return the GameObject
	 */
	public GameObject removeByName(String name) {
		int index = indexOfByName(name);
		return this.remove(index);
	}
	
	
	/**
	 * Remove all GameObjects from the list that match the name provided.
	 * @param name the name of the objects of which to remove
	 */
	public void removeAllMatchingName(String name) {
		while(removeByName(name) != null) { }
	}
	
	public int getTotalWeight() {
		return stream().mapToInt(GameObject::getWeight).sum();
	}
	
	public int getTotalVolume() {
		return stream().mapToInt(GameObject::getVolume).sum();
	}
	
	public int getTotalValue() {
		return stream().mapToInt(GameObject::getValue).sum();
	}
	
	public Optional<GameObject> mostValuableObject() {
		BinaryOperator<GameObject> bestFinder = new BinaryOperator<GameObject>() {
			@Override
			public GameObject apply(GameObject t, GameObject u) {
				return u.getValue() > t.getValue() ? u : t;
			}};
		return stream().reduce(bestFinder);
	}

}
