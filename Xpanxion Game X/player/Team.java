package player;

import java.util.ArrayList;
import java.util.List;

import environment.Base;

public class Team {

	private final String name;
	private final List<Player> players;
	private final List<Base> bases;
	
	public Team(String name) {
		this.name = name;
		this.players = new ArrayList<>();
		this.bases = new ArrayList<>();
	}
	
	public String getName() { return name; }
	
	public List<Player> getPlayers() {
		return new ArrayList<>(players);
	}
	
	public void addPlayer(Player player) {
		players.add(player);
	}
	
	public List<Base> getBases() {
		return new ArrayList<>(bases);
	}
	
	public void addBase(Base base) {
		bases.add(base);
	}
}
