using System;

namespace Player {

	public class Team {

		private final String name;
		private final List<Player> players;
		
		public Team(String name) {
			this.name = name;
			this.players = new ArrayList<>();
		}
		
		public String getName() { return name; }
		
		public List<Player> getPlayers() {
			return new ArrayList<>(players);
		}
		
		public void addPlayer(Player player) {
			players.add(player);
		}
	}
}