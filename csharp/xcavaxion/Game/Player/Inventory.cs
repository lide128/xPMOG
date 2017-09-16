using System;

using system;
using Util;

namespace Player {

	public class Inventory {

		private int currentMoney { get; } // credits held
		private GameObjectList inventory;
		private int maxWeight; // grams
		private int maxVolume; // cm^3

		public Inventory() {
			this.currentMoney = 0;
			this.inventory = new GameObjectList();
			// Set max weight to hold at least one hundred liters of anything: 
			// kg/m^3 * 1000 g/kg / 1000 liters/m^3 * 100 liter = grams
			this.maxWeight = ElementKind.heaviestElement().getDensity() * 100;
			//		this.maxWeight = Integer.MAX_VALUE;
			this.maxVolume = 1000000; // 1,000,000 cm^3 = one cubic meter
			//		this.maxVolume = Integer.MAX_VALUE;
		}

		/**
	 * @return the total value in credits of this inventory, 
	 * including its money and the total value of all contained {@link GameObject}s
	 */
		public int getValue() {
			return currentMoney() + inventory.getTotalValue();
		}

		public boolean isEmpty() {
			return inventory.isEmpty();
		}

		public boolean isFull() {
			return inventory.getTotalWeight() >= maxWeight || inventory.getTotalVolume() >= maxVolume;
		}

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
		Optional<? : GameObject> add(GameObject obj) {
			if (obj is GameElement)
				return addAndSpillOver((GameElement) obj);
			if (inventory.getTotalWeight() + obj.getWeight() > maxWeight 
				|| inventory.getTotalVolume() + obj.getVolume() > maxVolume)
				return Optional.of(obj);
			inventory.add(obj);
			return Optional.empty();
		}

		Optional<GameObjectList> addAll(GameObjectList objects) {
			while (!objects.isEmpty()) {
				Optional<? : GameObject> added = add(objects.remove(0));
				if (added.isPresent()){
					objects.add(added.get());
					return Optional.of(objects);
				}
			}
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

		GameObject removeObject(GameObject obj) throws ObjectNotFoundException {
			if (!inventory.contains(obj))
				throw new ObjectNotFoundException();
			return inventory.get(inventory.indexOf(obj));
		}

		GameObjectList removeAllObjects() {
			GameObjectList oldObjects = inventory;
			inventory = new GameObjectList();
			return oldObjects;
		}

		public static class ObjectNotFoundException extends Exception {
			public ObjectNotFoundException() {
				super("Object not found!");
			}
		}


	}

}