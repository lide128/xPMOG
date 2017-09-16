using System;

using system.ElementKind;
using system.GameElement;
using system.GameObject;
using system.GameObjectList;

namespace Player {

public class Inventory {
	
	private int currentMoney; // credits held
	private GameObjectList inventory;
	private int maxWeight; // grams
	private int maxVolume; // cm^3
	
	public Inventory() {
		this.currentMoney = 0;
		this.inventory = new GameObjectList();
		// Set max weight to hold at least one liter of anything: 
		// kg/m^3 * 1000 g/kg / 1000 liters/m^3 * 1 liter = grams
		this.maxWeight = ElementKind.heaviestElement().getDensity() * 1;
		this.maxVolume = 10000; // 10,000 cm^3 = one cubic meter
	}
	
	/**
	 * @return the total value in credits of this inventory, 
	 * including its money and the total value of all contained {@link GameObject}s
	 */
	public int getValue() {
		return currentMoney() + inventory.getTotalValue();
	}
	
	int currentMoney() { return currentMoney; }
	
	void addMoney(int credits) {
		currentMoney += credits;
	}
	
	boolean spendMoney(int credits) {
		if (credits > currentMoney)
			return false;
		currentMoney -= credits;
		return true;
	}

	/** @return an {@code Optional} which contains the GameObject that did not fit into this inventory */
	Optional<? extends GameObject> add(GameObject obj) {
		if (obj instanceof GameElement)
			return addAndSpillOver((GameElement) obj);
		if (inventory.getTotalWeight() + obj.getWeight() > maxWeight 
				|| inventory.getTotalVolume() + obj.getVolume() > maxVolume)
			return Optional.of(obj);
		inventory.add(obj);
		return Optional.empty();
	}
	
	/** 
	 * @return an {@code Optional} which contains the GameElement that 
	 * represents portion of the given element that did not fit into this inventory
	 */
	Optional<GameElement> addAndSpillOver(GameElement element) {
		GameElement toAdd = element;
		Optional<GameElement> spillover = Optional.empty();
		
		int excess = inventory.getTotalVolume() + element.getVolume() - maxVolume;
		if (excess > 0) {
			toAdd = new GameElement(element.getKind(), element.getVolume() - excess);
			spillover = Optional.of(new GameElement(element.getKind(), excess));
		}
		GameElement contained = (GameElement) inventory.getByName(element.getName());
		if (contained != null)
			contained.absorb(toAdd);
		else
			inventory.add(toAdd);
		
		return spillover;
	}
	
}
