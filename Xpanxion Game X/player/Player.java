package player;

import java.util.Optional;

import player.Inventory.ObjectNotFoundException;
import system.GameObject;
import system.GameObjectList;
import environment.Base;

public class Player {

	private String name;
	private final String symbol;
	private final Inventory inventory;
	private Team team;
	
	public Player(String name, String symbol, Team team) {
		this.name = name;
		this.symbol = symbol;
		this.inventory = new Inventory();
		this.team = team;
	}
	
	public String getName() { return name; }

	public String getSymbol() { return symbol; }
	
	Inventory getInventory() {
		return inventory;
	}

	public int currentMoney() { return inventory.currentMoney(); }
	public void acquireMoney(int credits) { inventory.addMoney(credits); }
	public boolean spendMoney(int credits) { return inventory.spendMoney(credits); }
	
	public int netWorth() { return inventory.getValue(); }
	
	/** @return an {@code Optional} which contains the GameObject that did not fit into this player's inventory */
	public Optional<? extends GameObject> acquireObject(GameObject obj) {
		return inventory.add(obj);
	}
	
	public Optional<GameObjectList> acquireAll(GameObjectList objects) {
		return inventory.addAll(objects);
	}
	
	public GameObject loseObject(GameObject obj) throws ObjectNotFoundException {
		return inventory.removeObject(obj);
	}
	
	public GameObjectList loseAllObjects() {
		return inventory.removeAllObjects();
	}

	public Team getTeam() { return team; }
	
	Base getHomeBase() {
		return getTeam().getBases().get(0);
	}
	
}
