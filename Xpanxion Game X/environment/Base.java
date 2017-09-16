package environment;

import system.GameObject;
import system.GameObjectList;

public class Base extends Structure {

	private GameObjectList inventory;
	
	public Base() {
		super("Base", "B");
		this.inventory = new GameObjectList();
		this.value = Integer.MAX_VALUE;
	}
	
	@Override
	public boolean isTraversible() { return true; }
	
	public GameObject getObject(GameObject obj) {
		return inventory.get(inventory.indexOf(obj));
	}
	
	public void storeObject(GameObject obj) {
		inventory.add(obj);
	}

	public void storeAll(GameObjectList objects) {
		inventory.addAll(objects);
	}
	
}
