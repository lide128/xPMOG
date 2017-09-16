using System;
using system.GameObject;

namespace Player {

	public class Player {

		private String name;
		private final char symbol;
		private final Inventory inventory;
		
		public Player(String name, char symbol) {
			this.name = name;
			this.symbol = symbol;
			this.inventory = new Inventory();
		}
		
		public char getSymbol() { return symbol; }
		
		public int currentMoney() { return inventory.currentMoney(); }
		public void acquireMoney(int credits) { inventory.addMoney(credits); }
		public boolean spendMoney(int credits) { return inventory.spendMoney(credits); }
		
		public int netWorth() { return inventory.getValue(); }
		
		/** @return an {@code Optional} which contains the GameObject that did not fit into this player's inventory */
		public Optional<? extends GameObject> acquireObject(GameObject obj) {
			return inventory.add(obj);
		}

		public String getName() { return name; }
		
	}
}