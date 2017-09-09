package player;

import java.util.Optional;

import system.GameObject;

public class Player {

	private final char symbol;
	private final Inventory inventory;
	
	public Player(char symbol) {
		this.symbol = symbol;
		this.inventory = new Inventory();
	}
	
	public char getSymbol() { return symbol; }
	
	public int currentMoney() { return inventory.currentMoney(); }
	public void acquireMoney(int credits) { inventory.addMoney(credits); }
	public boolean spendMoney(int credits) { return inventory.spendMoney(credits); }
	
	/** @return an {@code Optional} which contains the GameObject that did not fit into this player's inventory */
	public Optional<? extends GameObject> acquireObject(GameObject obj) {
		return inventory.add(obj);
	}
	
}
