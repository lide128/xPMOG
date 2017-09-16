using System;
using Environment;

namespace Player {

	public class Team {

		private readonly String name;
		private readonly List<Player> players;
		private readonly List<Base> bases;

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

		public void addBase(Base b) {
			bases.add(b);
		}
	}

}